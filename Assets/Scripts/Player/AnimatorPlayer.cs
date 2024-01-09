using System;
using UnityEngine;

namespace Player
{
    public class AnimatorPlayer : MonoBehaviour
    {
        public event Action OnKickedAnimationFinished;

        public void AnimationEvent()
        {
            OnKickedAnimationFinished?.Invoke();
        }
    }
}
