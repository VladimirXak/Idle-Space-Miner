using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HakoLibrary.LocalizationSpace
{
    public class Localization : Singleton<Localization>, ILocalization
    {
        public event Action<ILocalization> OnChangeLanguage;

        private Dictionary<string, string> _listLocalizationValues = new Dictionary<string, string>();

        private const string _keyLanguage = "Language";

        public SystemLanguage Language { get; private set; }

        protected override void OnAwake()
        {
            if (!PlayerPrefs.HasKey(_keyLanguage))
            {
                if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Belarusian)
                {
                    PlayerPrefs.SetInt(_keyLanguage, (int)SystemLanguage.Russian);
                }
                else
                {
                    PlayerPrefs.SetInt(_keyLanguage, (int)SystemLanguage.English);
                }
            }

            SetLanguage((SystemLanguage)PlayerPrefs.GetInt(_keyLanguage));
        }

        public void SetLanguage(SystemLanguage language)
        {
            Language = language;

            _listLocalizationValues.Clear();

            TextAsset txtLocalization = Resources.Load<TextAsset>($"Localization/{language}") as TextAsset;

            if (txtLocalization != null)
            {
                DataLocalization dataLocalization = JsonUtility.FromJson<DataLocalization>(txtLocalization.ToString());

                foreach (var dataItemLocalization in dataLocalization.Data)
                {
                    _listLocalizationValues.Add(dataItemLocalization.Key, dataItemLocalization.Value);
                }
            }

            OnChangeLanguage?.Invoke(this);

            var items = FindObjectsOfType<MonoBehaviour>().OfType<ILocalizationItem>();

            foreach (var item in items)
            {
                item.ChangeLocalization();
            }

            PlayerPrefs.SetInt(_keyLanguage, (int)language);
        }

        public string GetValue(string key)
        {
            if (key == null)
                return null;

            key = key.ToUpper();

            if (_listLocalizationValues.ContainsKey(key))
            {
                return _listLocalizationValues[key];
            }

            Debug.Log(key);

            return key;
        }
    }
}
