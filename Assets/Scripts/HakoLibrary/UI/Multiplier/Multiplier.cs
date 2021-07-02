using System;
using UnityEngine;
using UnityEngine.UI;

namespace HakoLibrary.UI
{
    [RequireComponent(typeof(Button))]
    public class Multiplier : MonoBehaviour
    {
        public event Action<int> OnChange;

        private int _value = _multilierArray[0];
        public int Value
        {
            get => _value;
            private set
            {
                _value = value;
                OnChange?.Invoke(value);
            }
        }

        private static int[] _multilierArray = { 1, 5, 10, 25, 50, 100 };

        private int _indexMultiplier;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(NextMultiplier);
        }

        private void NextMultiplier()
        {
            _indexMultiplier++;

            if (_indexMultiplier >= _multilierArray.Length)
                _indexMultiplier = 0;

            Value = _multilierArray[_indexMultiplier];
        }
    }
}
