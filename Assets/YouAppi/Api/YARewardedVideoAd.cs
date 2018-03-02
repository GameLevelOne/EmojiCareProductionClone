using System;
using UnityEngine;
using YouAPPiSDK.Api;
using YouAPPiSDK.Common;

namespace YouAPPiSDK.Api
{
	

    public class YARewardedVideoAd : YAInterstitialVideoAd
    {
        public YARewardedVideoAd(IYARewardedVideoAdClient adClient) : base(adClient)
        {
            adClient.Rewarded+= (sender, args) =>
            {
                if (this.Rewarded != null)
                {
                    this.Rewarded(sender, args);
                }
            };
        }

        public event EventHandler<AdUnitEventArg> Rewarded;
    }
}