using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        //si l'instance n'a pas été appelée avant
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(this);
            return;
        }
            
        //on détruit l'objet pour éviter des doublons
        DontDestroyOnLoad(gameObject);

        //on ajoute les fichiers audios aux fichiers sources
        foreach(Sound snd in sounds)
        {
            snd.m_Source = gameObject.AddComponent<AudioSource>();
            snd.m_Source.clip = snd.m_Clip;

            snd.m_Source.volume = snd.volume;
            snd.m_Source.pitch = snd.pitch;
            snd.m_Source.loop = snd.loop;
        }
    }

    public void Play(string name)
    {
        //quand utiliser : FindObjectOfType<AudioManager>().Play("nom");

        //on va jouer le son en fonction du nom donné
        Sound snd = Array.Find(sounds, sound => sound.name == name);

        //si le son ne correspond pas, on ne le joue pas
        if (snd == null) return;

        snd.m_Source.Play();
    }

    public void ModifyPitch(string name, bool lowerHigher)
    {
        //on va jouer le son en fonction du nom donné
        Sound snd = Array.Find(sounds, sound => sound.name == name);

        //si le son ne correspond pas, on ne le joue pas
        if (snd == null) return;

        if (lowerHigher)
            snd.m_Source.pitch += 0.5f;
        else
            snd.m_Source.pitch -= 0.5f;
    }

}
