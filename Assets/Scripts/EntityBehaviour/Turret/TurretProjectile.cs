using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoblinRush;

public class TurretProjectile : MonoBehaviour
{
    public float projectileSpeed { private get; set; }
    public Transform m_Target { private get; set; }
    public Turret m_Turret { private get; set; }

    public double attackDamage { get; set; }

    void Update()
    {
        //si d�tection d'un ennemi
        if (m_Target)
        {
            transform.LookAt(m_Target);
            //move projectile to target
            transform.position += transform.forward * Time.deltaTime * projectileSpeed;
        }
        else Destroy(gameObject);
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
            health.TakeDamage((int)attackDamage);

            if (health.currentHealth <= 0 && m_enemy != null)
            {
                GameManager.Instance.currentMoney += m_enemy.getMoneyReward();
                m_Turret.m_Enemies.Remove(m_enemy);
                Destroy(collision.gameObject);
            }

            // retire le projectile
            Destroy(gameObject);
        }
    }
}