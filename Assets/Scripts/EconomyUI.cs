using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoblinRush;

public class EconomyUI : MonoBehaviour
{

    private Text m_moneyText;

    private void Awake()
    {
        m_moneyText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_moneyText.text = "" + GameManager.Instance.currentMoney;
    }
}
