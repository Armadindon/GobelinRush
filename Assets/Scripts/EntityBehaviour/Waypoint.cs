using UnityEngine;
using GoblinRush;

[RequireComponent(typeof(Collider))]
public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private Transform[] m_nextTarget;
    [SerializeField]
    private bool isFirstWaypoint;

    private void SetCastleAsTarget()
    {
        m_nextTarget = new Transform[] { GameManager.Instance.CastleTarget.transform }; // Si la prochain cible n'est pas renseignée, on change de target
    }

    private void Start()
    {
        if (isFirstWaypoint)
            GameManager.Instance.FirstWaypoint = transform;
        if (m_nextTarget.Length == 0 && GameManager.Instance.CastleTarget) SetCastleAsTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_nextTarget.Length == 0 && GameManager.Instance.CastleTarget) SetCastleAsTarget();
        //TODO : Faire des tags pours les ennemis, ou mettre un layer
        //TODO : Mettre les waypoint dans un layer à part, pour éviter les collisions avec le sol
        if (other.gameObject.name.Contains("Enemy"))
        {
            Debug.Log("On change de direction !");
            GameObject enemy = other.gameObject;
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript)
            {
                enemyScript.setTarget(m_nextTarget[Random.Range(0, m_nextTarget.Length)]);
            }
        }
    }

}
