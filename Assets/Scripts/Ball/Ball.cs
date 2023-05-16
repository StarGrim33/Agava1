using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private float _maxX; 
    [SerializeField] private float _minX; 
    [SerializeField] private float _maxZ; 
    [SerializeField] private float _minZ; 

    public void StopMoving()
    {
        //float clampedX = Mathf.Clamp(transform.position.x, _minX, _maxX);

        //float clampedZ = Mathf.Clamp(transform.position.z, _minZ, _maxZ);

        //transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
        //transform.position = _targetPosition.position;
        _rigidbody.velocity = Vector3.zero;
    }
}
