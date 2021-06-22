using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : Singleton<AudioManager>
{
    /// <summary>
    /// Liste de sons
    /// </summary>
    public Sound[] sounds;

    void Awake()
    {
        //awake de l'objet mère pas appelé
        base.Awake();

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

    /// <summary>
    /// Va jouer le son avec le nom exact
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        //quand utiliser : FindObjectOfType<AudioManager>().Play("nom");

        //on va jouer le son en fonction du nom donné
        Sound snd = Array.Find(sounds, sound => sound.name == name);

        //si le son ne correspond pas, on ne le joue pas
        if (snd == null) return;

        snd.m_Source.Play();
    }

    /// <summary>
    /// Va mettre en pause le son avec le nom exact
    /// </summary>
    /// <param name="name"></param>
    public void Pause(string name)
    {
        //on va mettre en pause le son en fonction du nom donné
        Sound snd = Array.Find(sounds, sound => sound.name == name);

        //si le son ne correspond pas, on ne le met pas en pause
        if (snd == null) return;

        snd.m_Source.Pause();
    }   

    /// <summary>
    /// Va stopper le son avec le nom exact
    /// </summary>
    /// <param name="name"></param>
    public void Stop(string name)
    {
        //on va stopper le son en fonction du nom donné
        Sound snd = Array.Find(sounds, sound => sound.name == name);

        //si le son ne correspond pas, on ne le stoppe pas
        if (snd == null) return;

        snd.m_Source.Stop();
    }

    /// <summary>
    /// Va changer le pitch du son demandé en fonction du booléen d'entrée
    /// </summary>
    /// <param name="name"></param>
    /// <param name="lowerHigher"></param>
    public void ModifyPitch(string name, bool lowerHigher)
    {
        //on va chercher le son en fonction du nom donné
        Sound snd = Array.Find(sounds, sound => sound.name == name);

        //si le son ne correspond pas, on ne le joue pas
        if (snd == null) return;

        //change le pitch du son en fonction du booléen d'entrée
        if (lowerHigher)
            snd.m_Source.pitch += 0.5f;
        else
            snd.m_Source.pitch -= 0.5f;
    }

}
