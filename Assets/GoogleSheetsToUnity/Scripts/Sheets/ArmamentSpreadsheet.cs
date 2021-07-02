#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;
using System;
using HakoLibrary.GoogleSheets;
using Game.Data;
using Game;

namespace GoogleSpreadsheet
{
    public static class ArmamentSpreadsheet
    {
        public static void Rebuild(List<string[]> data)
        {
            GoogleSpreadsheetHelper.DeleteAssets(WeaponConfig.PathPrefab);

            string parsedStringData = ParseGoogleSpreadsheet.Parse(data);

            var spreadsheetData = JsonUtility.FromJson<SpreadsheetData>(parsedStringData);

            for (int i = 0; i < spreadsheetData.ListWeapon.Count; i++)
                CreateAsset(spreadsheetData.ListWeapon[i], i + 1);
        }

        private static void CreateAsset(WeaponSheetData weaponSheetData, int id)
        {
            WeaponData armamentData = new WeaponData()
            {
                Id = id,

                Name = weaponSheetData.Name,
                Icon = GoogleSpreadsheetHelper.GetSprite(weaponSheetData.Name),

                Damage = ScientificNotation.FromString(weaponSheetData.Damage),
                PriceUpgrade = ScientificNotation.FromString(weaponSheetData.PriceUpgrade)
            };

            WeaponConfig config = ScriptableObject.CreateInstance<WeaponConfig>();
            config.Init(armamentData);

            GoogleSpreadsheetHelper.CreateAsset(config, $"{WeaponConfig.PathPrefab}{config.Name}");
        }

        [Serializable]
        private struct SpreadsheetData
        {
            public List<WeaponSheetData> ListWeapon;
        }
        
        [Serializable]
        private struct WeaponSheetData
        {
            public string Name;

            public string Damage;
            public string PriceUpgrade;
        }
    }
}

#endif
