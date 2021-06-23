using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoblinRush;

public class TurretProjectile : MonoBehaviour
{
    public float projectileSpeed { private get; set; }
    public GameObject m_Target { private get; set; }
    public Turret m_Turret { private get; set; }

    public double attackDamage { get; set; }

    void Update()
    {
        if (!GameManager.Instance.IsPlaying) return;

        //si détection d'un ennemi
        if (m_Target)
        {
            transform.LookAt(m_Target.GetComponentInChildren<Renderer>().bounds.center);
            //move projectile to target
            transform.position += transform.forward * Time.deltaTime * projectileSpeed;
        }
        else Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if collision with enemy
        if (collision.gameObject.GetComponentInParent(typeof(Enemy)))
        {
            // on récupère l'ennemi
            Enemy m_enemy = collision.gameObject.GetComponentInParent(typeof(Enemy)) as Enemy;
            // on récupère son component.script Life
            Health health = collision.gameObject.GetComponent<Health>();
            // l'ennemi encaisse des dégâts
            health.TakeDamage((int)attackDamage,collision.contacts[0].point); //Permet d'avoir la particule de hit qui spawn a l'emplacement du hit
            if (health.currentHealth <= 0 && m_enemy != null)
            {
                health.DeathAnimation();
                AudioManager.Instance.Play("Enemy Death");

                GameManager.Instance.currentMoney += m_enemy.getMoneyReward();
                m_Turret.m_Enemies.Remove(m_enemy);
                Debug.Log("On ajoute du score");
                GameManager.Instance.EnemyKilled += collision.gameObject.GetComponent<Enemy>().GetScoreReward();
                Destroy(collision.gameObject);
            }

            // retire le projectile
            Destroy(gameObject);
        }
    }
}
