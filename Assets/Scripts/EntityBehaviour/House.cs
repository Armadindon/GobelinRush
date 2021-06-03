using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField]
    private Transform enemySpawnPoint;
    [SerializeField]
    private GameObject enemyPrefab;

    private void Start()
    {
        if (enemyPrefab)
        {
            Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
        }
    }
}
