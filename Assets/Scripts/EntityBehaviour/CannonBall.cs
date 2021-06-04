using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float cannonballSpeed { private get; set; }
    public Transform m_Target { private get; set; }
    public Cannon_Turret m_CannonTurret { private get; set; }

    void Update()
    {
        if (m_Target)
        {
            transform.LookAt(m_Target);
            //moove CannonBall to target
            transform.position += transform.forward * Time.deltaTime * cannonballSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if collision with enemy
        if (collision.gameObject.name.Contains("Enemy"))
        {
            //get enemy 
            Enemy m_enemy = collision.gameObject.GetComponentInParent(typeof(Enemy)) as Enemy;
            //remov enemy from the list enemy in range
            m_CannonTurret.m_Enemies.Remove(m_enemy);

            //Destroy enemy and CannonBall
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
