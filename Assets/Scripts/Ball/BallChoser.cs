using System.Collections.Generic;
using UnityEngine;

public class BallChoser : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<PlayerBall> _balls;
    private int _currentBallIndex = 0;

    public void SetActiveBall()
    {
        _balls[_currentBallIndex].gameObject.SetActive(false); // ��������� ������� ���

        _currentBallIndex++; // ����������� ������ �������� ����

        if (_currentBallIndex >= _balls.Count)
            _currentBallIndex = 0; // ���� ������ ������� �� ������� ������, ��������� � ������� ����

        _balls[_currentBallIndex].gameObject.SetActive(true); // �������� ����� ���
    }
}
