using System.Collections.Generic;
using UnityEngine;

public class BallChoser : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<PlayerBall> _balls;
    private int _currentBallIndex = 0;

    public void SetActiveBall()
    {
        _balls[_currentBallIndex].gameObject.SetActive(false); // ¬ыключаем текущий м€ч

        _currentBallIndex++; // ”величиваем индекс текущего м€ча

        if (_currentBallIndex >= _balls.Count)
            _currentBallIndex = 0; // ≈сли индекс выходит за пределы списка, переходим к первому м€чу

        _balls[_currentBallIndex].gameObject.SetActive(true); // ¬ключаем новый м€ч
    }
}
