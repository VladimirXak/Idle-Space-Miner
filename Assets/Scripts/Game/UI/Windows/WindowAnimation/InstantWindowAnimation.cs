using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class InstantWindowAnimation : MonoBehaviour, IWindowAnimation
    {
        [SerializeField] private Button _closeButton;

        public event Action Opening;
        public event Action Opened;
        public event Action Closing;
        public event Action Closed;

        private void Awake()
        {
            _closeButton.onClick.AddListener(Close);
        }

        public void Open()
        {
            Opening?.Invoke();
            gameObject.SetActive(true);
            Opened?.Invoke();
        }

        public void Close()
        {
            Closing?.Invoke();
            gameObject.SetActive(false);
            Closed?.Invoke();
        }
    }
}
