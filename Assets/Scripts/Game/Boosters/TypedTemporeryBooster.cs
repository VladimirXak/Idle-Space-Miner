using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;

namespace Game
{
    public class TypedTemporeryBooster : TemporeryBooster
    {
        public BoosterType Type { get; private set; }

        public TypedTemporeryBooster(BoosterType type, int multiplier, TimeSpan time) : base(multiplier, time)
        {
            Type = type;
        }

        public BoosterSavedData GetData()
        {
            return new BoosterSavedData()
            {
                Type = Type.ToString(),
                EndTime = EndTime.ToFileTime(),
            };
        }
    }
}
