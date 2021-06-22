using GoblinRush;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHUD : MonoBehaviour
{
    [Header("Turret HUD")]
    [SerializeField] private GameObject m_CanvasTurretHUD;
    [SerializeField] private bool _HUDVisibility;
    public bool HUDVisibilty { get; private set; }

    [SerializeField] private SpriteRenderer m_UpgradeArrow;

    public Turret m_Turret { private get; set; }
    
    void Awake()
    {
        //set visibility
        ChangeHUDVisibility(_HUDVisibility);
    }

    /// <summary>
    /// Change HUD turret visibility
    /// </summary>
    /// <param name="newVisibility"></param>
    public void ChangeHUDVisibility(bool newVisibility)
    {
        m_CanvasTurretHUD.gameObject.SetActive(newVisibility);
        _HUDVisibility = HUDVisibilty = newVisibility;
    }

    void Update()
    {
        //if HUD turret visible look at camera
        if (HUDVisibilty) m_CanvasTurretHUD.transform.LookAt(Camera.main.transform.position, Vector3.up);

        if (GameManager.Instance.currentMoney < m_Turret.getNextLevelTurretMoneyCost())
            m_UpgradeArrow.color = new Color(0, 0, 0);
        else
            m_UpgradeArrow.color = new Color(255, 255, 255);
    }

    public void DeleteUpgrade()
    {
        Destroy(m_UpgradeArrow.gameObject);
    }
}
