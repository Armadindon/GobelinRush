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
            // on récupère l'ennemi
            Enemy m_enemy = collision.gameObject.GetComponentInParent(typeof(Enemy)) as Enemy;
            // on récupère son component.script Life
            Life life = collision.gameObject.GetComponent<Life>();
            // l'ennemi encaisse des dégâts
            life.TakeDamage(m_CannonTurret.m_Damage);

            // retire le boulet de canon
            Destroy(this.gameObject);
        }
    }
}
