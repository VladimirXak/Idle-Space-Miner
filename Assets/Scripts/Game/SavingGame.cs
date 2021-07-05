using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class SavingGame : MonoBehaviour
    {
        private GameInfo _gameInfo;

        [Inject]
        private void Construct(GameInfo gameInfo)
        {
            _gameInfo = gameInfo;
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
                OnApplicationQuit();
        }

        private void OnApplicationQuit()
        {
            _gameInfo.SaveData();
        }
    }
}
