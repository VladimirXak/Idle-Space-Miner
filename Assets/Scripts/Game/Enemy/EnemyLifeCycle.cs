using System.Collections;
using UnityEngine;
using Zenject;

namespace Game
{
    public class EnemyLifeCycle : MonoBehaviour
    {
        [SerializeField] private EnemyBuilder _enemyBuilder;

        private IHealth<ScientificNotation> _health;
        private GameLevel _level;
        private Enemy _enemy;

        [Inject]
        private void Construct(GameLevel level, IHealth<ScientificNotation> health)
        {
            _level = level;
            _health = health;

            _health.Died += LevelCompletion;
            _level.ValueChanged += CreateEnemy;
        }

        private void Start()
        {
            CreateNewEnemy(_level.Value);
        }

        private void LevelCompletion()
        {
            _enemy.Return();
            StartCoroutine(Wait());
        }

        private void CreateEnemy(int level)
        {
            CreateNewEnemy(level);
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSecondsRealtime(0.25f);

            _level.LevelCompletion();         
        }

        private void CreateNewEnemy(int level)
        {
            bool isBoss = (level % 25 == 0 && _level.Value > _level.MaxPassedValue);

            _enemy = _enemyBuilder.Build(level, isBoss);
        }

        private void OnDestroy()
        {
            _health.Died -= LevelCompletion;
            _level.ValueChanged -= CreateEnemy;
        }
    }
}
