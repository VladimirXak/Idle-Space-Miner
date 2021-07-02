using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameInfo
    {
        public Currency Currency { get; private set; }

        public GameLevel Level { get; private set; }
        public Armament Armament { get; private set; }

        public GameInfo(Currency currency, GameLevel level, Armament armament)
        {
            Currency = currency;

            Level = level;
            Armament = armament;

            Currency.Coins.Add(new ScientificNotation(1, 4));
        }
    }
}
