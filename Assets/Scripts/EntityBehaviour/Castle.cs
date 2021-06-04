using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoblinRush;

public class Castle : MonoBehaviour
{
    Health m_CastleHealth;

    private void Start()
    {
        GameManager.Instance.CastleTarget = gameObject;
        m_CastleHealth = this.GetComponent<Health>();
    }

    private void Update()
    {
        if (m_CastleHealth.currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
