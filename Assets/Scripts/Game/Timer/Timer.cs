using System;
using UnityEngine;

namespace Game
{
    public class Timer : MonoBehaviour
    {
        public event Action Started;
        public event Action Stoped;
        public event Action<TimeSpan> Tick;

        public bool IsActive { get; private set; }

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public void StartTimer(DateTime endTime)
        {
            StartTime = DateTime.Now;
            EndTime = endTime;
            IsActive = true;

            Started?.Invoke();
        }

        public void Stop()
        {
            IsActive = false;
            Stoped?.Invoke();
        }

        private void Update()
        {
            if (IsActive == false)
                return;

            if (DateTime.Now > EndTime)
            {
                IsActive = false;
                Stoped?.Invoke();
                return;
            }

            TimeSpan timeSpan = EndTime - DateTime.Now;

            Tick?.Invoke(timeSpan);
        }
    }
}
