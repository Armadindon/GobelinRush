using System;
using UnityEngine;
using UnityEngine.UI;
using GoblinRush;
using SDD.Events;

public class SaveButton : MonoBehaviour
{

    public SaveData m_Save { get; set; }

    [Header("Texts")]
    [SerializeField] private Text m_SaveName;
    [SerializeField] private Text m_DateSave;
    [SerializeField] private Text m_LevelName;

    // Update is called once per frame
    void Update()
    {
        if (m_Save != null)
        {
            m_SaveName.text = m_Save.FileName;
            m_DateSave.text = DateTime.ParseExact(m_Save.FileName.Replace("Save_",""), "yyyy_dd_MM_HH_mm_ss", null).ToString("g");
            m_LevelName.text = "Niveau " + (m_Save.CurrentLevel + 1);
        }
    }

    public void LoadLevelButtonClicked()
    {
        EventManager.Instance.Raise(new LoadSaveEvent(m_Save));
    }
}
