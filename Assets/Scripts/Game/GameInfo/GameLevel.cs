using Game.Data;
using System;
using UnityEngine;

namespace Game
{
    public class GameLevel : IValue<int>
    {
        public event Action<int> ValueChanged;
        public event Action<int> Completed;

        public int MaxPassedValue { get; private set; }

        private int _value;
        public int Value
        {
            get => _value;

            private set
            {
                if (value < 1)
                    value = 1;

                _value = value;

                if (_value > MaxPassedValue)
                    MaxPassedValue = _value - 1;

                ValueChanged?.Invoke(_value);
            }
        }

        public GameLevel(GameLevelSavedData savedData)
        {
            MaxPassedValue = savedData.MaxPassedLevel;
            Value = savedData.CurrentLevel;
        }

        public void LevelCompletion()
        {
            if (Value <= MaxPassedValue)
            {
                ValueChanged?.Invoke(Value);
                return;
            }

            Value++;

            Completed?.Invoke(_value);
        }

        public void GoToPreviousLevel()
        {
            Value--;
        }

        public void GoToNextLevel()
        {
            if (Value > MaxPassedValue)
                return;

            Value++;
        }

        public GameLevelSavedData GetData()
        {
            return new GameLevelSavedData()
            {
                MaxPassedLevel = MaxPassedValue,
                CurrentLevel = Value,
            };
        }
    }
}
