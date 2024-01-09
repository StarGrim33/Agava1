using UnityEngine;

namespace Utils
{
    public class PlayerBoundaryChecker : MonoBehaviour
    {
        private readonly float _maxXBorder = 9.4f;
        private readonly float _minXBorder = -19.23f;
        private readonly float _maxZBorder = 16.81f;
        private readonly float _minZBorder = -4.83f;

        private void Update()
        {
            CheckBorders();
        }

        private void CheckBorders()
        {
            if (transform.position.x > _maxXBorder || transform.position.x < _minXBorder
                        || transform.position.z > _maxZBorder || transform.position.z < _minZBorder)
            {
                transform.position = new Vector3(0, 0, 0);
            }
        }
    }
}

