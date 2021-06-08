using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHUD : MonoBehaviour
{
    [Header("Turret HUD")]
    [SerializeField]
    private bool _HUDVisibility;

    private Canvas m_CanvasTurretHUD;
    public bool HUDVisibilty { get; private set; }

    public Cannon_Turret m_CannonTurret { private get; set; }

    void Awake()
    {
        //get canvas
        m_CanvasTurretHUD = gameObject.GetComponentInChildren<Canvas>();
        //set visibility
        ChangeVisibility(_HUDVisibility);
    }

    /// <summary>
    /// Change HUD turret visibility
    /// </summary>
    /// <param name="newVisibility"></param>
    public void ChangeVisibility(bool newVisibility)
    {
        m_CanvasTurretHUD.gameObject.SetActive(newVisibility);
        HUDVisibilty = newVisibility;
    }

    void Update()
    {
        //if HUD turret visible look at camera
        if (HUDVisibilty) m_CanvasTurretHUD.transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

}
