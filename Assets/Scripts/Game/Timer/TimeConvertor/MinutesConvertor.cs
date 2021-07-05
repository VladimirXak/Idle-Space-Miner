using HakoLibrary.LocalizationSpace;
using System.Text;

namespace Game
{
    public class MinutesConvertor
    {
        private const int _minutesPerDay = 1440;
        private const int _minutesPerHour = 60;

        private string shortDay;
        private string shortHour;
        private string shortMinute;

        public MinutesConvertor()
        {
            if (TryGetLocalization(out var localization))
            {
                OnLanguageChanged(localization);
                localization.OnChangeLanguage += OnLanguageChanged;
            }
        }

        public string GetStringTime(int minutes)
        {
            int countDays = GetDays(minutes, out minutes);
            int countHours = GetHours(minutes, out minutes);
            int countMinutes = minutes;

            var line = new StringBuilder();

            if (countDays != 0)
            {
                line.Append($"{countDays}{shortDay}");

                if (countHours != 0)
                    line.Append($" {countHours}{shortHour}");

                return line.ToString();
            }

            if (countHours != 0)
            {
                line.Append($"{countHours}{shortHour}");

                if (countMinutes != 0)
                    line.Append($" {countMinutes}{shortMinute}");

                return line.ToString();
            }

            return $"{countMinutes}{shortMinute}";
        }

        public int GetDays(int minutes, out int remainder)
        {
            int countDays = minutes / _minutesPerDay;
            remainder = minutes - (countDays * _minutesPerDay);

            return countDays;
        }

        public int GetHours(int minutes, out int remainder)
        {
            int countHours = minutes / _minutesPerHour;
            remainder = minutes - (countHours * _minutesPerHour);

            return countHours;
        }

        private void OnLanguageChanged(ILocalization localization)
        {
            shortDay = localization.GetValue("short.day");
            shortHour = localization.GetValue("short.hour");
            shortMinute = localization.GetValue("short.minute");
        }

        private bool TryGetLocalization(out ILocalization value)
        {
            value = Singleton<Localization>.Instance;
            return value != null;
        }

        ~MinutesConvertor()
        {
            if (TryGetLocalization(out var localization))
                localization.OnChangeLanguage -= OnLanguageChanged;
        }
    }
}
