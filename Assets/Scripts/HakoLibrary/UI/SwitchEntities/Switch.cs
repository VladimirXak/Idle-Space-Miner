using System;
using UnityEngine;

namespace HakoLibrary.UI
{
    public class Switch : MonoBehaviour
    {
        public event Action<bool> StateChanged;

        private bool _isOn = true;
        public bool IsOn
        {
            get => _isOn;
            private set
            {
                _isOn = value;
                StateChanged?.Invoke(_isOn);
            }
        }

        public void TrySetState(bool isOn)
        {
            if (IsOn != isOn)
                SetState(isOn);
        }

        public void SetState(bool isOn)
        {
            IsOn = isOn;
        }
    }
}
