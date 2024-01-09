using System.Collections;
using UnityEngine;

namespace Core
{
    public class DeceptiveGoal : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;
        private WaitForSeconds _waitForSeconds;

        private void Start()
        {
            _waitForSeconds = new WaitForSeconds(_lifeTime);
            StartCoroutine(LifeCycle());
        }

        private IEnumerator LifeCycle()
        {
            yield return _waitForSeconds;
            Destroy(gameObject);
        }
    }
}
