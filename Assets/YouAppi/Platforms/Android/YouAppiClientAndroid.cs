using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using YouAPPiSDK.Common;
using YouAPPiSDK.Api;
#if UNITY_ANDROID
namespace YouAPPiSDK.Android
{

	public class YouAppiClientAndroid : IYouAppiClient,IUIExecutor {
		private AndroidJavaObject _youappi;

		public void initialize (string token)
		{
				AndroidJavaClass youAPPiSDKCls = new AndroidJavaClass ("com.youappi.ai.sdk.YouAPPi");
				AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
				AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject> ("currentActivity");
				runOnUI(()=>{
					youAPPiSDKCls.CallStatic<bool> ("init", new object[] { activity, token});
					_youappi = youAPPiSDKCls.CallStatic<AndroidJavaObject> ("getInstance");
				});
		}

	public void showLog ()
	{
		throw new System.NotImplementedException ();
	}

	public void setLogLevel (YALogLevel level)
	{
		throw new System.NotImplementedException ();
	}

	public YAInterstitialAd interstitialAd (string adUnitID)
	{
            AndroidJavaObject interstitialNativeObject= _youappi.Call<AndroidJavaObject>("interstitialAd", new object[] { adUnitID });
            return new YAInterstitialAd(new InterstitialAdAndroidClient(interstitialNativeObject, "com.youappi.ai.sdk.ads.YAInterstitialAd$InterstitialAdListener", "setInterstitialAdListener", this));

    }

	public YAInterstitialVideoAd interstitialVideoAd (string adUnitID)
	{
            AndroidJavaObject interstitialNativeObject = _youappi.Call<AndroidJavaObject>("interstitialVideoAd", new object[] { adUnitID });
            return new YAInterstitialVideoAd(new InterstitialVideoAdAndoridClient(interstitialNativeObject, "com.youappi.ai.sdk.ads.YAInterstitialVideoAd$InterstitialVideoAdListener", "setInterstitialVideoAdListener", this));
    }

	public YARewardedVideoAd rewardedVideoAd (string adUnitID)
	{
            AndroidJavaObject interstitialNativeObject = _youappi.Call<AndroidJavaObject>("rewardedVideoAd", new object[] { adUnitID });
            return new YARewardedVideoAd(new RewardedVideoAdAndroidClient(interstitialNativeObject, "com.youappi.ai.sdk.ads.YARewardedVideoAd$RewardedVideoAdListener", "setRewardedVideoAdListener", this));
        }

	public string environment {
		get {
			return "";
		}
		set {
			
		}
	}

	public string accessToken {
		get {
                return "";
		}
		set {
			;
		}
	}

	public bool isInitialized {
		get {
                return true;
		}
		set {
			throw new System.NotImplementedException ();
		}
	}

		public void runOnUI(Action func)
		{
			AndroidJavaClass youAPPiSDKCls = new AndroidJavaClass ("com.youappi.ai.sdk.AdActivity");
			AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject> ("currentActivity");
			activity.Call ("runOnUiThread", new AndroidJavaRunnable (func));
		}


}

}
#endif
