namespace HakoLibrary.LocalizationSpace
{
    public class LocalizationValue
    {
        public string Value;

        private string _key;

        public LocalizationValue(string key)
        {
            _key = key;

            if (TryGetLocalization(out var localization))
            {
                localization.OnChangeLanguage += ChangeLocalization;
                ChangeLocalization(localization);
            }
        }

        ~LocalizationValue()
        {
            if (TryGetLocalization(out var localization))
                localization.OnChangeLanguage -= ChangeLocalization;
        }

        private bool TryGetLocalization(out ILocalization localization)
        {
            localization = Singleton<Localization>.Instance;
            return localization != null;
        }

        public void ChangeLocalization(ILocalization localization)
        {
            Value = localization.GetValue(_key);
        }
    }
}
