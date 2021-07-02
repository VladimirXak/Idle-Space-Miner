using HakoLibrary.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class ArmamentWindow : MonoBehaviour
    {
        [SerializeField] private WeaponItem _weaponItemPrefab;
        [SerializeField] private Transform _parentItems;
        [Space(10)]
        [SerializeField] private Multiplier _multiplier;

        private Dictionary<int, WeaponItem> _weapons;

        [Inject]
        private void Construct(Armament armament, Currency currency)
        {
            _weapons = new Dictionary<int, WeaponItem>();

            foreach (var weapon in armament)
            {
                WeaponItem weaponItem = Instantiate(_weaponItemPrefab, _parentItems);
                weaponItem.Init(weapon, _multiplier, currency.Coins);

                if (weapon.Level == 0)
                {
                    weaponItem.gameObject.SetActive(false);
                    weapon.Upgraded += OnWeaponFirstUpgraded;

                    _weapons.Add(weapon.Config.Id, weaponItem);
                }
            }

            if (_weapons.Count != 0)
                _weapons.First().Value.Preview();
        }

        private void OnWeaponFirstUpgraded(Weapon weapon)
        {
            weapon.Upgraded -= OnWeaponFirstUpgraded;

            int nextWeaponId = weapon.Config.Id + 1;

            if (_weapons.ContainsKey(nextWeaponId))
            {
                WeaponItem nextWeapon = _weapons[nextWeaponId];
                nextWeapon.Preview();
            }
        }
    }
}
