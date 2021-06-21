﻿using System;
using System.Collections;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using GoblinRush;
using System.IO;

class SaveManager : Singleton<SaveManager>
{

    public void SaveLevel(string SaveName)
    {
        //On construit l'objet de sauvegarde
        SaveData data = new SaveData();
        data.CurrentLevel = LevelManager.Instance.CurrentLevel;
        data.CurrentWave = GameManager.Instance.m_House.GetComponent<House>().CurrentWave;

        Turret[] turrets = FindObjectsOfType<Turret>();
        foreach (Turret t in turrets)
        {
            SaveTurret ts = new SaveTurret();
            ts.Level = t.actualLevel;
            Vector3 pos = t.gameObject.transform.position;
            ts.Position = new float[3] { pos.x, pos.y, pos.z };
        }

        data.Remaining_health = GameManager.Instance.CastleTarget.GetComponent<Health>().currentHealth;

        //On serialize l'objet
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        String path = Application.persistentDataPath + "/saves/" + SaveName + ".gblr";

        Debug.Log("Sauvegarde ici : " + path);

        FileStream fileStream = new FileStream(path, FileMode.Create);
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public void LoadSave(String name)
    {
        String path = Application.persistentDataPath + "/saves/" + name + ".gblr";
        if (File.Exists(path))
        {
            // On récupère la sauvegarde
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            SaveData save = binaryFormatter.Deserialize(fs) as SaveData;
            fs.Close();

            // On applique la sauvegarde
            LevelManager.Instance.LoadLevel(save.CurrentLevel, () =>
            {
                
            }
            );
        }
    }

    public IEnumerable setDataAfterDelay(SaveData save)
    {
        yield return new WaitForFixedUpdate(); // Permet d'être sûr que la scene a bien load

        GameManager.Instance.m_House.GetComponent<House>().CurrentWave = save.CurrentWave;
        
        foreach(SaveTurret t in save.m_Turrets)
        {
            Vector3 pos = new Vector3(t.Position[0], t.Position[1], t.Position[3]);
            RaycastHit[] hits = Physics.SphereCastAll(pos, 1f, Vector3.forward);
            foreach(RaycastHit hit in hits)
            {
                // On supprime les zones de placements à la même position
                if (hit.transform.gameObject.name.Contains("Placement_Zone"))
                {
                    Destroy(hit.transform.gameObject);
                }
            }

            //On instancie une tourelle à la même position
            GameObject g = Instantiate(GameManager.Instance.M_CrossbowTurretPrefab, pos, Quaternion.identity);
            g.GetComponent<Turret>().ChangeTurretLevel(t.Level);
        }

        GameManager.Instance.CastleTarget.GetComponent<Health>().currentHealth = save.Remaining_health;
    }

}

