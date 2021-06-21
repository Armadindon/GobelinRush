using System.Collections;
using System;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    // File Management
    public string FileName { get; set; }

    // Scene Management
    public int CurrentLevel { get; set; }

    // Wave Management
    public int CurrentWave { get; set; }

    // Turrets Management
    public SaveTurret[] m_Turrets { get; set; }

    // House Management
    public int Remaining_health { get; set; }


}
