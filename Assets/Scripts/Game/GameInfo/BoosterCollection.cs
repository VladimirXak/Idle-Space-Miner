using Game.Data;
using System;
using System.Collections.Generic;

namespace Game
{
    public class BoosterCollection
    {
        public Dictionary<int, TypedTemporeryBooster> _boosters;

        public BoosterCollection(BoosterCollectionSavedData savedData)
        {
            _boosters = new Dictionary<int, TypedTemporeryBooster>();

            if (savedData.Boosters == null)
                savedData.Boosters = new List<BoosterSavedData>();

            foreach (var boosterSavedData in savedData.Boosters)
            {
                BoosterType type = Enums.GetType<BoosterType>(boosterSavedData.Type);
                DateTime endTime = DateTime.FromFileTime(boosterSavedData.EndTime);

                _boosters.Add((int)type, CreateBooster(type, endTime));
            }
        }

        public TemporeryBooster GetItem(BoosterType type)
        {
            int idType = (int)type;

            if (_boosters.ContainsKey(idType) == false)
            {
                _boosters.Add(idType, CreateBooster(type, DateTime.Now));
            }

            return _boosters[idType];
        }

        private TypedTemporeryBooster CreateBooster(BoosterType type, DateTime endTime)
        {
            int multiplier = 1;

            switch (type)
            {
                case BoosterType.Coin:
                    multiplier = 2;
                    break;
            }


            TimeSpan time = TimeSpan.Zero;

            if (DateTime.Now < endTime)
                time = endTime - DateTime.Now;

            return new TypedTemporeryBooster(type, multiplier, time);
        }

        public BoosterCollectionSavedData GetData()
        {
            List<BoosterSavedData> boosterSavedDataCollection = new List<BoosterSavedData>();

            foreach (var booster in _boosters)
                boosterSavedDataCollection.Add(booster.Value.GetData());

            return new BoosterCollectionSavedData()
            {
                Boosters = boosterSavedDataCollection,
            };
        }
    }
}
