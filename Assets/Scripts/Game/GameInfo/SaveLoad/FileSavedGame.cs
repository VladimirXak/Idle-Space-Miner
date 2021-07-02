using Game.Data;
using System.IO;
using UnityEngine;

namespace Game
{
    public class FileSavedGame : ISaveLoadSavedGame
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        private string _pathApplication = Application.persistentDataPath;
#else
        private string _pathApplication = Application.dataPath;
#endif

        private string _fileName;

        private StartingSavedData _startingSavedData;

        public FileSavedGame(string fileName = "SavedGame")
        {
            _fileName = fileName;
            _startingSavedData = new StartingSavedData();
        }

        public void Save(SavedData savedData)
        {
            string dataSavedGame = JsonUtility.ToJson(savedData);
            string encryptData = AesEncryption.Encrypt(dataSavedGame);

            using (StreamWriter sw = new StreamWriter($"{_pathApplication}/{_fileName}.hako"))
            {
                sw.Write(encryptData);
            }
        }

        public SavedData Load()
        {
            string path = $"{_pathApplication}/{_fileName}.hako";

            if (File.Exists(path) == false)
                return _startingSavedData.GetData();

            using (StreamReader sr = new StreamReader(path))
            {
                string data = sr.ReadToEnd();
                return Decrypt(data);
            }
        }

        private SavedData Decrypt(string data)
        {
            try
            {
                string decryptedData = AesEncryption.Decrypt(data);
                SavedData savedData = JsonUtility.FromJson<SavedData>(decryptedData);

                return savedData;
            }
            catch
            {
                return CreateStartSavedData();
            }
        }

        public SavedData CreateStartSavedData()
        {
            return _startingSavedData.GetData();
        }
    }
}
