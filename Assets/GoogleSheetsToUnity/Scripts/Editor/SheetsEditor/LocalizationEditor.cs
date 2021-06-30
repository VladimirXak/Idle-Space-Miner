using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace HakoLibrary.GoogleSheets
{
    public static class LocalizationEditor
    {
        private static string pathFiles = $"{Application.dataPath}/Resources/Localization/";

        public static void Rebuild(JsonDataSpreadSheet data)
        {
            var values = data.values;

            for (int cell = 1; cell < values[0].Length; cell++)
            {
                JsonLocalization localization = new JsonLocalization()
                {
                    Language = values[0][cell],
                    Data = new List<NodeLocalization>()
                };

                for (int row = 1; row < values.Count; row++)
                {
                    if (values[row].Length == 0 || cell >= values[row].Length)
                        continue;

                    NodeLocalization dataNode = new NodeLocalization()
                    {
                        Key = values[row][0].ToUpper(),
                        Value = values[row][cell]
                    };

                    localization.Data.Add(dataNode);
                }

                if (Directory.Exists(pathFiles) == false)
                    Directory.CreateDirectory(pathFiles);

                using (StreamWriter sw = new StreamWriter($"{pathFiles}{localization.Language}.json"))
                {
                    sw.Write(JsonUtility.ToJson(localization));
                }
            }

            AssetDatabase.Refresh();
        }

        [System.Serializable]
        public struct JsonLocalization
        {
            public string Language;
            public List<NodeLocalization> Data;
        }

        [System.Serializable]
        public struct NodeLocalization
        {
            public string Key;
            public string Value;
        }
    }
}
