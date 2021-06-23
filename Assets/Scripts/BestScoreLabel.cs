using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreLabel : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = "Highest Score : " + PlayerPrefs.GetInt("BestScore", 0);
    }
}
