using UnityEngine;
using Game.UI;
using Zenject;

namespace Game
{
    public class EnemyBuilder : MonoBehaviour
    {
        [SerializeField] private ProgressBarHealth _progressBar;
        [SerializeField] private ScnValueDisplay _valueHealthDisplay;
        [SerializeField] private EnemySelector _enemySelector;

        private EnemyHealthAssigner _enemyHealthAssigner;

        [Inject]
        private void Construct(GameLevel level, IHealth<ScientificNotation> health)
        {
            _enemyHealthAssigner = new EnemyHealthAssigner(health);

            _progressBar.Init(health);
            _valueHealthDisplay.Init(health);
        }

        public Enemy Build(int level, bool isBoss)
        {
            _enemyHealthAssigner.SetHealth(level, isBoss);
            return _enemySelector.GetEnemy();
        }
    }
}
