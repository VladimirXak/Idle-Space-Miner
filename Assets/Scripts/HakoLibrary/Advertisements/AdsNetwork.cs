using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HakoLibrary.Ads
{
    public abstract class AdsNetwork : MonoBehaviour
    {
        public abstract void Init(bool isTestMode);
        public abstract bool IsReadyRewardAds();
        public abstract bool ShowRewardAds(Action callback);
    }
}
