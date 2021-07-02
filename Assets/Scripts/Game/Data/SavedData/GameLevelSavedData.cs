using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.Data
{
    [Serializable]
    public struct GameLevelSavedData
    {
        public int MaxPassedLevel;
        public int CurrentLevel;
    }
}
