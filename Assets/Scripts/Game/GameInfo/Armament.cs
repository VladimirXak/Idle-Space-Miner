using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;

namespace Game
{
    public class Armament : IEnumerable<Weapon>
    {
        private Dictionary<int, Weapon> _weapons;

        public Armament(ArmamentSavedData savedData, List<WeaponConfig> configs)
        {
            _weapons = new Dictionary<int, Weapon>();

            if (savedData.Weapons == null)
                savedData.Weapons = new List<WeaponSavedData>();

            foreach (var config in configs)
            {
                WeaponSavedData weaponSavedData = savedData.Weapons.Find(v => v.Id == config.Id);

                Weapon weapon = new Weapon(weaponSavedData, config);

                _weapons.Add(config.Id, weapon);
            }
        }

        public Weapon GetItem(int id)
        {
            if (_weapons.ContainsKey(id))
                return _weapons[id];

            return null;
        }

        public IEnumerator<Weapon> GetEnumerator()
        {
            foreach (var weapon in _weapons)
                yield return weapon.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _weapons.GetEnumerator();
        }

        public ArmamentSavedData GetData()
        {
            List<WeaponSavedData> weaponsData = new List<WeaponSavedData>();

            foreach (var weapon in _weapons)
                weaponsData.Add(weapon.Value.GetData());

            return new ArmamentSavedData()
            {
                Weapons = weaponsData
            };
        }
    }
}
