using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class TimerProgressDisplay : MonoBehaviour
    {
        [SerializeField] private Timer _timer;
        [Space(10)]
        [SerializeField] private Image _fill;

        private double _totalSeconds;

        private void OnStartedTimer()
        {
            _fill.fillAmount = 0;
            _totalSeconds = (_timer.EndTime - _timer.StartTime).TotalSeconds;
        }

        private void OnTick(TimeSpan timeSpan)
        {
            _fill.fillAmount = 1 - (float)(timeSpan.TotalSeconds / _totalSeconds);
        }

        private void OnStoped()
        {
            _fill.fillAmount = 0;
        }

        private void OnEnable()
        {
            OnStartedTimer();

            if (_timer.IsActive)
                OnTick(_timer.EndTime - _timer.StartTime);

            _timer.Started += OnStartedTimer;
            _timer.Tick += OnTick;
            _timer.Stoped += OnStoped;
        }

        private void OnDisable()
        {
            _timer.Started -= OnStartedTimer;
            _timer.Tick -= OnTick;
            _timer.Stoped -= OnStoped;
        }
    }
}
