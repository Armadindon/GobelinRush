using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoblinRush;

public class WaveLabel : MonoBehaviour
{
    void Update()
    {
        GetComponent<Text>().text = "Current Wave : " + (GameManager.Instance.m_House.CurrentWave + 1) + " / " + GameManager.Instance.m_House.NbWaves;
    }
}
