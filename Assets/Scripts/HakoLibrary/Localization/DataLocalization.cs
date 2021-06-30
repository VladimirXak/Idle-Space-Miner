using System.Collections.Generic;
using System;

namespace HakoLibrary.LocalizationSpace
{
    [Serializable]
    public struct DataLocalization
    {
        public List<DataItemLocalization> Data;
    }

    [Serializable]
    public struct DataItemLocalization
    {
        public string Key;
        public string Value;
    }
}
