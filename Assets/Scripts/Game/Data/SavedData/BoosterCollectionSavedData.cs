using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.Data
{
    [Serializable]
    public struct BoosterCollectionSavedData
    {
        public List<BoosterSavedData> Boosters;
    }

    [Serializable]
    public struct BoosterSavedData
    {
        public string Type;
        public long EndTime;
    }
}
