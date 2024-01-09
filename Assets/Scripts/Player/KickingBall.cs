using System.Collections;
using UnityEngine;
using Utils;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public abstract class KickingBall : MonoBehaviour
    {
        [SerializeField] protected Rigidbody BallRigidbody;
        [SerializeField] protected Transform BallSpawnPoint;
        [SerializeField] protected float HifForce = 10.0f;
        [SerializeField] protected GameObject FooballPlayer;
        [SerializeField] protected ParticleSystem ParticleSystem;
        [SerializeField] protected int HitsRemained;

        protected Animator Animator;
        protected Vector3 HitDirection = Vector3.forward;
        protected float AngleRotation = 7.0f;
        protected float TimeHitsReload = 3f;

        protected virtual void Start()
        {
            Animator = GetComponent<Animator>();
        }

        protected virtual void OnKickedAnimationFinished()
        {
            ParticleSystem.Play();
            BallRigidbody.AddForce(HitDirection * HifForce, ForceMode.Impulse);

            if (HitsRemained > 0)
                HitsRemained--;

            if (HitsRemained <= 0)
                StartCoroutine(ReloadHits());

            Time.timeScale = 1f;
        }

        protected virtual IEnumerator ReloadHits()
        {
            var waitForSeconds = new WaitForSeconds(TimeHitsReload);

            if (HitsRemained <= 0)
                yield return waitForSeconds;
        }

        protected virtual IEnumerator Kicking()
        {
            while (HitsRemained > 0)
                yield return null;

            if (HitsRemained <= 0)
                Animator.SetBool(Constants.IsAiming, false);
        }
    }
}
