using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public enum Side { Enemy, Friendly }

    [Header("Life parameters")]
    [SerializeField] private int health;
    [SerializeField] private Side side;

    [Header("HUD Healthbar")]
    [Tooltip("Healthbar Foreground")]
    [SerializeField] private Image m_HealthbarForeground;
    //[Tooltip("Camera")]
    //[SerializeField] private Camera m_CameraBase;

    public int currentHealth { get; set; }

    //public event Action<float> OnHealthPctChanged = delegate { };

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > 0)
        {
            //on fait en sorte que le canvas pointe toujours à la caméra
            m_HealthbarForeground.rectTransform.parent.transform.LookAt(Camera.main.transform.position, Vector3.up);
            //on gère le remplissage de la barre de vie en fonction de son pourcentage
            float currentHealthPct = (float)currentHealth / (float)health;
            OnHealthPctChanged(currentHealthPct);
        }
    }

    private void OnHealthPctChanged(float pct)
    {
        //on récupère l'image du foreground directement à partir du prefab
        m_HealthbarForeground.fillAmount = pct;
    }

    public void TakeDamage(int amountOfDamage)
    {
        currentHealth -= amountOfDamage;
    }
}
