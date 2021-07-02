using HakoLibrary.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(MovableSideUI))]
    public class RetractableWindowAnimation : MonoBehaviour, IWindowAnimation
    {
        [SerializeField] private Button _closeButton;

        private MovableSideUI _movableSideUiAxisY;

        public event Action Opening;
        public event Action Opened;
        public event Action Closing;
        public event Action Closed;

        public MovableSideUI MovableSideUiAxisY
        {
            get
            {
                if (_movableSideUiAxisY == null)
                    _movableSideUiAxisY = GetComponent<MovableSideUI>();

                return _movableSideUiAxisY;
            }
        }

        private void Awake()
        {
            _closeButton.onClick.AddListener(Close);
        }

        public void Open()
        {
            Opening?.Invoke();
            gameObject.SetActive(true);

            MovableSideUiAxisY.Show(Opened);
        }

        public void Close()
        {
            Closing?.Invoke();
            MovableSideUiAxisY.Hide(Closed);
        }
    }
}
