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

        public event Action<Gate> OnGoalGateSpawned;

        public event Action<Vector3> OnGateSpawned;

        private void Start() => SpawnGate();

        public void OnGoalScored(Gate gate)
        {
            gate.OnPlayerGoalScored -= OnGoalScored;
            _currentGate = null;
            SpawnGate();
        }

        private void SpawnGate() => StartCoroutine(Spawner());

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
            _currentGate.OnPlayerGoalScored += OnGoalScored;
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

            if (randomValue < chance && _isFalseGateEnabled)
                return true;

            return false;
        }
    }
}
