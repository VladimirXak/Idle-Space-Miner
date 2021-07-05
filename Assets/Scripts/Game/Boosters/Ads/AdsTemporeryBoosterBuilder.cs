using UnityEngine;
using HakoLibrary.Ads;
using Zenject;

namespace Game
{
    public class AdsTemporeryBoosterBuilder : MonoBehaviour
    {
        [SerializeField] private AdsRewardActivator _adsRewardActivator;
        [SerializeField] private TimerTemporeryBooster _timerTemporeryBooster;
        [SerializeField] private TimerDisplay _timerDisplay;

        [SerializeField] private int _timeLifeBooster;
        [SerializeField] private BoosterType _boosterType;

        private TemporeryBooster _booster;

        [Inject]
        private void Construct(BoosterCollection boosters)
        {
            _booster = boosters.GetItem(_boosterType);

            _timerTemporeryBooster.Init(_booster);
            _timerDisplay.Init(new TimeConvertor());
        }

        private void OnWatchedAds()
        {
            _booster.Add(new System.TimeSpan(0,_timeLifeBooster,0));
        }

        private void OnEnable()
        {
            _adsRewardActivator.Watched += OnWatchedAds;
        }

        private void OnDisable()
        {
            _adsRewardActivator.Watched -= OnWatchedAds;
        }
    }
}
