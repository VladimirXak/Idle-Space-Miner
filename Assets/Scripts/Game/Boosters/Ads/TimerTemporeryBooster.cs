using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class TimerTemporeryBooster : MonoBehaviour
    {
        [SerializeField] private Timer _timer;

        private TemporeryBooster _booster;

        public void Init(TemporeryBooster booster)
        {
            _booster = booster;
        }

        private void OnChangeEndTimeBooster(TemporeryBooster booster)
        {
            if (booster.IsActive)
                _timer.StartTimer(booster.EndTime);
        }

        private void OnEnable()
        {
            if (_booster == null)
                return;

            OnChangeEndTimeBooster(_booster);

            _booster.Changed += OnChangeEndTimeBooster;
        }

        private void OnDisable()
        {
            if (_booster == null)
                return;

            _booster.Changed -= OnChangeEndTimeBooster;
        }
    }
}
