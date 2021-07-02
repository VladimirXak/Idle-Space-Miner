using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public class TapDamageDealer : MonoBehaviour, IValue<ScientificNotation>
    {
        [SerializeField] private TapOnField _tapOnField;
        [SerializeField] private DamageDealer _damageDealer;

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

        private IHealth<ScientificNotation> _enemyHealth;

        [Inject]
        private void Construct(IHealth<ScientificNotation> enemyHealth)
        {
            _enemyHealth = enemyHealth;
            _damageDealer.ValueChanged += DamageRecalculation;

            DamageRecalculation(_damageDealer.Value);
        }

        private void DamageRecalculation(ScientificNotation value)
        {
            Value = (value / 40) + 1;
        }

        private void ToDamage()
        {
            _enemyHealth.GetDamage(Value);
        }

        private void OnEnable()
        {
            _tapOnField.OnTap += ToDamage;
        }

        private void OnDisable()
        {
            _tapOnField.OnTap -= ToDamage;
        }

        private void OnDestroy()
        {
            _damageDealer.ValueChanged -= DamageRecalculation;
        }
    }
}
