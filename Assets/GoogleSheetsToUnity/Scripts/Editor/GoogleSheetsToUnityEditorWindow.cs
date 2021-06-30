using GoogleSpreadsheet;
using UnityEditor;
using UnityEngine;

namespace HakoLibrary.GoogleSheets.Editor
{
    public class GoogleSheetsToUnityEditorWindow : EditorWindow
    {
        private GoogleSheetsConfig _config;

        private int _selectIdToolBar = 0;
        private bool _isShowSecretKey;

        [MenuItem("Tools/Hako/GoogleSheet")]
        public static void Open()
        {
            GoogleSheetsToUnityEditorWindow window = GetWindow<GoogleSheetsToUnityEditorWindow>("Google Sheets");

            window.Init();
        }

        public void Init()
        {
            _config = Resources.Load("GoogleSheetsConfig") as GoogleSheetsConfig;
        }

        private void OnGUI()
        {
            int newSelectIdToolBar = GUILayout.Toolbar(_selectIdToolBar, new string[] { "Sheets", "Access Settings" });

            if (newSelectIdToolBar != _selectIdToolBar)
            {
                _selectIdToolBar = newSelectIdToolBar;
                GUI.FocusControl(null);
            }

            switch (_selectIdToolBar)
            {
                case 0:
                    RenderSheets();
                    break;
                case 1:
                    RenderAccessSettings();
                    break;
            }
        }

        private void RenderAccessSettings()
        {
            _config.ClientID = EditorGUILayout.TextField("Client ID", _config.ClientID);

            GUILayout.BeginHorizontal();

            if (_isShowSecretKey)
                _config.SecretKey = EditorGUILayout.TextField("Secret Key", _config.SecretKey);
            else
                _config.SecretKey = EditorGUILayout.PasswordField("Secret Key", _config.SecretKey);

            _isShowSecretKey = GUILayout.Toggle(_isShowSecretKey, "Show");

            GUILayout.EndHorizontal();

            EditorGUILayout.Space();

            _config.PublicApiKey = EditorGUILayout.TextField("Public API Key", _config.PublicApiKey);
        }

        private void RenderSheets()
        {
            if (_config.ListSpreadSheetId.Count == 0)
            {
                _config.ListSpreadSheetId.Add(null);
            }

            for (int i = 0; i < _config.ListSpreadSheetId.Count; i++)
            {
                GUILayout.BeginHorizontal();

                _config.ListSpreadSheetId[i] = EditorGUILayout.TextField("SpreadSheetId", _config.ListSpreadSheetId[i]);

                if (GUILayout.Button("Delete"))
                {
                    DeleteLine(i);
                    GUI.FocusControl(null);
                }

                GUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Line SpreadSheetId"))
                _config.ListSpreadSheetId.Add(null);

            EditorGUILayout.Space();

            if (GUILayout.Button("Update Data Sheets"))
                ReedDataSheets();
        }

        private void DeleteLine(int i)
        {
            _config.ListSpreadSheetId.RemoveAt(i);
        }

        private void ReedDataSheets()
        {
            SpreadSheetWebRequest.ReedPublicDataSheets(_config.ListSpreadSheetId, _config.PublicApiKey, UpdateData);
        }

        private void UpdateData(JsonDataSpreadSheet data)
        {
            switch (data.sheet)
            {
                case "Localization":
                    LocalizationEditor.Rebuild(data);
                    break;
            }

            Debug.Log($"We readed the sheet [{data.sheet}]");
        }
    }
}
