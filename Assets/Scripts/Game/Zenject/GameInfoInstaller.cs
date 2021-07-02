using System.Collections.Generic;
using UnityEngine;
using Game.Data;
using Zenject;
using System.Linq;

namespace Game.Zenject
{
    public class GameInfoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SavedData savedData = new FileSavedGame().Load();

            List<WeaponConfig> weaponConfigs = GetWeaponConfigs();

            Container.Bind<Currency>().AsSingle().WithArguments(savedData.Currency);
            Container.Bind<GameLevel>().AsSingle().WithArguments(savedData.GameLevel);
            Container.Bind<Armament>().AsSingle().WithArguments(savedData.Armament, weaponConfigs);

            Container.Bind<GameInfo>().AsSingle().NonLazy();

            Container.Bind<IHealth<ScientificNotation>>().To<EnemyHealth>().AsSingle();
        }

        private List<WeaponConfig> GetWeaponConfigs()
        {
            List<WeaponConfig> configs = Resources.LoadAll<WeaponConfig>(WeaponConfig.PathPrefab).ToList();

            configs = (from config in configs
                       orderby config.Id
                       select config).ToList();

            return configs;
        }
    }
}