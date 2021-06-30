#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace GoogleSpreadsheet
{
    public static class GoogleSpreadsheetHelper
    {
        private const string _resourcesPath = "Assets/Resources/";
        private const string _iconsPath = "Assets/Sprites";

        public static Sprite GetSprite(string name)
        {
            var GUIDsIcons = AssetDatabase.FindAssets($"{name} t: Texture2D", new[] { _iconsPath });

            foreach (var GUID in GUIDsIcons)
            {
                string tempPath = AssetDatabase.GUIDToAssetPath(GUID);

                if (tempPath.Contains($"{name}.png"))
                    return AssetDatabase.LoadAssetAtPath<Sprite>(tempPath);
            }

            return null;
        }

        public static Sprite GetSprite(string name, string path)
        {
            var GUIDsIcons = AssetDatabase.FindAssets($"{name} t: Texture2D", new[] { path });

            foreach (var GUID in GUIDsIcons)
            {
                string tempPath = AssetDatabase.GUIDToAssetPath(GUID);

                if (tempPath.Contains($"{name}.png"))
                    return AssetDatabase.LoadAssetAtPath<Sprite>(tempPath);
            }

            return null;
        }

        public static void CreateAsset(ScriptableObject scriptableObject, string path)
        {
            AssetDatabase.CreateAsset(scriptableObject, $"{_resourcesPath}{path}.asset");
            AssetDatabase.SaveAssets();
        }

        public static void DeleteAsset(string path)
        {
            AssetDatabase.DeleteAsset($"{_resourcesPath}{path}.asset");
            AssetDatabase.SaveAssets();
        }

        public static void DeleteAssets(string path)
        {
            var assets = Resources.LoadAll(path);

            foreach (var asset in assets)
                AssetDatabase.DeleteAsset($"Assets/Resources/{path}{asset.name}.asset");

            AssetDatabase.SaveAssets();
        }
    }
}

#endif
