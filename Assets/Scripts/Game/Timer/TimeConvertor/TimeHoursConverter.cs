namespace Game
{
    public class TimeHoursConverter : ITimeConvertor
    {
        public string GetStringTime(int seconds)
        {
            int hours = GetHours(seconds);

            seconds -= hours * 3600;

            int minutes = GetMinutes(seconds);

            int remainderSeconds = seconds % 60;

            return $"{hours.ToString("D2")}h : {minutes.ToString("D2")}m : {remainderSeconds.ToString("D2")}s";
        }

        private int GetHours(int seconds)
        {
            return seconds / 3600;
        }

        private int GetMinutes(int seconds)
        {
            return seconds / 60;
        }
    }
}
