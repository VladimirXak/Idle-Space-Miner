using UnityEngine;
using TMPro;
using System;
using HakoLibrary.LocalizationSpace;

namespace Game
{
    public class TimerDisplay : MonoBehaviour, ILocalizationItem
    {
        [SerializeField] private Timer _timer;
        [Space(10)]
        [SerializeField] private TextMeshProUGUI _tmpTimerRender;
        [Space(10)]
        [SerializeField] private string _keyLocEndValue;

        private ITimeConvertor _timeConvertor;

        public void Init(ITimeConvertor timeConvertor)
        {
            _timeConvertor = timeConvertor;
        }

        private void RenderTime(TimeSpan span)
        {
            _tmpTimerRender.text = _timeConvertor.GetStringTime((int)span.TotalSeconds);
        }

        public void ChangeLocalization()
        {
            if (_timer.IsActive == false)
                _tmpTimerRender.text = Singleton<Localization>.Instance.GetValue(_keyLocEndValue);
        }

        private void OnEnable()
        {
            ChangeLocalization();

            _timer.Tick += RenderTime;
            _timer.Stoped += ChangeLocalization;
        }

        private void OnDisable()
        {
            _timer.Tick -= RenderTime;
            _timer.Stoped -= ChangeLocalization;
        }
    }
}
