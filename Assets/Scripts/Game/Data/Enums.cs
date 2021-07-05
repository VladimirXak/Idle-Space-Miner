public static class Enums
{
    public static T GetType<T>(string value)
    {
        if (value == null)
            throw new System.Exception("The value can not is null");

        foreach (T type in System.Enum.GetValues(typeof(T)))
        {
            if (type.ToString() == value)
                return type;
        }

        return default;
    }
}

public enum BoosterType
{
    NULL = 0,

    Coin = 1,
}