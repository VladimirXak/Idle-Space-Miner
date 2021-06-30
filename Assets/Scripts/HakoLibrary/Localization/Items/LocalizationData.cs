using System;

namespace HakoLibrary.LocalizationSpace
{
    [Serializable]
    public struct LocalizationData
    {
        public LocalizationValueType Type;
        public string Value;

        public LocalizationData(LocalizationValueType type, object value)
        {
            Type = type;
            Value = value.ToString();
        }
    }

    public enum LocalizationValueType
    {
        Key,
        Value,
    }
}