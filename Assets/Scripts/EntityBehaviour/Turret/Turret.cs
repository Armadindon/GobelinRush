using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GoblinRush;
using System.Text.RegularExpressions;

public class Turret : MonoBehaviour
{
    public enum TypeTurret
    {
        Crossbow,
        Cannon
    }
    public enum Levels
    {
        Level1, 
        Level2, 
        Level3
    }

    #region Turret SerializeField
    [Header("Turret settings :")]
    [SerializeField] public TypeTurret typeTurret;
    [Tooltip("Projectile prefab")]
    [SerializeField] private GameObject m_ProjectilePrefab;

    [Tooltip("Turret range")]
    [SerializeField] private float _rangeTurret;

    [Tooltip("Projectile speed")]
    [SerializeField] private float projectileSpeed;
    

    [Tooltip("Turret cooldown")]
    [SerializeField] private float projectileShootCooldown;

    [Tooltip("Projectile spawn")]
    [SerializeField] private List<Transform> m_ProjectileSpawns;

    [Tooltip("Projectile damage")]
    [SerializeField] private double projectileDamage;
    #endregion

    #region Turret upgrade SerializeField
    [Header("Turret upgrade :")]
    [Tooltip("HUD visibility when upgrade")]
    [SerializeField] public bool HUDVisibilityOnUpgrade;

    [Tooltip("Level turret")]
    [SerializeField] private Levels _actualLevel;

    [SerializeField] private List<GameObject> m_TurretPrefab;
    #endregion

    #region Zone SerializeField
    [Header("Zone settings :")]
    [Tooltip("Zone range")]
    [SerializeField] private GameObject m_ZoneRangeSphere;
    #endregion

    #region Economy SerializeField
    [Header("Economy :")]
    [SerializeField] private int turretMoneyCost;
    #endregion

    /// <summary>
    /// Range visibility
    /// </summary>
    public bool zoneRangeVisibility { get; private set; }

    /// <summary>
    /// Range current turret
    /// </summary>
    public float rangeTurret { get; private set; }

    private float projectileNextShootTime;

    /// <summary>
    /// Link Turret HUD
    /// </summary>
    public TurretHUD m_TurretHUD { get; private set; }

    /// <summary>
    /// Level of current turret
    /// </summary>
    public Levels actualLevel { get; private set; }

    /// <summary>
    /// List of enemy in range of turret
    /// </summary>
    public List<Enemy> m_Enemies { get; set; }

    void Awake()
    {
        //create list of enemy
        m_Enemies = new List<Enemy>();
        //setup zone range visibility
        ChangeVisibilityRange(false);
        //setup range 
        ChangeRangeTurret(_rangeTurret);

        actualLevel = _actualLevel;

        //get turret HUD
        m_TurretHUD = gameObject.GetComponentInChildren<TurretHUD>();
        m_TurretHUD.m_Turret = this;
    }

    private void OnValidate()
    {
        //setup range 
        ChangeRangeTurret(_rangeTurret);
    }

