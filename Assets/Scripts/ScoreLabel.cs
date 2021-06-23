using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoblinRush;

public class ScoreLabel : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        gameObject.GetComponent<Text>().text = "Score : " + GameManager.Instance.Score;
    }
}
