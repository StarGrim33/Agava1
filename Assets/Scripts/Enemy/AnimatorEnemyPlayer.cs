using System;
using UnityEngine;

namespace Enemy
{
    public class AnimatorEnemyPlayer : MonoBehaviour
    {
        public event Action OnKickedEnemyAnimation;

        public void AnimationEvent()
        {
            OnKickedEnemyAnimation.Invoke();
        }

        public static class States
        {
            public const string NinjaIdle = nameof(NinjaIdle);
            public const string Strike = nameof(Strike);
        }
    }
}
