using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public enum Side { Enemy, Friendly }

    [Header("Life parameters")]
    [SerializeField] 
    private int health;
    [SerializeField] 
    private Side side;

    public int currentHealth { get; set; }

    public event Action<float> OnHealthPctChanged = delegate { };

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
            float currentHealthPct = (float)currentHealth / (float)health;
            OnHealthPctChanged(currentHealthPct);
        }
    }

    public void TakeDamage(int amountOfDamage)
    {
        currentHealth -= amountOfDamage;
    }
}
