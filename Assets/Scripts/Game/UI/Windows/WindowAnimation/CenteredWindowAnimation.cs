using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class CenteredWindowAnimation : MonoBehaviour, IWindowAnimation
    {
        [SerializeField] private Transform _mainPanel;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject _blockPanel;
        [Space(10)]
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

            _blockPanel.SetActive(true);
            _mainPanel.localScale = new Vector3(0.5f, 0.5f);
            _canvasGroup.alpha = 0;

            gameObject.SetActive(true);

            _canvasGroup.DOFade(1, 0.275f);

            _mainPanel.DOScale(1.05f, 0.2f).OnComplete(delegate
            {
                _mainPanel.DOScale(1f, 0.075f).OnComplete(delegate
                {
                    _blockPanel.SetActive(false);
                    Opened?.Invoke();
                });
            });
        }

        public void Close()
        {
            Closing?.Invoke();

            _blockPanel.SetActive(true);

            _canvasGroup.DOFade(0, 0.275f);

            _mainPanel.DOScale(1.05f, 0.075f).OnComplete(delegate
            {
                _mainPanel.DOScale(0.5f, 0.2f).OnComplete(delegate
                {
                    _blockPanel.SetActive(false);
                    gameObject.SetActive(false);
                    Closed?.Invoke();
                });
            });
        }
    }
}
