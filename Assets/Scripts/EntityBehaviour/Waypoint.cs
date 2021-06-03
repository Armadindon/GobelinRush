using UnityEngine;
using GoblinRush;

[RequireComponent(typeof(Collider))]
public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private Transform[] nextTarget;
    [SerializeField]
    private bool isFirstWaypoint;

    private void SetCastleAsTarget()
    {

        nextTarget = new Transform[] { GameManager.Instance.castleTarget.transform }; // Si la prochain cible n'est pas renseign�e, on change de target
    }

    private void Start()
    {
        if (isFirstWaypoint)
            GameManager.Instance.FirstWaypoint = transform;
        if (nextTarget.Length == 0 && GameManager.Instance.castleTarget) SetCastleAsTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (nextTarget.Length == 0 && GameManager.Instance.castleTarget) SetCastleAsTarget();
        //TODO : Faire des tags pours les ennemis, ou mettre un layer
        //TODO : Mettre les waypoint dans un layer � part, pour �viter les collisions avec le sol
        if (other.gameObject.name.Contains("Enemy"))
        {
            Debug.Log("On change de direction !");
            GameObject enemy = other.gameObject;
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript)
            {
                enemyScript.setTarget(nextTarget[Random.Range(0, nextTarget.Length)]);
            }
        }
    }

}
