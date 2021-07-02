using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class LevelDisplay : MonoBehaviour
    {
        [SerializeField] private IntValueDisplay _levelDisplay;

        [Inject]
        private void Construct(GameLevel level)
        {
            _levelDisplay.Init(level);
        }
    }
}
