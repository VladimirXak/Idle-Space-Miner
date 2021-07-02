using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;
using System;

namespace Game
{
    public class Weapon
    {
        public const double CoefUpgradeCost = 1.07;

        public event Action<Weapon> Upgraded;
        public event Action<ScientificNotation, ScientificNotation> DamageChanged;

        public WeaponConfig Config { get; private set; }

        public int Level { get; private set; }
        public ScientificNotation Damage { get; private set; }
        public ScientificNotation Price { get; private set; }

        public Weapon(WeaponSavedData savedData, WeaponConfig config)
        {
            Config = config;

            Level = savedData.Level;
            Price = config.PriceUpgrade;

            DamageRecalculation();

            if (Level > 0)
                Price *= ScientificNotation.Pow(CoefUpgradeCost, Level);
        }

        public void Upgrade(int value)
        {
            Level += value;
            Price *= ScientificNotation.Pow(CoefUpgradeCost, value);

            DamageRecalculation();

            Upgraded?.Invoke(this);
        }

        private void DamageRecalculation()
        {
            ScientificNotation previousDamage = Damage;

            Damage = Config.Damage * Level;

            DamageChanged?.Invoke(previousDamage, Damage);
        }

        public WeaponSavedData GetData()
        {
            return new WeaponSavedData()
            {
                Id = Config.Id,
                Level = Level,
            };
        }
    }
}
