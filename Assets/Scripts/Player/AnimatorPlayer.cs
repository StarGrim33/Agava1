using System;
using UnityEngine;

public class AnimatorPlayer : MonoBehaviour
{
    public event Action OnKickedAnimationFinished;

    public void AnimationEvent()
    {
        OnKickedAnimationFinished?.Invoke();
    }
}
