
using UnityEngine;
#if UNITY_ANDROID
using YouAPPiSDK.Android;
#elif UNITY_IOS
//using YouAPPiSDK.iOS;
#endif
using YouAPPiSDK.Common;

namespace YouAPPiSDK.Api
{
    public class YouAppi
	{
		#region Variables

		public string environment {
			get {
				return this.client.environment;
			}
			set {
				this.client.environment = value;
			}
		}

		public string accessToken {
			get {
				return this.client.accessToken;
			}
			set {
				this.client.accessToken = value;
			}
		}

		public bool isInitialized {
			get {
				return this.client.isInitialized;
			}
			set {
				this.client.isInitialized = value;
			}
		}

		private IYouAppiClient client;

		#endregion

		public YouAppi ()
		{
			#if UNITY_IOS
			//this.client = new YouAppiClientiOS ();
			#elif UNITY_ANDROID
			this.client = new YouAppiClientAndroid();
			#endif
		}

		public void initialize (string token)
		{
			Debug.Log ("Initializing YouAppi");
			this.client.initialize (token);
		}

		public void showLog ()
		{
			this.client.showLog ();
		}

		public void setLogLevel (YALogLevel logLevel)
		{
			this.client.setLogLevel (logLevel);
		}

		public YAInterstitialAd interstitialAd (string adUnitID)
		{
			YAInterstitialAd interstitialAd = this.client.interstitialAd (adUnitID);
			return interstitialAd;
		}

		public YARewardedVideoAd rewardedVideo (string adUnitID)
		{
			YARewardedVideoAd rewardedVideoAd = this.client.rewardedVideoAd (adUnitID);
			return rewardedVideoAd;
		}

		public YAInterstitialVideoAd interstitialVideo (string AdUnitID)
		{
			YAInterstitialVideoAd interstitialVideoAd = this.client.interstitialVideoAd (AdUnitID);
			return interstitialVideoAd;
		}
	}
}