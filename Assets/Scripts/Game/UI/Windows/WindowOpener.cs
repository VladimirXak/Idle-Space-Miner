using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class WindowOpener : MonoBehaviour
    {
        [SerializeField] private List<WindowData> _windows;

        private void Awake()
        {
            foreach (var windowData in _windows)
            {
                if (windowData.Window.TryGetComponent<IWindowAnimation>(out var windowAnimation))
                    windowData.OpenButton.onClick.AddListener(() => OpenWindow(windowAnimation));
            }
        }

        private void OpenWindow(IWindowAnimation window)
        {
            window.Open();
        }

        [Serializable]
        private struct WindowData
        {
            public GameObject Window;
            public Button OpenButton;
        }
    }
}
