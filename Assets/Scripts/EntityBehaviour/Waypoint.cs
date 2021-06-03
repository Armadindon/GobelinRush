using UnityEngine;
using GoblinRush;

[RequireComponent(typeof(Collider))]
public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private Transform[] nextTarget;
    [SerializeField]
    private bool isFirstWaypoint;

    private void Start()
    {
        if (isFirstWaypoint)
            GameManager.Instance.FirstWaypoint = transform;
        if (nextTarget.Length == 0) nextTarget = new Transform[] { GameManager.Instance.castleTarget.transform }; // Si la prochain cible n'est pas renseignée, on change de target
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        //TODO : Faire des tags pours les ennemis, ou mettre un layer
        //TODO : Mettre les waypoint dans un layer à part, pour éviter les collisions avec le sol
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
