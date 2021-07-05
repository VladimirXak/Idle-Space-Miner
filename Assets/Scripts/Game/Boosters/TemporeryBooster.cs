using System;
using UnityEngine;

namespace Game
{
    public class TemporeryBooster : IAdditive<TimeSpan>
    {
        public event Action<TemporeryBooster> Changed;

        private bool _isActive;
        public bool IsActive
        {
            get
            {
                if (_isActive)
                    _isActive = EndTime > DateTime.Now;

                return _isActive;
            }
        }

        private int _multiplier;
        public int Multiplier
        {
            get => IsActive ? _multiplier : 1;
            private set => _multiplier = value;
        }

        public DateTime EndTime { get; private set; }

        public TemporeryBooster(int multiplier, TimeSpan time)
        {
            Multiplier = multiplier;
            EndTime = DateTime.Now.Add(time);

            _isActive = true;
        }

        public void Add(TimeSpan time)
        {
            if (EndTime < DateTime.Now)
                EndTime = DateTime.Now;

            EndTime = EndTime.Add(time);

            _isActive = true;

            Changed?.Invoke(this);
        }
    }
}
