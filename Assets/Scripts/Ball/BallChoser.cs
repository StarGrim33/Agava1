using Cinemachine;
using UnityEngine;

public class BallChoser : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerBall[] _balls;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerKickingBall _playerKickingBall;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private PlayerData _playerData;

    public int GetBallCount => _balls.Length;

    private int _currentBallIndex = 0;

    public void ChangeBall()
    {
        int nextBallIndex = (_currentBallIndex + 1) % _balls.Length;

        if (_balls[nextBallIndex].IsBuyed)
            ChangeToBall(nextBallIndex);
        else
            ChangeToBall(0);
    }

    private void ChangeCameraTargetToNewBall()
    {
        _camera.LookAt = _balls[_currentBallIndex].transform;
        _camera.Follow = _balls[_currentBallIndex].transform;
    }

    private void ChangeToBall(int ballIndex)
    {
        _balls[_currentBallIndex].gameObject.SetActive(false);
        _currentBallIndex = ballIndex;
        _balls[_currentBallIndex].gameObject.SetActive(true);
        _playerMovement.SetBall(_balls[_currentBallIndex]);
        _playerKickingBall.SetBall(_balls[_currentBallIndex]);
        _balls[_currentBallIndex].StopMoving();
        ChangeCameraTargetToNewBall();
    }
}
