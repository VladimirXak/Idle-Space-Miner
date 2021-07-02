using UnityEngine;

namespace Game.Data
{
    public class WeaponConfig : ScriptableObject
    {
        public const string PathPrefab = "Data/Armament/";

        [SerializeField] private int _id;
        public int Id => _id;

        [SerializeField] private string _name;
        public string Name => _name;

        [SerializeField] private Sprite _icon;
        public Sprite Icon => _icon;

        [SerializeField] private ScientificNotation _damage;
        public ScientificNotation Damage => _damage;

        [SerializeField] private ScientificNotation _priceUpgrade;
        public ScientificNotation PriceUpgrade => _priceUpgrade;

        public void Init(WeaponData data)
        {
            _id = data.Id;

            _name = data.Name;
            _icon = data.Icon;

            _damage = data.Damage;
            _priceUpgrade = data.PriceUpgrade;
        }
    }

    public struct WeaponData
    {
        public int Id;

        public string Name;
        public Sprite Icon;

        public ScientificNotation Damage;
        public ScientificNotation PriceUpgrade;
    }
}
