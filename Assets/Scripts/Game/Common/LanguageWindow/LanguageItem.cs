using UnityEngine;
using UnityEngine.UI;
using HakoLibrary.LocalizationSpace;
using System;

namespace Game.UI
{
    public class LanguageItem : MonoBehaviour
    {
        [SerializeField] private SystemLanguage _language;
        [Space(10)]
        [SerializeField] private Button _selectButton;

        public event Action Selected;

        private Localization _localization;

        private void Awake()
        {
            _selectButton.onClick.AddListener(Select);

            _localization = Singleton<Localization>.Instance;
        }

        private void Select()
        {
            if (_localization.Language != _language)
                _localization.SetLanguage(_language);

            Selected?.Invoke();
        }
    }
}
