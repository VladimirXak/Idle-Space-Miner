using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace HakoLibrary.GoogleSheets
{
    public static class SpreadSheetWebRequest
    {
        private const string _startCell = "A1";
        private const string _endCell = "Z1000";

        public static void ReedPublicDataSheets(List<string> listSpreadSheetId, string publicAPIKey, Action<JsonDataSpreadSheet> callBack)
        {
            EditorCoroutineRunner.StartCoroutine(ReedTables(listSpreadSheetId, publicAPIKey, callBack));
        }

        private static IEnumerator ReedTables(List<string> listSpreadSheetId, string publicAPIKey, Action<JsonDataSpreadSheet> callBack)
        {
            for (int i = 0; i < listSpreadSheetId.Count; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("https://sheets.googleapis.com/v4/spreadsheets/");
                sb.Append(listSpreadSheetId[i]);
                sb.Append("?key=" + publicAPIKey);

                UnityWebRequest uwr = UnityWebRequest.Get(sb.ToString());

                yield return uwr.SendWebRequest();

                JsonDataTable dataTable = new JsonDataTable();

                try
                {
                    dataTable = JsonConvert.DeserializeObject<JsonDataTable>(uwr.downloadHandler.text);
                }
                catch { }

                yield return EditorCoroutineRunner.StartCoroutine(ReadSheets(dataTable, listSpreadSheetId[i], publicAPIKey, callBack));
            }
        }

        private static IEnumerator ReadSheets(JsonDataTable jsonDataTable, string spreadSheetId, string publicAPIKey, Action<JsonDataSpreadSheet> callBack)
        {
            Debug.Log(jsonDataTable.sheets);
            Debug.Log($"Found {jsonDataTable.sheets.Count} sheets. Table id:[{spreadSheetId}]");

            foreach (var dataSheet in jsonDataTable.sheets)
            {
                //Debug.Log($"We read the sheet [{dataSheet.properties.title}]");

                StringBuilder sb = new StringBuilder();
                sb.Append("https://sheets.googleapis.com/v4/spreadsheets/");
                sb.Append(spreadSheetId);
                sb.Append("/values/");
                sb.Append(dataSheet.properties.title);
                sb.Append($"!{_startCell}:{_endCell}");
                sb.Append("?key=" + publicAPIKey);

                UnityWebRequest uwr = UnityWebRequest.Get(sb.ToString());

                yield return uwr.SendWebRequest();

                JsonDataSpreadSheet jsonDataSpreadSheet = JsonConvert.DeserializeObject<JsonDataSpreadSheet>(uwr.downloadHandler.text);
                jsonDataSpreadSheet.sheet = dataSheet.properties.title;

                callBack?.Invoke(jsonDataSpreadSheet);
            }

            AssetDatabase.Refresh();

            Debug.Log($"Reading the table finished");
        }
    }
}
