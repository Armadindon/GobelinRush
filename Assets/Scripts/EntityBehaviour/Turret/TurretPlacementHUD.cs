using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoblinRush;

public class TurretPlacementHUD : MonoBehaviour
{
    [Header("Turret Placement HUD")]
    [SerializeField] private GameObject m_CanvasTurretPlacementHUD;
    [SerializeField] private bool _HUDVisibility;

    [Header("Turret creation")]
    [SerializeField] private List<SpriteRenderer> _keysAssoctiatedImageHUD;
    [SerializeField] private List<GameObject> _valuesAssoctiatedTurret;

    //Unity doesn't know how to serialize a Dictionary
    public Dictionary<SpriteRenderer, GameObject> _createDictionary = new Dictionary<SpriteRenderer, GameObject>();

    public bool HUDVisibilty { get; private set; }

    void Awake()
    {
        //empty dictionnary
        _createDictionary.Clear();
        //fil dictionnary with _keysAssoctiatedImageHUD, _valuesAssoctiatedTurret
        for (int i = 0; i != Math.Min(_keysAssoctiatedImageHUD.Count, _valuesAssoctiatedTurret.Count); i++)
            _createDictionary.Add(_keysAssoctiatedImageHUD[i], _valuesAssoctiatedTurret[i]);

        //set visibility
        ChangeHUDVisibility(_HUDVisibility);
        m_CanvasTurretPlacementHUD.transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
   
    private void OnValidate()
    {
        if (!Application.isPlaying) return;
        //set visibility
        ChangeHUDVisibility(_HUDVisibility);
    }

    /// <summary>
    /// Change HUD turret visibility
    /// </summary>
    /// <param name="newVisibility"></param>
    public void ChangeHUDVisibility(bool newVisibility)
    {
        m_CanvasTurretPlacementHUD.gameObject.SetActive(newVisibility);
        _HUDVisibility = HUDVisibilty = newVisibility;
    }

    void Update()
    {
        //if HUD turret visible look at camera
        if (HUDVisibilty) m_CanvasTurretPlacementHUD.transform.LookAt(Camera.main.transform.position, Vector3.up);
        foreach (var item in _createDictionary)
        {
            if(GameManager.Instance.currentMoney < item.Value.GetComponent<Turret>().getTurretMoneyCost())
                item.Key.color = new Color(0, 0, 0);
            else
                item.Key.color = new Color(255, 255, 255);
        }
    }

    private void OnMouseExit()
    {
        ChangeHUDVisibility(false);
    }

    public GameObject getCorrespTurret(SpriteRenderer sprite)
    {
        foreach (var item in _createDictionary)
        {
            if (item.Key != sprite) continue;
            return item.Value;
        }
        return null;
    }
}
