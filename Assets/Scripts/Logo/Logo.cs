using DG.Tweening;
using HakoLibrary.Common;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Logo
{
    public class Logo : MonoBehaviour
    {
        [SerializeField] private Image _logo;
        [Space(10)]
        [SerializeField] private float _timeFade;

        public event Action Appeared;
        public event Action Faded;

        public void Appear()
        {
            _logo.SetAlpha(0);
            _logo.DOFade(1, _timeFade).SetEase(Ease.Linear).SetDelay(0.5f).OnComplete(delegate 
            {
                Appeared?.Invoke();
            });
        }

        public void Fade()
        {
            _logo.DOFade(0, _timeFade).SetEase(Ease.Linear).OnComplete(delegate
            {
                Faded?.Invoke();
            });
        }
    }
}
