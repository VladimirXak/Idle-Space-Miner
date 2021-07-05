namespace Game
{
    public class TimeMinutesConverter : ITimeConvertor
    {
        public string GetStringTime(int seconds)
        {
            int totalMinuts = seconds / 60;
            int remainderSeconds = seconds % 60;

            return $"{totalMinuts}m {remainderSeconds.ToString("D2")}s";
        }
    }
}
