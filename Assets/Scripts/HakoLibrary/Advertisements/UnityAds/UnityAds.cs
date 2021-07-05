using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace HakoLibrary.Ads
{
    public class UnityAds : AdsNetwork
    {
        [SerializeField] private string _androidAdvertId;
        [SerializeField] private string _iosAdvertId;
        [Space(10)]
        [SerializeField] private string _rewardVideoAndroidId;

        [SerializeField] private RewardUnityAds _rewardUnityAds;

        private bool _isTestMode;
        private string _advertId;

        public override void Init(bool isTestMode)
        {
            _isTestMode = isTestMode;

#if UNITY_IOS
            _advertId = _iosAdvertId;
#elif UNITY_ANDROID || UNITY_EDITOR
            _advertId = _androidAdvertId;
#endif
            CheckAndTryInitialize();

            _rewardUnityAds.Init(this, _rewardVideoAndroidId);
        }

        public void CheckAndTryInitialize()
        {
            if (Advertisement.isInitialized == false)
                Advertisement.Initialize(_advertId, _isTestMode);
        }

        public override bool IsReadyRewardAds()
        {
            return _rewardUnityAds.IsReadyAds();
        }

        public override bool ShowRewardAds(Action callback)
        {
            return _rewardUnityAds.Show(callback);
        }
    }
}
