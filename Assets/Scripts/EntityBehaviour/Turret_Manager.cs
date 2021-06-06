using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Turret_Manager : MonoBehaviour
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
            RaycastHit hitPlacementZone = hits.FirstOrDefault(hit => hit.transform.name == "Placement_Zone" );

            if (hitPlacementZone.transform != null)
            {
                //récupère l'objet Turret_PLacement         
                GameObject m_TurretPlacement = hitPlacementZone.transform.parent.gameObject;
                //Create newTurret
                GameObject m_newTurretTurret = Instantiate(m_turretPrefab, m_TurretPlacement.transform.position, Quaternion.identity);
                //Destroy Turret placement zone
                Destroy(hitPlacementZone.transform.gameObject);
            }

            //get first turret hit 
            RaycastHit hitTurret = hits.FirstOrDefault(hit => hit.collider.name.Contains("Hitbox") && hit.transform.name.Contains("Turret"));

            if (hitTurret.transform != null)
            {
                //get game object
                GameObject m_CannonTurret = hitTurret.transform.gameObject;
                //cast game object
                Cannon_Turret cannonTurret = (Cannon_Turret)m_CannonTurret.GetComponent(typeof(Cannon_Turret));
                //show visibility range
                cannonTurret.ChangeVisibilityRange(true);
            }
        }
    }   
}
