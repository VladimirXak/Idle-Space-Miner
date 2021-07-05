using HakoLibrary.LocalizationSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class TimeConvertor : ITimeConvertor
    {
        private const int _secondsPerDay = 86400;
        private const int _secondsPerHour = 3600;
        private const int _secondsPerMinute = 60;

        private LocalizationValue shortDay;
        private LocalizationValue shortHour;
        private LocalizationValue shortMinute;
        private LocalizationValue shortSecond;

        public TimeConvertor()
        {
            shortDay = new LocalizationValue("short.day");
            shortHour = new LocalizationValue("short.hour");
            shortMinute = new LocalizationValue("short.minute");
            shortSecond = new LocalizationValue("short.second");
        }

        public string GetStringTime(int seconds)
        {
            int countDays = GetDays(ref seconds);
            int countHours = GetHours(ref seconds);

            if (countDays != 0)
                return $"{countDays}{shortDay.Value} : {countHours}{shortHour.Value}";

            int countMinutes = GetMinutes(ref seconds);

            if (countHours != 0)
                return $"{countHours}{shortHour.Value} : {countMinutes}{shortMinute.Value}";

            return $"{countMinutes}{shortMinute.Value} : {seconds}{shortSecond.Value}";
        }

        private int GetDays(ref int remainderSeconds)
        {
            int countDays = remainderSeconds / _secondsPerDay;
            remainderSeconds -= (countDays * _secondsPerDay);

            return countDays;
        }

        private int GetHours(ref int remainderSeconds)
        {
            int countHours = remainderSeconds / _secondsPerHour;
            remainderSeconds -= (countHours * _secondsPerHour);

            return countHours;
        }

        private int GetMinutes(ref int remainderSeconds)
        {
            int countMinutes = remainderSeconds / _secondsPerMinute;
            remainderSeconds -= (countMinutes * _secondsPerMinute);

            return countMinutes;
        }
    }
}
