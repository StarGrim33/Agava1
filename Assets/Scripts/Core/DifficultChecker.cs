using Enemy;
using UnityEngine;
using Utils;

namespace Core
{
    public class DifficultChecker : MonoBehaviour
    {
        private readonly int _lastLevel = 12;
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
            int unlockedLevels = PlayerPrefs.GetInt(Constants.LevelsUnlocked, 0);
            return unlockedLevels >= _lastLevel;
        }
    }
}
