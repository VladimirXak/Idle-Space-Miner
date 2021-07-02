using Game.Data;

namespace Game
{
    public class Currency
    {
        public ICurrency<ScientificNotation> Coins { get; private set; }
        public ICurrency<ScientificNotation> Grols { get; private set; }

        public Currency(CurrencySavedData savedData)
        {
            Coins = new CurrencyScn(savedData.Coins.ToScn());
            Grols = new CurrencyScn(savedData.Grols.ToScn());
        }

        public CurrencySavedData GetData()
        {
            return new CurrencySavedData()
            {
                Coins = new ScientificNotationData(Coins.Value),
                Grols = new ScientificNotationData(Grols.Value),
            };
        }
    }
}
