using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoblinRush;

public class Castle : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.castleTarget = gameObject;
        Debug.Log("La position du castle a été enregistrée c: !");
    }
}
