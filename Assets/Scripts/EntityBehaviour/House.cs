using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField]
    private Transform m_enemySpawnPoint;
    [SerializeField]
    private GameObject m_enemyPrefab;

    private void Start()
    {
        if (m_enemyPrefab)
        {
            Instantiate(m_enemyPrefab, m_enemySpawnPoint.position, Quaternion.identity);
        }
    }
}
