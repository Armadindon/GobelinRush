using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoblinRush;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy Movement")]
    [SerializeField]
    private Transform m_Target;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private LayerMask m_GroundLayer;

    [Header("Enemy Attack")]
    [SerializeField]
    private int attackDamage;
    [SerializeField]
    private int attackCooldownDuration;

    private float attackCooldown;

    private Rigidbody m_Rigidbody;
    private bool isAtTarget;
    private float distToGround;

    private Castle m_CastleTarget;
    private Health m_CastleHealth;

    private void Awake()
    {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        m_Target = GameManager.Instance.FirstWaypoint;
        distToGround = gameObject.GetComponentInChildren<Collider>().bounds.extents.y;
    }

    private bool IsGrounded()
    {
        //On regarde si un raycast en dessous touche un collider, si oui, c'est que l'on est au sol
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    private void Update()
    {
        // TODO : Trouver un moyen de redresser le personnage petit � petit
        Quaternion q = transform.rotation;
        q[0] = 0;
        q[2] = 0;
        transform.rotation = q;

        //si le temps �coul� est toujours sup�rieur au cooldown et que l'ennemi a atteint la cible
        if (Time.time > attackCooldown && isAtTarget)
        {
            //on retire de la vie en fonction des d�g�ts inflig�s
            m_CastleHealth.TakeDamage(attackDamage);
            //on r�duit le cooldown
            attackCooldown = Time.time + attackCooldownDuration;
        }
    }

    private void FixedUpdate()
    {
        //On code le behaviour de l'enemy

        //On commence par r�gler la rotation
        if (m_Target)
        {
            Vector3 targetDelta = m_Target.position - transform.position;

            //On calcule la diff�rence d'angle
            float angleDiff = Vector3.Angle(transform.forward, targetDelta);

            //Si l'angle la diff�rence d'angle est en dessous un certain nombre, on fixe l'angle pour �viter que l'ennemi convulse
            if (angleDiff < 10)
            {
                transform.LookAt(m_Target);
            }

            Vector3 cross = Vector3.Cross(transform.forward, targetDelta);

            m_Rigidbody.AddTorque(cross * angleDiff * rotationSpeed);
        }

        //On Applique une plus grande gravit� lorsqu'il est en l'air
        if (!IsGrounded()) m_Rigidbody.AddForce(Vector3.down * gravity * m_Rigidbody.mass);

        //n'est pas arriv� au ch�teau
        if (!isAtTarget)
        {
            // L'ennemi avance toujours devant lui
            Vector3 newVelocity = speed * transform.forward;
            Vector3 velocityChange = newVelocity - m_Rigidbody.velocity;
            m_Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("CastleHitbox"))
        {
            Debug.Log("On a atteint le ch�teau");
            //atteint le ch�teau
            isAtTarget = true;
            //on r�cup�re l'instance du ch�teau
            m_CastleTarget = other.gameObject.GetComponentInParent(typeof(Castle)) as Castle;
            //on r�cup�re la vie du ch�teau
            m_CastleHealth = m_CastleTarget.gameObject.GetComponent<Health>();
            //on met la v�locit� � 0
            m_Rigidbody.velocity = Vector3.zero;

            Debug.Log(m_CastleHealth.currentHealth);
        }
    }


    public void setTarget(Transform target)
    {
        this.m_Target = target;
    }
}
