using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Turret : MonoBehaviour
{
    [Header("Cannonball settings")]
    [Tooltip("Cannonball prefab")]
    [SerializeField] private GameObject m_CannonballPrefab;

    [Tooltip("Cannonball range")]
    [SerializeField] private float rangeCannonball;

    [Tooltip("Cannonball speed")]
    [SerializeField] private float cannonballSpeed;

    [Tooltip("Cannonball cooldown")]
    [SerializeField] private float cannonballShootCallDownDuration;

    [Tooltip("Cannonball spawn")]
    [SerializeField] private Transform m_CannonballSpawn;

    [Tooltip("Cannonball damage")]
    [SerializeField] private int cannonballDamage;

    private float cannonballNextShootTime;

    //List of enemy in range 
    public List<Enemy> m_Enemies { get; set; }

    void Awake()
    {
        //create list of enemy
        m_Enemies = new List<Enemy>();
    }

    void Update()
    {
        //if there is enemy
        if (m_Enemies.Count > 0)
        {
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
                cannonballNextShootTime = Time.time + cannonballShootCallDownDuration;
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

    void ShootCannonball(Transform target)
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
    }
}
