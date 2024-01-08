using System.Collections;
using UnityEngine;

public class DeceptiveGoal : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    private void Start()
    {
        StartCoroutine(LifeCycle());
    }

    private IEnumerator LifeCycle()
    {
        var waitForSeconds = new WaitForSeconds(_lifeTime);

        yield return waitForSeconds;

        Destroy(gameObject);
    }
}
