using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyAnimationEnd : MonoBehaviour
{
    public bool AnimationEnded { get; private set; }

    public void FinishedTransition()
    {
        AnimationEnded = true;
    }
}
