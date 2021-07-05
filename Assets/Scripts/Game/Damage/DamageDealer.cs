using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public class DamageDealer : MonoBehaviour, IValue<ScientificNotation>
    {
        public event Action<ScientificNotation> ValueChanged;

        private ScientificNotation _value;
        public ScientificNotation Value
        {
            get => _value;
            private set
            {
                _value = value;
                ValueChanged?.Invoke(_value);
            }
        }

        private Armament _armament;

        [Inject]
        private void Construct(Armament armament)
        {
            _armament = armament;

            foreach (var weapon in _armament)
            {
                Value += weapon.Damage;

                weapon.DamageChanged += OnWeaponDamageChanged;
            }
        }

        private void OnWeaponDamageChanged(ScientificNotation previousDamage, ScientificNotation newDamage)
        {
            Value += newDamage - previousDamage;
        }

        private void OnDestroy()
        {
            foreach (var weapon in _armament)
                weapon.DamageChanged -= OnWeaponDamageChanged;
        }
    }
}
