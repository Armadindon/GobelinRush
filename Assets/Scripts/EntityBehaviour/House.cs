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
    private Wave[] m_Waves;

    private List<GameObject> m_InstanciatedEnemies = new List<GameObject>();

    public int CurrentWave { get; set; }
    private bool isWaveFinished = true;

    private float nextEnemy;
    private float nextWave;

    public float WaveStartedAt {get; set;}

    private void Start()
    {
        //On set up la première wave
        nextWave = Time.time + timeBetweenWaves;
        GameManager.Instance.m_House = this;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPlaying) return;
        //On vérifie que les gameobjects des ennemis sont toujours là (sinon c'est qu'ils sont dead)
        m_InstanciatedEnemies = m_InstanciatedEnemies.Where(enemy => enemy != null).ToList();

        //Si on a dépassé le délai, et que il reste des waves
        if(isWaveFinished && Time.time > nextWave && m_Waves.Length != CurrentWave)
        {
            WaveStartedAt = GameManager.Instance.ElapsedTime;
            isWaveFinished = false;
        }


        //On fait deux if pour que aux même update on puisse instancier le premier ennemis
        if (m_Waves.Length != CurrentWave && !isWaveFinished && Time.time > nextEnemy && !m_Waves[CurrentWave].isFinished())
        {
            m_InstanciatedEnemies.Add(
                Instantiate(
                    m_prefabByEnemy[(int)m_Waves[CurrentWave].getNextEnemy()], 
                    m_enemySpawnPoint.position, Quaternion.identity)
                );
            nextEnemy = Time.time + timeBetweenEnemy;
        }


        //Si la wave est finie et on a tué tous les mobs
        if (m_Waves.Length != CurrentWave && m_Waves[CurrentWave].isFinished() && m_InstanciatedEnemies.Count == 0)
        {
            isWaveFinished = true;
            nextWave = Time.time + timeBetweenWaves;
            CurrentWave++;
        }
    }

    public bool finished()
    {
        return CurrentWave == m_Waves.Length;
    }

}
