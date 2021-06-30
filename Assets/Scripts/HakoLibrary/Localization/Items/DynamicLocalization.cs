using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

namespace HakoLibrary.LocalizationSpace
{
    public class DynamicLocalization
    {
        [SerializeField] private List<LocalizationData> _values;

        private TMP_Text _tmp;

        public DynamicLocalization(TMP_Text tmp)
        {
            _values = new List<LocalizationData>();

            _tmp = tmp;

            if (TryGetLocalization(out var localization))
                localization.OnChangeLanguage += OnLocalizationChanged;
        }

        public void Add(LocalizationData data, bool isRefresh = false)
        {
            _values.Add(data);

            if (isRefresh && TryGetLocalization(out var localization))
                OnLocalizationChanged(localization);
        }

        public void Refresh()
        {
            if (TryGetLocalization(out var localization))
                OnLocalizationChanged(localization);
        }

        public void Clear()
        {
            _values.Clear();
        }

        private void OnLocalizationChanged(ILocalization localization)
        {
            var line = new StringBuilder();

            foreach (var value in _values)
            {
                if (value.Type == LocalizationValueType.Key)
                    line.Append($"{localization.GetValue(value.Value)} ");
                else if (value.Type == LocalizationValueType.Value)
                    line.Append($"{value.Value} ");
            }

            _tmp.text = line.ToString();
        }

        private bool TryGetLocalization(out ILocalization value)
        {
            value = Singleton<Localization>.Instance;
            return value != null;
        }

        ~DynamicLocalization()
        {
            if (TryGetLocalization(out var localization))
                localization.OnChangeLanguage -= OnLocalizationChanged;
        }
    }
}
