using UnityEngine;

namespace Game
{
    public class EnemyHealthAssigner
    {
        private const double _coefHealth = 1.055;
        private readonly ScientificNotation _startHealth = 50;

        private IHealth<ScientificNotation> _health;

        private ScientificNotation _currentHealth;
        private int _currentLevel;

        public EnemyHealthAssigner(IHealth<ScientificNotation> health)
        {
            _health = health;

            _currentLevel = 1;
            _currentHealth = _startHealth;
        }

        public void SetHealth(int level, bool isBoss)
        {
            int differentLevel = level - _currentLevel;

            if (differentLevel == 0)
            {
                _health.SetHealth(_currentHealth);
                return;
            }

            _currentLevel = level;

            ScientificNotation differentHealth = ScientificNotation.Pow(_coefHealth, Mathf.Abs(differentLevel));

            if (differentLevel > 0)
                _currentHealth *= differentHealth;
            else
                _currentHealth /= differentHealth;

            ScientificNotation newHealth = _currentHealth;

            int multiplier = ((level - 1) / 25) + 1;

            newHealth *= multiplier;

            if (isBoss)
                newHealth *= 3;

            _health.SetHealth(newHealth);
        }
    }
}
