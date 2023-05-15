using System.Collections.Generic;
using UnityEngine;

public class GateSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _gatePrefab;
    [SerializeField] private List<Transform> _gatePositions;

    private GameObject _currentGate;

    private void Start()
    {
        //SpawnGate();
    }

    private void OnGoalScored()
    {
        Destroy(gameObject);
        SpawnGate();
    }

    private void SpawnGate()
    {
        int randomPosition = Random.Range(0, _gatePositions.Count);
        Transform spawnPoint = _gatePositions[randomPosition];
        _currentGate = Instantiate(_gatePrefab, spawnPoint.position, Quaternion.identity);
        _currentGate.GetComponentInChildren<Goal>().OnGoalScored += OnGoalScored;
    }
}
