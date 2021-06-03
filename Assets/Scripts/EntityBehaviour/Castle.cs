using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoblinRush;

public class Castle : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.castleTarget = transform;
        Debug.Log("La position du castle a été enregistrée c: !");
    }
}
