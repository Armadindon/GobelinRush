using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public enum Levels
    {
        Level1, 
        Level2, 
        Level3
    }

    #region Turret SerializeField
    [Header("Turret settings :")]
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

    public bool zoneRangeVisibility { get; private set; }

    public float rangeTurret { get; private set; }

    private float projectileNextShootTime;

    public TurretHUD m_TurretHUD { get; private set; }

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
        foreach (Transform m_ProjectileSpawn in m_ProjectileSpawns)
        {
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

    public void ChangeTurretLevel(Levels newLevel)
    {
        if (actualLevel == newLevel) return;
        if ((int)newLevel >= Enum.GetNames(typeof(Levels)).Length) return;
        
        GameObject m_newTurret = Instantiate(m_TurretPrefab[(int)actualLevel+1], gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
        actualLevel = newLevel;
        if ((int)actualLevel + 1 >= Enum.GetValues(typeof(Levels)).Length)
            m_TurretHUD.ChangeUpgradeVisibility(false);
    }

    public void NextTurretLevel()
    {
        if ((int)actualLevel + 1 >= Enum.GetValues(typeof(Levels)).Length) return;
        Levels newLevel = (Levels)Enum.GetValues(actualLevel.GetType()).GetValue((int)actualLevel + 1);
        ChangeTurretLevel(newLevel);
    }


    public int getTurretMoneyCost()
    {
        return turretMoneyCost;
    }


}
