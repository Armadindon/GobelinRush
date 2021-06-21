using System;
using System.Collections;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using GoblinRush;
using System.IO;
using System.Collections.Generic;

class SaveManager : Singleton<SaveManager>
{

    public void SaveLevel(string SaveName)
    {
        ensureDirectoryExist();
        //On construit l'objet de sauvegarde
        SaveData data = new SaveData();
        data.CurrentLevel = LevelManager.Instance.CurrentLevel;
        data.CurrentWave = GameManager.Instance.m_House.GetComponent<House>().CurrentWave;

        Turret[] turrets = FindObjectsOfType<Turret>();
        List<SaveTurret> turretsToSave = new List<SaveTurret>();
        foreach (Turret t in turrets)
        {
            SaveTurret ts = new SaveTurret();
            ts.Level = t.actualLevel;
            Vector3 pos = t.gameObject.transform.position;
            ts.Position = new float[3] { pos.x, pos.y, pos.z };
            turretsToSave.Add(ts);
        }
        data.m_Turrets = turretsToSave.ToArray();

        data.Remaining_health = GameManager.Instance.CastleTarget.GetComponent<Health>().currentHealth;
        data.currentMoney = GameManager.Instance.currentMoney;

        //On serialize l'objet
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        String path = Application.persistentDataPath + "/saves/" + SaveName + ".gblr";

        Debug.Log("Sauvegarde ici : " + path);

        FileStream fileStream = new FileStream(path, FileMode.Create);
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public SaveData LoadSave(String name)
    {
        ensureDirectoryExist();
        if (File.Exists(name))
        {
            // On récupère la sauvegarde
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fs = new FileStream(name, FileMode.Open);
            SaveData save = binaryFormatter.Deserialize(fs) as SaveData;
            fs.Close();
            return save;
        }
        return null;
    }

    public IEnumerator setDataAfterDelay(SaveData save)
    {
        yield return new WaitForFixedUpdate(); // Permet d'être sûr que la scene a bien load

        GameManager.Instance.m_House.GetComponent<House>().CurrentWave = save.CurrentWave;
        
        foreach(SaveTurret t in save.m_Turrets)
        {
            Vector3 pos = new Vector3(t.Position[0], t.Position[1], t.Position[2]);
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
        GameManager.Instance.currentMoney = save.currentMoney;

        GameManager.Instance.Play();
    }

    public List<SaveData> AvailableSaves()
    {
        List<SaveData> saves = new List<SaveData>();
        String[] files = Directory.GetFiles(Application.persistentDataPath + "/saves/", "*gblr");
        foreach(string file in files)
        {
            Debug.Log(file);
            SaveData data = LoadSave(file);
            data.FileName = Path.GetFileNameWithoutExtension(file);
            saves.Add(data);
        }
        return saves;
    }

    private void ensureDirectoryExist()
    {
        if(!Directory.Exists(Application.persistentDataPath + "/saves/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        }
    }

}

