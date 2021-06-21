using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaveMainMenuScroller : MonoBehaviour
{
    private float y_position;

    [Header("UI Elements")]
    [SerializeField] private RectTransform m_content;
    [SerializeField] private GameObject m_LevelButtonPrefab;

    void OnEnable()
    {
        //On reset la size et on retrouve
        foreach (Transform child in m_content)
        {
            Destroy(child.gameObject);
        }

        m_content.sizeDelta = new Vector2(0, 150);
        y_position = 150f;

        List<SaveData> saves = SaveManager.Instance.AvailableSaves();

        foreach(SaveData save in saves)
        {
            GameObject g = Instantiate(m_LevelButtonPrefab, m_content);
            g.transform.position -= new Vector3(0, y_position, 0);

            SaveButton sb = g.GetComponent<SaveButton>();
            sb.m_Save = save;

            y_position += 350;
            m_content.sizeDelta += new Vector2(0, 350);
        }
    }
}
