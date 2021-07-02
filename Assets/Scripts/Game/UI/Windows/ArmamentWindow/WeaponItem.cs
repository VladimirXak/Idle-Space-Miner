using HakoLibrary.LocalizationSpace;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HakoLibrary.UI;

namespace Game.UI
{
    public class WeaponItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private LocalizationTmp _name;
        [Space(10)]
        [SerializeField] private MultipleLocalizationTmp _damage;
        [SerializeField] private MultipleLocalizationTmp _level;
        [SerializeField] private TMP_Text _price;
        [Space(10)]
        [SerializeField] private Switch _switchButtonActivity;
        [SerializeField] private Button _upgradeButton;

        private Weapon _weapon;
        private Multiplier _multiplier;
        private ICurrency<ScientificNotation> _coins;

        private ScientificNotation _priceUpgrade;

        private void Awake()
        {
            _upgradeButton.onClick.AddListener(Upgrade);
        }

        public void Init(Weapon weapon, Multiplier multiplier, ICurrency<ScientificNotation> coins)
        {
            _weapon = weapon;
            _multiplier = multiplier;
            _coins = coins;

            _icon.sprite = weapon.Config.Icon;
            _name.SetKey(weapon.Config.Name);

            if (_weapon.Level == 0)
                _weapon.Upgraded += OnWeaponFirstUpgraded;
        }

        public void Preview()
        {
            _icon.color = Color.black;
            gameObject.SetActive(true);
        }

        private void Upgrade()
        {
            if (_coins.TrySpend(_priceUpgrade))
                _weapon.Upgrade(_multiplier.Value);
        }

        private void RecalculatePriceUpgrade(int multiplier)
        {
            _priceUpgrade = _weapon.Price;
            ScientificNotation priceForCurrentLevel = _weapon.Price;

            for (int i = 1; i < multiplier; i++)
            {
                priceForCurrentLevel *= Weapon.CoefUpgradeCost;
                _priceUpgrade += priceForCurrentLevel;
            }

            RenderPriceUpgrade();
        }

        private void OnWeaponUpgraded(Weapon weapon)
        {
            _damage.SetValue(weapon.Damage.ToString());
            _level.SetValue(weapon.Level.ToString());
            _price.text = weapon.Price.ToString();

            RecalculatePriceUpgrade(_multiplier.Value);
        }

        private void RenderPriceUpgrade()
        {
            _price.text = _priceUpgrade.ToString();

            TrySwitchActivityButtonUpgrade(_coins.Value);
        }

        private void TrySwitchActivityButtonUpgrade(ScientificNotation coinsValue)
        {
            _switchButtonActivity.TrySetState(coinsValue >= _priceUpgrade);
        }

        private void OnWeaponFirstUpgraded(Weapon weapon)
        {
            weapon.Upgraded -= OnWeaponFirstUpgraded;

            _icon.color = Color.white;
        }

        private void OnEnable()
        {
            OnWeaponUpgraded(_weapon);

            _weapon.Upgraded += OnWeaponUpgraded;
            _coins.ValueChanged += TrySwitchActivityButtonUpgrade;
            _multiplier.OnChange += RecalculatePriceUpgrade;
        }

        private void OnDisable()
        {
            _weapon.Upgraded -= OnWeaponUpgraded;
            _coins.ValueChanged -= TrySwitchActivityButtonUpgrade;
            _multiplier.OnChange -= RecalculatePriceUpgrade;
        }
    }
}
