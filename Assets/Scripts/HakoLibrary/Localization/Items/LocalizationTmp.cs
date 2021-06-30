using UnityEngine;
using TMPro;

namespace HakoLibrary.LocalizationSpace
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizationTmp : MonoBehaviour
    {
        [SerializeField] private string _keyLocalization;

        private TextMeshProUGUI _tmp;

        public string Key => _keyLocalization;

        private void Awake()
        {
            if (TryGetLocalization(out var localization))
            {
                localization.OnChangeLanguage += ChangeLocalization;
                ChangeLocalization(localization);
            }
        }

        public void ChangeLocalization(ILocalization localization)
        {
            if (_tmp == null)
                _tmp = GetComponent<TextMeshProUGUI>();

            _tmp.text = localization.GetValue(_keyLocalization);
        }

        public void SetKey(string key)
        {
            _keyLocalization = key;

            if (TryGetLocalization(out var localization))
                ChangeLocalization(localization);
        }

        private bool TryGetLocalization(out ILocalization value)
        {
            value = Singleton<Localization>.Instance;
            return value != null;
        }

        private void OnDestroy()
        {
            if (TryGetLocalization(out var localization))
                localization.OnChangeLanguage -= ChangeLocalization;
        }
    }
}
