using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class LanguageWindow : MonoBehaviour
    {
        [SerializeField] private List<LanguageItem> _languageItems;

        private IWindowAnimation _windowAnimation;
        public IWindowAnimation WindowAnimation
        {
            get
            {
                if (_windowAnimation == null)
                    _windowAnimation = GetComponent<IWindowAnimation>();

                return _windowAnimation;
            }
        }

        private void OnChangeLanguage()
        {
            _windowAnimation.Close();
        }

        private void OnEnable()
        {
            foreach (var language in _languageItems)
                language.Selected += OnChangeLanguage;
        }

        private void OnDisable()
        {
            foreach (var language in _languageItems)
                language.Selected -= OnChangeLanguage;
        }
    }
}
