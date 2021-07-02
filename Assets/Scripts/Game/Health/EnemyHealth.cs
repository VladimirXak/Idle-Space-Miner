using System;
using UnityEngine;

namespace Game
{
    public class EnemyHealth : IHealth<ScientificNotation>
    {
        public event Action<ScientificNotation> Initialized;
        public event Action<ScientificNotation> ValueChanged;
        public event Action Died;

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

        public void SetHealth(ScientificNotation value)
        {
            Value = value;
            Initialized?.Invoke(value);
        }

        public void GetDamage(ScientificNotation value)
        {
            if (value < 0)
                throw new Exception("The value cannot be less than zero");

            if (Value.IsZero())
                return;

            Value -= value;

            if (Value.IsZero())
                Died?.Invoke();
        }
    }
}
