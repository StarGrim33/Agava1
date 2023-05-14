using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    public event UnityAction OnGoalScored;

    [SerializeField] private ParticleSystem _particleSystem;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Ball>(out Ball ball))
        {
            Debug.Log("Goal");
            ScoreGoal();
            _particleSystem.Play();
        }
    }

    private void ScoreGoal()
    {
        OnGoalScored?.Invoke();
    }
}
