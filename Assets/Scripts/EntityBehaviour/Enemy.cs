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

    [Header("Economy")]
    [SerializeField]
    private int moneyReward;


    [Header("Animation")]
    [SerializeField]
    private Animator m_animator;

    [Header("Score")]
    [SerializeField] private int scoreReward;

    
    //Gestion de la pause
    private bool paused = false;
    private Vector3 m_SavedVelocity;
    private Vector3 m_SavedAngularVelocity;


    public int getMoneyReward()
    {
        return moneyReward;
    }

    private void Awake()
    {
        //son du spawn
        FindObjectOfType<AudioManager>().Play("Enemy Spawn");

        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        m_Target = GameManager.Instance.FirstWaypoint;
        distToGround = gameObject.GetComponentInChildren<Collider>().bounds.extents.y;
        transform.LookAt(m_Target);
    }

    private bool IsGrounded()
    {
        //On regarde si un raycast en dessous touche un collider, si oui, c'est que l'on est au sol
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPlaying)
        {
            if (!paused)
            {
                paused = true;
                OnPauseGame();
            }
            return;
        }
        else if (paused)
        {
            paused = false;
            OnResumeGame();
        }

        // TODO : Trouver un moyen de redresser le personnage petit à petit
        Quaternion q = transform.rotation;
        q[0] = 0;
        q[2] = 0;
        transform.rotation = q;
        if (isAtTarget)
            m_animator.SetBool("Attack", true);
        //si le temps écoulé est toujours supérieur au cooldown et que l'ennemi a atteint la cible
        if (Time.time > attackCooldown && isAtTarget)
        {
            //son d'attaque
            FindObjectOfType<AudioManager>().Play(RandomAttackSound());

            //on retire de la vie en fonction des dégâts infligés
            m_CastleHealth.TakeDamage(attackDamage);
            //on réduit le cooldown
            attackCooldown = Time.time + attackCooldownDuration;
        }
    }

    /// <summary>
    /// Retourne un choix de son aléatoire pour le son d'attaque de l'ennemi
    /// </summary>
    private string RandomAttackSound()
    {
        string rtn = "";
        int choix = new System.Random().Next(0, 1);

        switch(choix)
        {
            case 0: rtn = "Sword Clash 1"; break;
            case 1: rtn = "Sword Clash 2"; break;
        }

        return rtn;
    }

    private void FixedUpdate()
    {
        if (paused) return;

        //On code le behaviour de l'enemy

        //On commence par régler la rotation
        if (m_Target)
        {
            Vector3 targetDelta = m_Target.position - transform.position;

            //On calcule la différence d'angle
            float angleDiff = Vector3.Angle(transform.forward, targetDelta);

            //Si l'angle la différence d'angle est en dessous un certain nombre, on fixe l'angle pour éviter que l'ennemi convulse
            if (angleDiff < 10)
            {
                transform.LookAt(m_Target);
            }

            Vector3 cross = Vector3.Cross(transform.forward, targetDelta);

            m_Rigidbody.AddTorque(cross * angleDiff * rotationSpeed);
        }

        //On Applique une plus grande gravité lorsqu'il est en l'air
        if (!IsGrounded()) m_Rigidbody.AddForce(Vector3.down * gravity * m_Rigidbody.mass);

        //n'est pas arrivé au château
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
            //atteint le château
            isAtTarget = true;
            //on récupère l'instance du château
            m_CastleTarget = other.gameObject.GetComponentInParent(typeof(Castle)) as Castle;
            //on récupère la vie du château
            m_CastleHealth = m_CastleTarget.gameObject.GetComponent<Health>();
            //on met la vélocité à 0
            m_Rigidbody.velocity = Vector3.zero;
        }
    }

    public void setTarget(Transform target)
    {
        this.m_Target = target;
    }

    void OnPauseGame()
    {
        m_SavedVelocity = m_Rigidbody.velocity;
        m_SavedAngularVelocity = m_Rigidbody.angularVelocity;
        m_Rigidbody.isKinematic = true;
    }

    void OnResumeGame()
    {
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.AddForce(m_SavedVelocity, ForceMode.VelocityChange);
        m_Rigidbody.AddTorque(m_SavedAngularVelocity, ForceMode.VelocityChange);
    }

    public int GetScoreReward()
    {
        return scoreReward;
    }
}
