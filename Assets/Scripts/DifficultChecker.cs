using UnityEngine;

public class DifficultChecker : MonoBehaviour
{
    [SerializeField] private EnemyKickingBall _enemy;

    private int _lastLevel = 12;

    private void Start()
    {
        if (CheckLastLevelReached())
            ChangeEnemyAcurracy();
    }

    private void ChangeEnemyAcurracy()
    {
        float missProbability = 0.1f;
        _enemy.ChangeMissProbability(missProbability);
    }

    private bool CheckLastLevelReached()
    {
        int unlockedLevels = PlayerPrefs.GetInt(Constants.LevelsUnlocked, 0);
        return unlockedLevels >= _lastLevel;
    }
}
