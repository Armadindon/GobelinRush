using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Wave
{
    [SerializeField]
    private EnemyType[] m_wave;

    private int currentEnemy;

    public EnemyType getNextEnemy()
    {
        return m_wave[currentEnemy++];
    }

    public bool isFinished()
    {
        return currentEnemy == m_wave.Length;
    }

}

public enum EnemyType
{
    BASIC,
}