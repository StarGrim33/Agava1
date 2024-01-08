using System;
using System.Collections;
using UnityEngine;

public class GateSpawner : MonoBehaviour
{
    [SerializeField] private Gate _gatePrefab;
    [SerializeField] private SpawnPointParts _spawnPointParts;
    [SerializeField] private DeceptiveGoal _falseGatePrefab;
    [SerializeField] private bool _isFalseGateEnabled;
    private Gate _currentGate;
    private DeceptiveGoal _currentFalseGate;
    private float _spawnDelay = 2f;
    private float _spawnDelayAfterFalseGate = 5f;

    public event Action<Gate> OnGoalGateSpawned;
    public event Action<Vector3> OnGateSpawned;

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
        var waitForSeconds = new WaitForSeconds(_spawnDelay);
        yield return waitForSeconds;
        var randomPosition = GetRandomPosition();

        if (IsGateFalse())
        {
            yield return SpawnFalseGate(randomPosition);
        }
        else
        {
            yield return SpawnTrueGate(randomPosition);
        }
    }

    private IEnumerator SpawnFalseGate(Transform spawnPosition)
    {
        var waitForSeconds = new WaitForSeconds(_spawnDelayAfterFalseGate);
        _currentFalseGate = Instantiate(_falseGatePrefab, spawnPosition.position, Quaternion.identity);

        yield return waitForSeconds;

        StartCoroutine(Spawner());
    }

    private IEnumerator SpawnTrueGate(Transform spawnPosition)
    {
        var waitForSeconds = new WaitForSeconds(_spawnDelay);
        yield return waitForSeconds;

        _currentGate = Instantiate(_gatePrefab, spawnPosition.position, Quaternion.identity);
        _currentGate.transform.rotation = SetGateRotationBySpawnPosition(spawnPosition);
        _currentGate.OnGoalScored += OnGoalScored;
        OnGoalGateSpawned?.Invoke(_currentGate);
        Vector3 gatePosition = _currentGate.DetermineMiddlePosition();
        OnGateSpawned?.Invoke(gatePosition);
    }

    private Quaternion SetGateRotationBySpawnPosition(Transform spawnPoint)
    {
        float angle = -90f;

        if (_spawnPointParts.LeftSideSpawnPoints.Contains(spawnPoint))
            return Quaternion.Euler(angle, angle, angle);
        else if (_spawnPointParts.RightSideSpawnPoints.Contains(spawnPoint))
            return Quaternion.Euler(angle, 0f, 0f);

        return Quaternion.identity;
    }

    private Transform GetRandomPosition()
    {
        float randomValue = UnityEngine.Random.value;
        float chance = 0.5f;

        if (randomValue < chance)
        {
            return GetSpawnPoint(GateSides.Left);
        }
        else
        {
            return GetSpawnPoint(GateSides.Right);
        }
    }

    private Transform GetSpawnPoint(GateSides side)
    {
        if(side == GateSides.Left)
        {
            int randomLeftSidePosition = UnityEngine.Random.Range(0, _spawnPointParts.LeftSideCount);
            Transform spawnPoint = _spawnPointParts.GetRandomSpawnPoint(randomLeftSidePosition);
            return spawnPoint;
        }
        else
        {
            int randomRightSidePosition = UnityEngine.Random.Range(0, _spawnPointParts.RightSideCount);
            Transform spawnPoint = _spawnPointParts.GetRandomSpawnPoint(randomRightSidePosition);
            return spawnPoint;
        }
    }

    private bool IsGateFalse()
    {
        float randomValue = UnityEngine.Random.value;
        float chance = 0.3f;

        if (randomValue < chance && _isFalseGateEnabled)
            return true;

        return false;
    }
}
