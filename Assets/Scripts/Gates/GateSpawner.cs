using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GateSpawner : MonoBehaviour
{
    [SerializeField] private Gate _gatePrefab;
    [SerializeField] private List<Transform> _gatePositions;

    public event UnityAction<Gate> OnGoalGateSpawned;
    public event UnityAction<Vector3> OnGateSpawned;

    private Gate _currentGate;

    private void Start()
    {
        SpawnGate();
    }

    public void OnGoalScored(Gate gate, bool isEnemyGoal)
    {
        gate.OnGoalScored -= OnGoalScored;
        _currentGate = null;
        SpawnGate();
    }

    private void SpawnGate()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        var waitForSeconds = new WaitForSeconds(2f);
        yield return waitForSeconds;
        int randomPosition = Random.Range(0, _gatePositions.Count);
        Transform spawnPoint = _gatePositions[randomPosition];
        _currentGate = Instantiate(_gatePrefab, spawnPoint.position, Quaternion.Euler(-90f, 0f, 0f));
        _currentGate.OnGoalScored += OnGoalScored;
        OnGoalGateSpawned?.Invoke(_currentGate);
        Vector3 gatePosition = _currentGate.transform.position;
        OnGateSpawned?.Invoke(gatePosition);
    }
}
