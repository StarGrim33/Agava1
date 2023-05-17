using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GateSpawner : MonoBehaviour
{
    [SerializeField] private Gate _gatePrefab;
    [SerializeField] private SpawnPointParts _spawnPointParts;

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
        var randomPosition = GetRandomPosition();
        _currentGate = Instantiate(_gatePrefab, randomPosition.position, Quaternion.identity);
        _currentGate.transform.rotation = DefineRotation(randomPosition);
        _currentGate.OnGoalScored += OnGoalScored;
        OnGoalGateSpawned?.Invoke(_currentGate);
        Vector3 gatePosition = _currentGate.MiddleTarget();
        OnGateSpawned?.Invoke(gatePosition);
    }

    private Quaternion DefineRotation(Transform spawnPoint)
    {
        if (_spawnPointParts.GetLeftSideSpawnPoints().Contains(spawnPoint))
        {
            return Quaternion.Euler(-90f, -90f, -90f); 
        }
        else if (_spawnPointParts.GetRightSideSpawnPoints().Contains(spawnPoint))
        {
            return Quaternion.Euler(-90f, 0f, 0f); 
        }

        return Quaternion.identity;
    }

    private Transform GetRandomPosition()
    {
        float randomValue = UnityEngine.Random.value;

        if (randomValue < 0.5f)
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
