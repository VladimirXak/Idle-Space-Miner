using Game.Data;

namespace Game
{
    public class GameInfo
    {
        public Currency Currency { get; private set; }

        public GameLevel Level { get; private set; }
        public Armament Armament { get; private set; }

        public BoosterCollection Boosters { get; private set; }

        private ISaveLoadSavedGame _saveLoadSavedGame;

        public GameInfo(Currency currency, GameLevel level, Armament armament, BoosterCollection boosters)
        {
            _saveLoadSavedGame = new FileSavedGame();

            Currency = currency;

            Level = level;
            Armament = armament;

            Boosters = boosters;
        }

        public void SaveData()
        {
            SavedData savedData = GetData();

            _saveLoadSavedGame.Save(savedData);
            new FileSavedGame().Save(savedData);
        }

        private SavedData GetData()
        {
            return new SavedData()
            {
                Currency = Currency.GetData(),
                Level = Level.GetData(),
                Armament = Armament.GetData(),

                Boosters = Boosters.GetData(),
            };
        }
    }
}
