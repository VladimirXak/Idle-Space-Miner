using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.Data
{
    [Serializable]
    public struct CurrencySavedData
    {
        public ScientificNotationData Coins;
        public ScientificNotationData Grols;
    }
}
