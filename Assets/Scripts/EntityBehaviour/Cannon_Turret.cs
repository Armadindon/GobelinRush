using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Turret : MonoBehaviour
{
    public enum Levels
    {
        NONE, Level1, Level2, Level3
    }

    [Header("Cannonball settings :")]
    [Tooltip("Cannonball prefab")]
    [SerializeField] private GameObject m_CannonballPrefab;

    [Tooltip("Cannonball range")]
    [SerializeField] private float _rangeCannonball;

    [Tooltip("Cannonball speed")]
    [SerializeField] private float cannonballSpeed;

    [Tooltip("Cannonball cooldown")]
    [SerializeField] private float cannonballShootCooldown;

    [Tooltip("Cannonball spawn")]
    [SerializeField] private Transform m_CannonballSpawn;

    [Tooltip("Cannonball damage")]
    [SerializeField] public double cannonballDamage;


    [Header("Zone settings :")]
    [Tooltip("Zone range")]
    [SerializeField] private GameObject m_ZoneRangeSphere;

    [Tooltip("Set visibility on create")]
    [SerializeField] private bool _zoneRangeVisibility;


    [Header("Turret upgrade :")]
    [Tooltip("HUD visibility when upgrade")]
    [SerializeField] public bool HUDVisibilityOnUpgrade;

    [Tooltip("Level turret")]
    [SerializeField] private Levels _actualLevel;

    [Tooltip("Damage turret by level")]
    [SerializeField] public double damageLevelUpgrade;

    [Tooltip("Scale turret by level")]
    [SerializeField] public double scaleLevelUpgrade;

    [Tooltip("Cooldown reduction upgrade")]
    [SerializeField] public float cooldownReductionUpgrade;


    public bool zoneRangeVisibility { get; private set; }

    public float rangeCannonball { get; private set; }

    private float cannonballNextShootTime;

    [Header("Economy")]
    [SerializeField]
    private int moneyCost;
    
    public TurretHUD m_TurretHUD { get; private set; }

    public int getMoneyCost()
    {
        return moneyCost;
    }
    
    public Levels actualLevel { get; private set; }

    private Vector3 initScale;



    /// <summary>
    /// List of enemy in range of turret
    /// </summary>
    public List<Enemy> m_Enemies { get; set; }

    void Awake()
    {
        //create list of enemy
        m_Enemies = new List<Enemy>();  
        //setup zone range visibility
        ChangeVisibilityRange(_zoneRangeVisibility);
        //setup range cannonball
        ChangeRangeTurret(_rangeCannonball);

        //get turret HUD
        m_TurretHUD = gameObject.GetComponentInChildren<TurretHUD>();
        //m_TurretHUD.m_CannonTurret = this;

        //setup level turret
        initScale = transform.localScale;
        if(initScale != Vector3.zero)  ChangeTurretLevel(_actualLevel);
    }

    private void OnValidate()
    {
        //setup zone range visibility
        ChangeVisibilityRange(_zoneRangeVisibility);
        //setup range cannonball
        ChangeRangeTurret(_rangeCannonball);
        //setup level turret
        ChangeTurretLevel(_actualLevel);
    }

    void Update()
    {
        //if there is enemy
        if (m_Enemies.Count > 0)
        {
            //delete null enemies
            foreach (var enemy in m_Enemies)
                if(enemy==null) m_Enemies.Remove(enemy);

            if (m_Enemies.Count <= 0) return;
                
            //get look position
            Vector3 lookPos = m_Enemies[0].transform.position - transform.position;
            //don't allow y moove
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            //rotate Cannon turret
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100);

            //if can shoot
            if ( Time.time > cannonballNextShootTime)
            {
                ShootCannonball(m_Enemies[0].gameObject.transform);
                //wait before shoot again
                cannonballNextShootTime = Time.time + cannonballShootCooldown - (cooldownReductionUpgrade * ((int)actualLevel - 1));
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

    /// <summary>
    /// Shoot cannonball to target
    /// </summary>
    /// <param name="target"></param>
    private void ShootCannonball(Transform target)
    {
        //Create cannonball
        GameObject m_newCannonball = Instantiate(m_CannonballPrefab, m_CannonballSpawn.position, Quaternion.identity);
        //get Cannonball
        CannonBall m_Cannonball = m_newCannonball.GetComponent<CannonBall>();
        //Setup the speed
        m_Cannonball.cannonballSpeed = cannonballSpeed;
        //setup the target enemy
        m_Cannonball.m_Target = target;
        m_Cannonball.m_CannonTurret = this;
        //on attribue les dommages de la tourelle au boulet
        m_Cannonball.attackDamage = cannonballDamage + damageLevelUpgrade * ((int)actualLevel-1);
    }

    /// <summary>
    /// Change range turret
    /// </summary>
    /// <param name="newRange"></param>
    public void ChangeRangeTurret(float newRange)
    {
        if (rangeCannonball == newRange) return;
        Transform zoneRange = this.transform.Find("Zone_Range");
        zoneRange.localScale = new Vector3(newRange, newRange, newRange);
        _rangeCannonball = rangeCannonball = newRange;
    }

    /// <summary>
    /// Change visibility of zone range 
    /// </summary>
    /// <param name="newVisibility"></param>
    public void ChangeVisibilityRange(bool newVisibility)
    {
        if (zoneRangeVisibility == newVisibility) return;
        m_ZoneRangeSphere.GetComponent<Renderer>().enabled = newVisibility;
        _zoneRangeVisibility = zoneRangeVisibility = newVisibility;
    }


    private void OnMouseExit()
    {
        //if Zone range visible hide it 
        if(zoneRangeVisibility == true) ChangeVisibilityRange(false);
        //if HUD turret visible hid it
        if (m_TurretHUD.HUDVisibilty == true) m_TurretHUD.ChangeHUDVisibility(false);
    }

    public void ChangeTurretLevel(Levels newLevel)
    {
        if (actualLevel == newLevel) return;
        if ((int)newLevel > Enum.GetNames(typeof(Levels)).Length) return;
        
        Vector3 newScale = new Vector3( (float)(initScale.x + scaleLevelUpgrade * ((int)newLevel - 1)), 
                                        (float)(initScale.y + scaleLevelUpgrade * ((int)newLevel - 1)), 
                                        (float)(initScale.z + scaleLevelUpgrade * ((int)newLevel - 1)));
        if (newScale == Vector3.zero || newScale.magnitude < Vector3.zero.magnitude) return;
        transform.localScale = newScale;
        _actualLevel = actualLevel = newLevel;
        if ((int)actualLevel+1 >= Enum.GetValues(typeof(Levels)).Length)
            m_TurretHUD.ChangeUpgradeVisibility(false);
    }
    
    public void NextTurretLevel()
    {
        if ((int)actualLevel + 1 >= Enum.GetValues(typeof(Levels)).Length) return;
        Levels newLevel = (Cannon_Turret.Levels)Enum.GetValues(actualLevel.GetType()).GetValue((int)actualLevel + 1);
        ChangeTurretLevel(newLevel);
    }
}
