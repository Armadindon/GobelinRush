using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GoblinRush;


public class House : MonoBehaviour
{
    [SerializeField]
    private Transform m_enemySpawnPoint;
    [SerializeField]
    private List<GameObject> m_prefabByEnemy; //On se base sur l'index de l'enum pour choisir un GameObject
    [SerializeField]
    private float timeBetweenWaves;
    [SerializeField]
    private float timeBetweenEnemy;
    [SerializeField]
    private Wave[] m_waves;

    private List<GameObject> m_InstanciatedEnemies = new List<GameObject>();

    private int currentWave = 0;
    private bool isWaveFinished = true;

    private float nextEnemy;
    private float nextWave;

    private void Start()
    {
        //On set up la première wave
        nextWave = Time.time + timeBetweenWaves;
        GameManager.Instance.m_house = this;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPlaying) return;
        //On vérifie que les gameobjects des ennemis sont toujours là (sinon c'est qu'ils sont dead)
        m_InstanciatedEnemies = m_InstanciatedEnemies.Where(enemy => enemy != null).ToList();

        //Si on a dépassé le délai, et que il reste des waves
        if(isWaveFinished && Time.time > nextWave && m_waves.Length != currentWave)
        {
            isWaveFinished = false;
        }


        //On fait deux if pour que aux même update on puisse instancier le premier ennemis
        if (m_waves.Length != currentWave && !isWaveFinished && Time.time > nextEnemy && !m_waves[currentWave].isFinished())
        {
            m_InstanciatedEnemies.Add(
                Instantiate(
                    m_prefabByEnemy[(int)m_waves[currentWave].getNextEnemy()], 
                    m_enemySpawnPoint.position, Quaternion.identity)
                );
            nextEnemy = Time.time + timeBetweenEnemy;
        }


        //Si la wave est finie et on a tué tous les mobs
        if (m_waves.Length != currentWave && m_waves[currentWave].isFinished() && m_InstanciatedEnemies.Count == 0)
        {
            isWaveFinished = true;
            nextWave = Time.time + timeBetweenWaves;
            currentWave++;
        }
    }

    public bool finished()
    {
        return currentWave == m_waves.Length;
    }

}
