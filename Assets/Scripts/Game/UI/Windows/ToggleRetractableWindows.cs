using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ToggleRetractableWindows : MonoBehaviour
    {
        [SerializeField] private List<ToggleWindowData> _windowCollection;

        private IWindowAnimation _activeWindow;
        private IWindowAnimation _nextWindow;

        private StatusWindowMoveType _status;

        private void Awake()
        {
            foreach (var toggleWindow in _windowCollection)
            {
                if (toggleWindow.Window.TryGetComponent<IWindowAnimation>(out var windowAnimation))
                    toggleWindow.ToggleButton.onClick.AddListener(() => TryOpenWindow(windowAnimation));
            }
        }

        private void TryOpenWindow(IWindowAnimation window)
        {
            if (_nextWindow == window)
                return;

            if (_status == StatusWindowMoveType.Close)
            {
                _activeWindow = window;

                OpenWindow();
            }
            else if (_activeWindow.Equals(window))
            {
                if (_status == StatusWindowMoveType.Closing)
                {
                    _status = StatusWindowMoveType.Opening;

                    _activeWindow.Opened += OnWindowOpened;
                    _activeWindow.Open();
                    return;
                }

                _status = StatusWindowMoveType.Closing;

                _activeWindow.Opened -= OnWindowOpened;
                _activeWindow.Close();
            }
            else
            {
                _nextWindow = window;

                _status = StatusWindowMoveType.Closing;

                _activeWindow.Opened -= OnWindowOpened;
                _activeWindow.Close();
            }
        }

        private void OpenWindow()
        {
            _status = StatusWindowMoveType.Opening;

            _nextWindow = null;

            _activeWindow.Opened += OnWindowOpened;
            _activeWindow.Closed += OnWindowClosed;
            _activeWindow.Open();
        }

        private void OnWindowOpened()
        {
            _status = StatusWindowMoveType.Open;
            _activeWindow.Opened -= OnWindowOpened;
        }

        private void OnWindowClosed()
        {
            _status = StatusWindowMoveType.Close;
            _activeWindow.Closed -= OnWindowClosed;

            if (_nextWindow != null)
            {
                _activeWindow = _nextWindow;
                OpenWindow();
            }
        }

        [System.Serializable]
        private struct ToggleWindowData
        {
            [SerializeField] private GameObject _window;
            [SerializeField] private Button _toggleButton;

            public GameObject Window => _window;
            public Button ToggleButton => _toggleButton;
        }

        private enum StatusWindowMoveType
        {
            Close,
            Open,
            Closing,
            Opening,
        }
    }
}
