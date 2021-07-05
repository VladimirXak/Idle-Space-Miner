using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GrolMining : MonoBehaviour
    {
        private GameLevel _level;
        private ICurrency<ScientificNotation> _grols;

        private const int _standartCountMinig = 1;

        [Inject]
        private void Construct(GameLevel level, Currency coins)
        {
            _level = level;
            _grols = coins.Grols;
        }

        private void OnLevelCompleted(int level)
        {
            _grols.Add(_standartCountMinig);
        }

        private void OnEnable()
        {
            _level.Completed += OnLevelCompleted;
        }

        private void OnDisable()
        {
            _level.Completed -= OnLevelCompleted;
        }
    }
}
