using System;
using System.Collections;
using UnityEngine;

namespace Core
{
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

        private WaitForSeconds _waitForSpawnDelay;
        private WaitForSeconds _waitForSpawnDelayAfterFalseGate;

        public event Action<Gate> OnGoalGateSpawned;

        public event Action<Vector3> OnGateSpawned;

        private void Start()
        {
            _waitForSpawnDelay = new WaitForSeconds(_spawnDelay);
            _waitForSpawnDelayAfterFalseGate = new WaitForSeconds(_spawnDelayAfterFalseGate);
            SpawnGate();
        }

        public void OnGoalScored(Gate gate)
        {
            gate.OnPlayerGoalScored -= OnGoalScored;
            _currentGate = null;
            SpawnGate();
        }

        private void SpawnGate()
        {
            StartCoroutine(Spawner());
        }

        private IEnumerator Spawner()
        {
            yield return _waitForSpawnDelay;
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
            _currentFalseGate = Instantiate(_falseGatePrefab, spawnPosition.position, Quaternion.identity);
            yield return _waitForSpawnDelayAfterFalseGate;
            StartCoroutine(Spawner());
        }

        private IEnumerator SpawnTrueGate(Transform spawnPosition)
        {
            yield return _waitForSpawnDelay;
            _currentGate = Instantiate(_gatePrefab, spawnPosition.position, Quaternion.identity);
            _currentGate.transform.rotation = GetGateRotationFromSpawnPosition(spawnPosition);
            _currentGate.OnPlayerGoalScored += OnGoalScored;
            OnGoalGateSpawned?.Invoke(_currentGate);
            Vector3 gatePosition = _currentGate.DetermineMiddlePosition();
            OnGateSpawned?.Invoke(gatePosition);
        }

        private Quaternion GetGateRotationFromSpawnPosition(Transform spawnPoint)
        {
            float angle = -90f;

            if (_spawnPointParts.LeftSideSpawnPoints.Contains(spawnPoint))
            {
                return Quaternion.Euler(angle, angle, angle);
            }
            else if (_spawnPointParts.RightSideSpawnPoints.Contains(spawnPoint))
            {
                return Quaternion.Euler(angle, 0f, 0f);
            }

            return Quaternion.identity;
        }

        private Transform GetSpawnPoint(GateSides side)
        {
            int randomPosition;

            if (side == GateSides.Left)
            {
                randomPosition = UnityEngine.Random.Range(0, _spawnPointParts.LeftSideCount);
                return _spawnPointParts.GetRandomSpawnPoint(randomPosition);
            }
            else
            {
                randomPosition = UnityEngine.Random.Range(0, _spawnPointParts.RightSideCount);
                return _spawnPointParts.GetRandomSpawnPoint(randomPosition);
            }
        }

        private Transform GetRandomPosition()
        {
            GateSides side = UnityEngine.Random.value < 0.5f ? GateSides.Left : GateSides.Right;
            return GetSpawnPoint(side);
        }

        private bool IsGateFalse()
        {
            float randomValue = UnityEngine.Random.value;
            float chance = 0.3f;

            return randomValue < chance && _isFalseGateEnabled;
        }
    }
}
