using System;
using UnityEngine;

public class AnimatorPlayer : MonoBehaviour
{    
    public event Action OnKickedAnimationFinished;

    public void AnimationEvent()
    {
        OnKickedAnimationFinished.Invoke();
    }

    public static class States
    {
        public const string Idle = nameof(Idle);
        public const string Strike = nameof(Strike);
    }
}
