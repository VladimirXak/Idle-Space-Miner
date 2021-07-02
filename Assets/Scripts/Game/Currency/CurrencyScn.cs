using Game.Data;
using System;
using UnityEngine;

namespace Game
{
    public class CurrencyScn : ICurrency<ScientificNotation>
    {
        public event Action<ScientificNotation> ValueChanged;
        public event Action<ScientificNotation> FailedSpend;

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

        public CurrencyScn(ScientificNotation value)
        {
            Value = value;
        }

        public CurrencyScn(ScientificNotationData jsonValue)
        {
            Value = new ScientificNotation(jsonValue.Mantissa, jsonValue.Order);
        }

        public virtual void Add(ScientificNotation value)
        {
            if (value < 0)
                throw new Exception("The value cannot be less than zero");

            Value += value;
        }

        public bool TrySpend(ScientificNotation price)
        {
            if (Value < price)
            {
                FailedSpend?.Invoke(price - Value);
                return false;
            }

            Value -= price;

            return true;
        }

        public ScientificNotationData GetData()
        {
            return new ScientificNotationData()
            {
                Mantissa = Value.Mantissa,
                Order = Value.Order
            };
        }
    }
}
