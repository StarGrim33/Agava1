using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class SpawnPointParts : MonoBehaviour
    {
        [SerializeField] private List<Transform> _leftSide;
        [SerializeField] private List<Transform> _rightSide;

        public int LeftSideCount => _leftSide.Count;
        public int RightSideCount => _rightSide.Count;

        public List<Transform> LeftSideSpawnPoints => _leftSide;

        public List<Transform> RightSideSpawnPoints => _rightSide;

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
    }
}
