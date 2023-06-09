using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _maxXBorder = 9.4f;
    private float _minXBorder = -19.23f;
    private float _maxZBorder = 16.81f;
    private float _minZBorder = -4.83f;

    private void Update()
    {
        StartCoroutine(CheckBorders());
    }

    private IEnumerator CheckBorders()
    {
        if (transform.position.x > _maxXBorder || transform.position.x < _minXBorder ||
                    transform.position.z > _maxZBorder || transform.position.z < _minZBorder)
            transform.position = new Vector3(0, 0, 0);

        yield return null;
    }
}
