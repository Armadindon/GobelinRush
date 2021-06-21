using UnityEngine;
using UnityEngine.Audio;
using System;

[Serializable]
public class Sound
{
    //nom du fichier audio
    public string name;

    //fichier audio
    [Tooltip("Fichier audio")]
    public AudioClip m_Clip;
    //fichier source
    [HideInInspector]
    public AudioSource m_Source;

    //volume et pitch du son
    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    //permets de savoir si l'audio doit boucler ou non
    public bool loop;
}