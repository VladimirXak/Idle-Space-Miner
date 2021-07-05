using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace HakoLibrary.Ads
{
    public class RewardUnityAds : MonoBehaviour, IUnityAdsListener
    {
        public TypeLoadingAds TypeLoadingAds { get; private set; }

        private Action OnGiveReward;

        private UnityAds _unityAds;
        private string _videoId;

        public void Init(UnityAds unityAds, string videoId)
        {
            _unityAds = unityAds;
            _videoId = videoId;

            Advertisement.AddListener(this);

            TypeLoadingAds = TypeLoadingAds.Loading;
            Advertisement.Load(_videoId);
        }

        public bool IsReadyAds()
        {
            if (Advertisement.IsReady(_videoId))
                return true;

            _unityAds.CheckAndTryInitialize();

            if (TypeLoadingAds == TypeLoadingAds.Error)
            {
                Load();
            }

            return false;
        }

        public bool Show(Action callback = null)
        {
            OnGiveReward = callback;

            if (Advertisement.IsReady(_videoId))
            {
                Advertisement.Show(_videoId);
                return true;
            }

            return false;
        }

        private void Load()
        {
            TypeLoadingAds = TypeLoadingAds.Loading;
            Advertisement.Load(_videoId);
        }

        public void OnUnityAdsReady(string placementId)
        {
            if (placementId == _videoId)
                TypeLoadingAds = TypeLoadingAds.Ready;
        }

        public void OnUnityAdsDidError(string message)
        {
            TypeLoadingAds = TypeLoadingAds.Error;
        }

        public void OnUnityAdsDidStart(string placementId)
        {

        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (showResult == ShowResult.Finished && placementId == _videoId)
            {
                OnGiveReward?.Invoke();
            }
        }
    }
}
