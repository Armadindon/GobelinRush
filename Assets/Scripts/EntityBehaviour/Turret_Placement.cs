using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret_Placement : MonoBehaviour
{
    [Tooltip("Turret prefab")]
    [SerializeField] GameObject m_turretPrefab;

    void Update()
    {
        //on click
        if (Input.GetMouseButtonDown(0))
        {
            //create ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //get all hit by ray
            RaycastHit[] hits = Physics.RaycastAll(ray);
            //get first placement zone
            RaycastHit hitPlacementZone = hits.FirstOrDefault(hit => hit.transform.name.Contains("Placement_Zone"));

            if (hitPlacementZone.transform != null)
            {
                //récupère l'objet Turret_PLacement         
                GameObject m_TurretPlacement = hitPlacementZone.transform.parent.gameObject;
                //Create newTurret
                GameObject m_newTurretTurret = Instantiate(m_turretPrefab, m_TurretPlacement.transform.position, Quaternion.identity);
                //Destroy Turret placement zone
                Destroy(hitPlacementZone.transform.gameObject);
            }
        }
    }
}
