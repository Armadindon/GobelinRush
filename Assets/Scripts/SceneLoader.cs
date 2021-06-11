using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoblinRush;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField]
    private Animator m_animator;

    [SerializeField]
    private float animationDuration = 1.0f;

    private void Start()
    {
        LevelManager.Instance.m_sceneLoader = this;
    }

    public IEnumerator LoadLevel(int level)
    {
        m_animator.SetTrigger("Start");

        yield return new WaitForSeconds(animationDuration);

        SceneManager.LoadScene(level);
    }

}
