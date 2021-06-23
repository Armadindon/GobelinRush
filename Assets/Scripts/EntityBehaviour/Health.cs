using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoblinRush;

public class Health : MonoBehaviour
{
    public enum Side { Enemy, Friendly }

    [Header("Life parameters")]
    [SerializeField] public int health;
    [SerializeField] private Side side;

    [Header("Particle :")]
    [Tooltip("Hit Particle")]
    [SerializeField] private GameObject m_HitParticlePrefab;
    [Tooltip("Death Particle")]
    [SerializeField] private GameObject m_DeathParticlePrefab;

    [Header("HUD Healthbar")]
    [Tooltip("Healthbar Foreground")]
    [SerializeField] private Image m_HealthbarForeground;

    private Canvas m_Healthbar;

    public int currentHealth { get; set; }


    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = health;
        //on récupère le canvas parent de l'image pour pouvoir le rendre visible/invisible
        m_Healthbar = m_HealthbarForeground.GetComponentInParent<Canvas>();
        m_Healthbar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_Healthbar.gameObject.SetActive(currentHealth < health);

        if (currentHealth > 0)
        {
            //on fait en sorte que le canvas pointe toujours à la caméra
            m_HealthbarForeground.rectTransform.parent.transform.LookAt(Camera.main.transform.position, Vector3.up);
            //on gère le remplissage de la barre de vie en fonction de son pourcentage
            float currentHealthPct = (float)currentHealth / (float)health;
            OnHealthPctChanged(currentHealthPct);
        }
    }

    /// <summary>
    /// Change le remplissage de la barre de vie de l'intité en fonction d'un pourcentage donné
    /// </summary>
    /// <param name="pct">Pourcentage du changement de vie</param>
    private void OnHealthPctChanged(float pct)
    {
        //on récupère l'image du foreground directement à partir du prefab
        m_HealthbarForeground.fillAmount = pct;
    }

    /// <summary>
    /// Lorsque les entités s'attaquent et/ou recoivent des dommages
    /// </summary>
    /// <param name="amountOfDamage">dommages perçus</param>
    public void TakeDamage(int amountOfDamage)
    {
        currentHealth -= amountOfDamage;
    }
    /// <summary>
    /// Lorsque les entités s'attaquent et/ou recoivent des dommages. Une particule apparait au point d'impact
    /// </summary>
    /// <param name="amountOfDamage">dommages perçus</param>
    /// <param name="HitPoint">Point de percussion</param>
    public void TakeDamage(int amountOfDamage, Vector3 HitPoint)
    {
        GameObject m_NewHitParticle = Instantiate(m_HitParticlePrefab, HitPoint, Quaternion.identity);
        Destroy(m_NewHitParticle, 1); //Le 1 permet de Delay la suppresion de la particule pour l'annimation
        TakeDamage(amountOfDamage);
    }

    /// <summary>
    /// Annimation de mort des entité
    /// </summary>
    public void DeathAnimation()
    {
        if (m_DeathParticlePrefab)
        {
            GameObject m_NewHitParticle = Instantiate(m_DeathParticlePrefab, transform.position, Quaternion.identity);
            Destroy(m_NewHitParticle, 1);
        }
    }
}
