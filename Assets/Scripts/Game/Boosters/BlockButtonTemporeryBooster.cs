using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class BlockButtonTemporeryBooster : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Timer _timer;

        private void OnStartTimer()
        {
            _button.interactable = false;
        }

        private void OnStopTimer()
        {
            _button.interactable = true;
        }

        private void OnEnable()
        {
            _button.interactable = !_timer.IsActive;

            _timer.Started += OnStartTimer;
            _timer.Stoped += OnStopTimer;
        }

        private void OnDisable()
        {
            _timer.Started -= OnStartTimer;
            _timer.Stoped -= OnStopTimer;
        }
    }
}
