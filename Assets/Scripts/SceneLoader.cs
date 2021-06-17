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
    private Animator m_animator;

    [SerializeField]
    private NotifyAnimationEnd m_animationState;

    private void Start()
    {
        LevelManager.Instance.m_sceneLoader = this;
    }

    public IEnumerator LoadLevel(int level, Action toExecute = null)
    {
        m_animator.SetTrigger("Start");

        while(!m_animationState.AnimationEnded) yield return null; //On attend que l'animation finisse

        SceneManager.LoadScene(level);
        if (toExecute != null) toExecute();
    }


}
