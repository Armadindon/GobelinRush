using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHUD : MonoBehaviour
{
    [Header("Turret HUD")]
    [SerializeField] private GameObject m_CanvasTurretHUD;
    [SerializeField] private bool _HUDVisibility;
    public bool HUDVisibilty { get; private set; }

    [SerializeField] private GameObject m_UpgradeArrow;
    [SerializeField] private bool _upgradeArrowVisibility;
    public bool upgradeArrowVisibility { get; private set; }

    public Cannon_Turret m_CannonTurret { private get; set; }
    
    void Awake()
    {
        //set visibility
        ChangeHUDVisibility(_HUDVisibility);
        ChangeUpgradeVisibility(_upgradeArrowVisibility);
    }
   
    private void OnValidate()
    {
        if (!Application.isPlaying) return;
        //set visibility
        ChangeHUDVisibility(_HUDVisibility);
        ChangeUpgradeVisibility(_upgradeArrowVisibility);
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

    /// <summary>
    /// Change UpgradeArrow visibility
    /// </summary>
    /// <param name="newVisibility"></param>
    public void ChangeUpgradeVisibility(bool newVisibility)
    {
        m_UpgradeArrow.SetActive(newVisibility);
        _upgradeArrowVisibility = upgradeArrowVisibility = newVisibility;
    }

    void Update()
    {
        //if HUD turret visible look at camera
        if (HUDVisibilty) m_CanvasTurretHUD.transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

}
