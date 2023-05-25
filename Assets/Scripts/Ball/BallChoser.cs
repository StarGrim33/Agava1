using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallChoser : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<PlayerBall> _balls;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerKickingBall _playerKickingBall;
    [SerializeField] private CinemachineVirtualCamera _camera;

    private int _currentBallIndex = 0;

    public void SetActiveBall()
    {
        _balls[_currentBallIndex].gameObject.SetActive(false); 

        _currentBallIndex++; 

        if (_currentBallIndex >= _balls.Count)
            _currentBallIndex = 0; 

        _balls[_currentBallIndex].gameObject.SetActive(true);
        _playerMovement.SetBall(_balls[_currentBallIndex]);
        _playerKickingBall.SetBall(_balls[_currentBallIndex]);
        _balls[_currentBallIndex].StopMoving();
        ChangeCameraTargetToNewBall();
    }

    private void ChangeCameraTargetToNewBall()
    {
        _camera.LookAt = _balls[_currentBallIndex].transform;
        _camera.Follow = _balls[_currentBallIndex].transform;
    }
}
