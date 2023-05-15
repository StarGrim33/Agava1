using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Gate : MonoBehaviour
{
    public event UnityAction<Gate> OnGoalScored;

    [SerializeField] private ParticleSystem _particleSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ball>(out Ball ball))
        {
            Debug.Log("Goal");
            ScoreGoal();
            _particleSystem.Play();
            Destroy(gameObject, 1f);
        }
    }

    private void ScoreGoal()
    {
        OnGoalScored?.Invoke(this);
    }
}
