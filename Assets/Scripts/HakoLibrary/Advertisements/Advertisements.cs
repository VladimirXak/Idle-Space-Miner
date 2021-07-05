using System;
using System.Collections.Generic;
using UnityEngine;

namespace HakoLibrary.Ads
{
    public class Advertisements : Singleton<Advertisements>
    {
        [SerializeField] private bool _isTestMode;
        [Space(10)]
        [SerializeField] private List<AdsNetwork> _adsNetworks;

        private bool _isInit;

        protected override void OnAwake()
        {
            Init();
        }

        private void Init()
        {
            _isInit = true;
            foreach (var adsNetwork in _adsNetworks)
                adsNetwork.Init(_isTestMode);
        }

        public bool IsReadyRewardAds()
        {
            if (_isInit == false)
                Init();

            foreach (var adsNetwork in _adsNetworks)
            {
                if (adsNetwork.IsReadyRewardAds())
                    return true;
            }

            return false;
        }

        public void ShowRewardAds(Action callback)
        {
            foreach (var adsNetwork in _adsNetworks)
            {
                if (adsNetwork.ShowRewardAds(callback))
                    break;
            }
        }
    }

    public enum TypeLoadingAds
    {
        Error,
        Ready,
        Loading,
    }
}
