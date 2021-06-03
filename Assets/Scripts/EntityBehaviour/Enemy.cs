using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoblinRush;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy Movement")]
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private LayerMask groundLayer;

    private Rigidbody m_rigidbody;
    private bool isAtTarget;
    private float distToGround;

    private void Awake()
    {
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        target = GameManager.Instance.FirstWaypoint;
        distToGround = gameObject.GetComponentInChildren<Collider>().bounds.extents.y;
    }

    private bool IsGrounded() {
        //On regarde si un raycast en dessous touche un collider, si oui, c'est que l'on est au sol
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    private void FixedUpdate()
    {
        //On code le behaviour de l'enemy

        //On commence par régler la rotation
        if (target)
        {
            Vector3 targetDelta = target.position - transform.position;

            //On calcule la différence d'angle
            float angleDiff = Vector3.Angle(transform.forward, targetDelta);
            
            //Si l'angle la différence d'angle est en dessous un certain nombre, on fixe l'angle pour éviter que l'ennemi convulse
            if(angleDiff < 10)
            {
                transform.LookAt(target);
            }

            Vector3 cross = Vector3.Cross(transform.forward, targetDelta);

            // apply torque along that axis according to the magnitude of the angle.
            m_rigidbody.AddTorque(cross * angleDiff * rotationSpeed);
        }

        //On Applique une plus grande gravité lorsqu'il est en l'air
        if (!IsGrounded()) m_rigidbody.AddForce(Vector3.down * gravity * m_rigidbody.mass);

        if (!isAtTarget)
        {
            // L'ennemi avance toujours devant lui
            Vector3 newVelocity = speed * transform.forward;
            Vector3 velocityChange = newVelocity - m_rigidbody.velocity;
            m_rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        
    }


    public void setTarget(Transform target)
    {
        this.target = target;
    }
}