    void Update()
    {
        if (!GameManager.Instance.IsPlaying) return;

        //delete all null enemy
        foreach (var enemy in m_Enemies.ToList())
            if (enemy == null) m_Enemies.Remove(enemy);

        //if there is enemy
        if (m_Enemies.Count > 0)
        {
            //delete null enemies
            if (m_Enemies.Count <= 0) return;

            //get look position
            Vector3 lookPos = m_Enemies[0].transform.position - transform.position;
            //don't allow y moove
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            //rotate turret
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100);

            //if can shoot
            if (Time.time > projectileNextShootTime)
            {
                ShootProjectile(m_Enemies[0].gameObject.transform);
                //wait before shoot again
                projectileNextShootTime = Time.time + projectileShootCooldown;
            }
        }
    }

    // if entities enter the range
    private void OnTriggerEnter(Collider other)
    {
        //Get enemy
        Enemy m_enemy = other.gameObject.GetComponentInParent(typeof(Enemy)) as Enemy;
        //if its an enemy add it in the list
        if (m_enemy) m_Enemies.Add(m_enemy);
    }
    
    //if entities exit the range
    private void OnTriggerExit(Collider other)
    {
        //Get enemy
        Enemy m_enemy = other.gameObject.GetComponentInParent(typeof(Enemy)) as Enemy;
        //if its an enemy remove it from the list
        if (m_enemy) m_Enemies.Remove(m_enemy);
    }

    private void OnMouseExit()
    {
        //if Zone range visible hide it 
        if (zoneRangeVisibility == true) ChangeVisibilityRange(false);
        //if HUD turret visible hid it
        if (m_TurretHUD.HUDVisibilty == true) m_TurretHUD.ChangeHUDVisibility(false);
    }

    /// <summary>
    /// Shoot projectile to target
    /// </summary>
    /// <param name="target"></param>
    private void ShootProjectile(Transform target)
    {
        //shoot mulyiple projectile
        foreach (Transform m_ProjectileSpawn in m_ProjectileSpawns)
        {
            
            //on choisit le son en fonction du type de tourelle
            string sound = ( m_ProjectilePrefab.gameObject.name == "Arrow" ? "Ballist Shoot" : "Cannon Shoot");
            //son d'attaque
            AudioManager.Instance.Play(sound);

            //Create projectile
            GameObject m_newProjectile = Instantiate(m_ProjectilePrefab, m_ProjectileSpawn.position, Quaternion.identity);
            //get projectile
            TurretProjectile m_TurretProjectile = m_newProjectile.GetComponent<TurretProjectile>();
            //Setup the speed
            m_TurretProjectile.projectileSpeed = projectileSpeed;
            //setup the target enemy
            m_TurretProjectile.m_Target = target;
            m_TurretProjectile.m_Turret = this;
            //on attribue les dommages de la tourelle au boulet
            m_TurretProjectile.attackDamage = projectileDamage;
        }

    }

    /// <summary>
    /// Change range turret
    /// </summary>
    /// <param name="newRange"></param>
    public void ChangeRangeTurret(float newRange)
    {
        if (rangeTurret == newRange) return;
        Transform zoneRange = this.transform.Find("Zone_Range");
        zoneRange.localScale = new Vector3(newRange, newRange, newRange);
        _rangeTurret = rangeTurret = newRange;
    }

    /// <summary>
    /// Change visibility of zone range 
    /// </summary>
    /// <param name="newVisibility"></param>
    public void ChangeVisibilityRange(bool newVisibility)
    {
        if (zoneRangeVisibility == newVisibility) return;
        m_ZoneRangeSphere.GetComponent<Renderer>().enabled = newVisibility;
        zoneRangeVisibility = newVisibility;
    }

    /// <summary>
    /// Change turret level
    /// </summary>
    /// <param name="newLevel"></param>
    public void ChangeTurretLevel(Levels newLevel, bool force = false)
    {
        if (actualLevel == newLevel) return;
        if ((int)newLevel >= Enum.GetNames(typeof(Levels)).Length) return;
        GameObject m_newTurretPrefab = m_TurretPrefab[(int)actualLevel + 1];
        //if can't buy it
        if (GameManager.Instance.currentMoney < m_newTurretPrefab.GetComponent<Turret>().getTurretMoneyCost() && !force) return;
        //money cost
        if(!force) GameManager.Instance.currentMoney -= m_newTurretPrefab.GetComponent<Turret>().getTurretMoneyCost();
        //create new turret level
        GameObject m_newTurret = Instantiate(m_newTurretPrefab, gameObject.transform.position, Quaternion.identity);

        //son d'upgrade
        AudioManager.Instance.Play("Construction");

        //Destroy old level turret
        Destroy(gameObject);
        actualLevel = newLevel;
        if ((int)actualLevel + 1 >= Enum.GetValues(typeof(Levels)).Length)
            m_TurretHUD.DeleteUpgrade();   
    }

    /// <summary>
    /// Next level of turret
    /// </summary>
    public void NextTurretLevel()
    {
        if ((int)actualLevel + 1 >= Enum.GetValues(typeof(Levels)).Length) return;
        Levels newLevel = (Levels)Enum.GetValues(actualLevel.GetType()).GetValue((int)actualLevel + 1);
        ChangeTurretLevel(newLevel);
    }

    /// <summary>
    /// Get money cost turret
    /// </summary>
    /// <returns></returns>
    public int getTurretMoneyCost()
    {
        return turretMoneyCost;
    }

    /// <summary>
    /// Return money cost of next level turret
    /// </summary>
    /// <returns></returns>
    public int getNextLevelTurretMoneyCost()
    {
        if((int)actualLevel + 1 >= Enum.GetValues(typeof(Levels)).Length) return int.MaxValue;
        GameObject m_nextLevelTurretPrefab = m_TurretPrefab[(int)actualLevel + 1];
        Turret m_nextLevelTurret = (Turret)m_nextLevelTurretPrefab.GetComponent(typeof(Turret));
        return m_nextLevelTurret.getTurretMoneyCost();
    }
}
