using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using YouAPPiSDK.Api;
using YouAPPiSDK.Common;

#if UNITY_ANDROID

namespace YouAPPiSDK.Android
{
    public class RewardedVideoAdAndroidClient : InterstitialVideoAdAndoridClient,IYARewardedVideoAdClient

    {
        public RewardedVideoAdAndroidClient(AndroidJavaObject interstitialNativeObject,string nativeListenerClassName,string setListenerMethodName, IUIExecutor uiRunner) : base(interstitialNativeObject, nativeListenerClassName,setListenerMethodName, uiRunner)
        {
        }

        public event EventHandler<AdUnitEventArg> Rewarded;
        /**
		 * This callback is called when a user has completed watching a full video ad.
		 */
       public void onRewarded(string adUnitId) {
            if (this.Rewarded!=null)
            {
                this.Rewarded(this,new AdUnitEventArg(adUnitId));
            }
        }
    }
}

#endif