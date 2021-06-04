using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public enum Side { Enemy, Friendly }

    [Header("Life parameters")]
    [SerializeField] int m_Life;
    [SerializeField] Side side;

    private int m_Amount;

    // Start is called before the first frame update
    void Start()
    {
        m_Amount = m_Life;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Amount <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int amountOfDamage)
    {
        m_Amount -= amountOfDamage;
    }
}
