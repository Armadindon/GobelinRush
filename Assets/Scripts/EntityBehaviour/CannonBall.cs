using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CannonBall : MonoBehaviour
{
    public float cannonballSpeed { private get; set; }
    public Transform m_Target { private get; set; }
    public Cannon_Turret m_CannonTurret { private get; set; }

    public int attackDamage { get; set; }

    void Update()
    {
        //si d�tection d'un ennemi
        if (m_Target)
        {
            transform.LookAt(m_Target);
            //move CannonBall to target
            transform.position += transform.forward * Time.deltaTime * cannonballSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if collision with enemy
        if (collision.gameObject.name.Contains("Enemy"))
        {
            // on r�cup�re l'ennemi
            Enemy m_enemy = collision.gameObject.GetComponentInParent(typeof(Enemy)) as Enemy;
            // on r�cup�re son component.script Life
            Health health = collision.gameObject.GetComponent<Health>();
            // l'ennemi encaisse des d�g�ts
            health.TakeDamage(attackDamage);

            if (health.currentHealth <= 0)
            {
                Destroy(collision.gameObject);
                m_CannonTurret.m_Enemies.Remove(m_enemy);
            }

            // retire le boulet de canon
            Destroy(this.gameObject);
        }
    }

}
