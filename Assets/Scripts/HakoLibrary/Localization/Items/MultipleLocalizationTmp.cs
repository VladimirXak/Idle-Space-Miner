using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace HakoLibrary.LocalizationSpace
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MultipleLocalizationTmp : MonoBehaviour
    {
        [SerializeField] private List<ValueData> _values;

        private TextMeshProUGUI _tmp;

        private StringBuilder _line;
        private string _value;

        private void Awake()
        {
            if (TryGetLocalization(out var localization))
            {
                localization.OnChangeLanguage += ChangeLocalization;
                ChangeLocalization(localization);
            }
        }

        public void SetValue(string value)
        {
            _value = value;

            if (_line != null)
                RenderText();
        }

        private void ChangeLocalization(ILocalization localization)
        {
            if (_tmp == null)
                _tmp = GetComponent<TextMeshProUGUI>();

            _line = new StringBuilder();

            foreach (var value in _values)
            {
                if (value.TypeValue == TypeValue.Key)
                    _line.Append(localization.GetValue(value.Value));
                else if (value.TypeValue == TypeValue.Value)
                    _line.Append(value.Value);
            }

            RenderText();
        }

        private void RenderText()
        {
            string value = _line.ToString();

            if (_value != null)
                value += $" {_value}";

            _tmp.text = value;
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


        [System.Serializable]
        private struct ValueData
        {
            public TypeValue TypeValue;
            public string Value;
        }

        private enum TypeValue
        {
            Key,
            Value,
        }
    }
}
