using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class CurrencyDisplay : MonoBehaviour
    {
        [SerializeField] private CurrencyScnDisplay Coins;
        [SerializeField] private CurrencyScnDisplay Grols;

        [Inject]
        private void Construct(Currency currency)
        {
            Coins.Init(currency.Coins);
            Grols.Init(currency.Grols);
        }
    }
}
