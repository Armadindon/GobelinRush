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
    commonGoblinLvl1,
    commonGoblinLvl2,
    commonGoblinLvl3,
    speedGoblinLvl1,
    speedGoblinLvl2,
    speedGoblinLvl3,
    strongGoblinLvl1,
    strongGoblinLvl2,
    strongGoblinLvl3,
    healthyGoblinLvl1,
    healthyGoblinLvl2,
    healthyGoblinLvl3
}