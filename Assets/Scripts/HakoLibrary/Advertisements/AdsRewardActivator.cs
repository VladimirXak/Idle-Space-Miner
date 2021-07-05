using System;
using UnityEngine;
using UnityEngine.UI;

namespace HakoLibrary.Ads
{
    public class AdsRewardActivator : MonoBehaviour
    {
        [SerializeField] private Button _watchAdsButton;

        public event Action Watched;

        private void Awake()
        {
            _watchAdsButton.onClick.AddListener(WatchAds);
        }

        private void WatchAds()
        {
            Advertisements ads = Singleton<Advertisements>.Instance;

            if (ads.IsReadyRewardAds())
                ads.ShowRewardAds(IssueReward);
        }

        private void IssueReward()
        {
            Watched?.Invoke();
        }
    }
}
