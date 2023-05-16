using UnityEngine;
using UnityEngine.Events;

public class AnimatorEnemyPlayer : MonoBehaviour
{
    public event UnityAction OnKickedEnemyAnimation;

    public void AnimationEvent()
    {
        OnKickedEnemyAnimation.Invoke();
    }

    public static class Params
    {
        public const string IsAiming = ("isAiming");
    }

    public static class States
    {
        public const string NinjaIdle = nameof(NinjaIdle);
        public const string Strike = nameof(Strike);
    }

}
