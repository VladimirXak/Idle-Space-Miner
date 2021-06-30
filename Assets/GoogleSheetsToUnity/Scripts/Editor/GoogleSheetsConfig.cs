using System.Collections.Generic;
using UnityEngine;

namespace HakoLibrary.GoogleSheets
{
    public class GoogleSheetsConfig : ScriptableObject
    {
        public string ClientID;
        public string SecretKey;

        public string PublicApiKey;

        public List<string> ListSpreadSheetId;
    }
}
