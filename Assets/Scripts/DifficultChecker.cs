using UnityEngine;

public class DifficultChecker : MonoBehaviour
{
    private const string LevelsUnlocked = "_levelsUnlocked";

    [SerializeField] private EnemyKickingBall _enemy;

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
        int unlockedLevels = PlayerPrefs.GetInt(LevelsUnlocked, 0);
        return unlockedLevels >= 12;
    }
}
