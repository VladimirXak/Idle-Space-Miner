using UnityEngine;
using Zenject;

namespace Game
{
    public class CoinMining : MonoBehaviour
    {
        private IAdditive<ScientificNotation> _coins;
        private IHealth<ScientificNotation> _enemyHealth;
        private TemporeryBooster _booster;

        private ScientificNotation _lastValueHealth;

        [Inject]
        private void Construct(Currency currency, IHealth<ScientificNotation> enemyHealth, BoosterCollection boosters)
        {
            _coins = currency.Coins;
            _enemyHealth = enemyHealth;

            _booster = boosters.GetItem(BoosterType.Coin);
        }

        private void AddCoins(ScientificNotation value)
        {
            ScientificNotation countAddcoins = _lastValueHealth - value;
            _lastValueHealth = value;

            if (countAddcoins.IsZero())
                return;

            _coins.Add(countAddcoins * _booster.Multiplier);
        }

        private void ResetLastValueHealth(ScientificNotation value)
        {
            _lastValueHealth = value;
        }

        private void OnEnable()
        {
            _enemyHealth.Initialized += ResetLastValueHealth;
            _enemyHealth.ValueChanged += AddCoins;
        }

        private void OnDisable()
        {
            _enemyHealth.Initialized += ResetLastValueHealth;
            _enemyHealth.ValueChanged += AddCoins;
        }
    }
}
