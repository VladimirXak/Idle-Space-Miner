using HakoLibrary.Pooling;
using UnityEngine;
using Zenject;

namespace Game
{
    public class EnemySelector : MonoBehaviour
    {
        [SerializeField] private GroupItemPoolContainer _poolEnemy;
        [SerializeField] private GroupItemPoolContainer _poolBossesEnemy;

        private GameLevel _level;

        [Inject]
        private void Construct(GameLevel level)
        {
            _level = level;
        }

        public Enemy GetEnemy()
        {
            Enemy enemy = null;

            if (_level.Value % 25 == 0 && _level.Value > _level.MaxPassedValue)
                enemy = _poolBossesEnemy.GetItem<Enemy>();
            else
                enemy = _poolEnemy.GetItem<Enemy>();

            enemy.Init(transform);

            return enemy;
        }
    }
}
