using System;
using System.Collections.Generic;

namespace HakoLibrary.GoogleSheets
{
    [Serializable]
    public struct JsonDataSpreadSheet
    {
        public string sheet;
        public List<string[]> values;
    }

    [Serializable]
    public struct JsonDataTable
    {
        public List<JsonSheetTable> sheets;
    }

    [Serializable]
    public struct JsonSheetTable
    {
        public JsonPropertiesSheetTable properties;
    }

    [Serializable]
    public struct JsonPropertiesSheetTable
    {
        public string title;
    }
}
