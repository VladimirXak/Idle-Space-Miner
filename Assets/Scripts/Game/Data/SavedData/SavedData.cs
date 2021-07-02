using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.Data
{
    [Serializable]
    public struct SavedData
    {
        public CurrencySavedData Currency;

        public GameLevelSavedData GameLevel;
        public ArmamentSavedData Armament;
    }
}
