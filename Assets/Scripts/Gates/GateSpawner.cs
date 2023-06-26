using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GateSpawner : MonoBehaviour
{
    [SerializeField] private Gate _gatePrefab;
    [SerializeField] private SpawnPointParts _spawnPointParts;
    [SerializeField] private FalseGate _falseGatePrefab;
    [SerializeField] private bool _isFalseGateEnabled;

    public event UnityAction<Gate> OnGoalGateSpawned;
    public event UnityAction<Vector3> OnGateSpawned;

    private Gate _currentGate;
    private FalseGate _currentFalseGate;
    private float _spawnDelay = 2f;
    private float _spawnDelayAfterFalseGate = 5f;

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
            _currentFalseGate = Instantiate(_falseGatePrefab, randomPosition.position, Quaternion.identity);

            yield return new WaitForSeconds(_spawnDelayAfterFalseGate);

            StartCoroutine(Spawner());
        }
        else
        {
            _currentGate = Instantiate(_gatePrefab, randomPosition.position, Quaternion.identity);
            _currentGate.transform.rotation = DefineRotation(randomPosition);
            _currentGate.OnGoalScored += OnGoalScored;
            OnGoalGateSpawned?.Invoke(_currentGate);
            Vector3 gatePosition = _currentGate.MiddleTarget();
            OnGateSpawned?.Invoke(gatePosition);
        }
    }

    private Quaternion DefineRotation(Transform spawnPoint)
    {
        float angle = -90f;

        if (_spawnPointParts.GetLeftSideSpawnPoints().Contains(spawnPoint))
            return Quaternion.Euler(angle, angle, angle);
        else if (_spawnPointParts.GetRightSideSpawnPoints().Contains(spawnPoint))
            return Quaternion.Euler(angle, 0f, 0f);

        return Quaternion.identity;
    }

    private Transform GetRandomPosition()
    {
        float randomValue = UnityEngine.Random.value;
        float chance = 0.5f;

        if (randomValue < chance)
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

    [System.Serializable]
    public class SpawnPointParts
    {
        [SerializeField] private List<Transform> _leftSide;
        [SerializeField] private List<Transform> _rightSide;

        public int LeftSideCount => _leftSide.Count;
        public int RightSideCount => _rightSide.Count;

        public Transform GetRandomSpawnPoint(int index)
        {
            if (index < _leftSide.Count)
            {
                return _leftSide[index];
            }
            else
            {
                index -= _leftSide.Count;
                return _rightSide[index];
            }
        }

        public List<Transform> GetLeftSideSpawnPoints()
        {
            return _leftSide;
        }

        public List<Transform> GetRightSideSpawnPoints()
        {
            return _rightSide;
        }
    }
}
