using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoblinRush;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField]
    private Animator m_Animator;

    [SerializeField]
    private NotifyAnimationEnd m_AnimationState;

    private void Start()
    {
        LevelManager.Instance.m_SceneLoader = this;
    }

    public IEnumerator LoadLevel(int level, Action toExecute = null)
    {
        if(level != SceneManager.GetActiveScene().buildIndex)
        {
            m_Animator.SetTrigger("Start");

            while (!m_AnimationState.AnimationEnded) yield return null; //On attend que l'animation finisse

            SceneManager.LoadScene(level);
        }

        if (toExecute != null) toExecute();
    }


}
