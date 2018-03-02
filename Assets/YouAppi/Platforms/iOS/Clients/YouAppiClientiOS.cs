using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using AOT;
using YouAPPiSDK.Api;
using YouAPPiSDK.Common;

#if UNITY_IOS
namespace YouAPPiSDK.iOS
{
	public class YouAppiClientiOS : IYouAppiClient
	{
		#region Variable Hooks

		[DllImport ("__Internal")]
		private static extern System.IntPtr _environment ();

		[DllImport ("__Internal")]
		private static extern System.IntPtr _setEnvironment (string environment);

		[DllImport ("__Internal")]
		private static extern System.IntPtr _accessToken ();

		[DllImport ("__Internal")]
		private static extern System.IntPtr _setAccessToken (string token);

		[DllImport ("__Internal")]
		private static extern bool _isInitialized ();

		#endregion

		#region Method Hooks

		[DllImport ("__Internal")]
		private static extern System.IntPtr _youAppiShared ();

		[DllImport ("__Internal")]
		private static extern void _youAppiInitialize (string token);

		[DllImport ("__Internal")]
		private static extern void _setLogLevel (int logLevel);

		[DllImport ("__Internal")]
		private static extern void _showLog ();

		[DllImport ("__Internal")]
		private static extern System.IntPtr _interstitialAd (string adUnitId);

		[DllImport ("__Internal")]
		private static extern System.IntPtr _interstitialVideoAd (string adUnitId);

		[DllImport ("__Internal")]
		private static extern System.IntPtr _rewardedVideoAd (string adUnitId);

		#endregion

		#region Class Variables

		public static Dictionary<int, YAInterstitialAdClientiOS> ptrToInterstialAdDict;

		public string environment {
			get {
				return Marshal.PtrToStringAuto (_environment ());
			}
			set {
				_setEnvironment (value);
			}
		}

		public string accessToken {
			get {
				return Marshal.PtrToStringAuto (_accessToken ());
			}
			set {
				_setAccessToken (value);
			}
		}

		public bool isInitialized {
			get {
				return _isInitialized ();
			}
			set {

			}
		}

		// Pointer to iOS implementations, created on object construction.
		private System.IntPtr youAppiPtr;

		#endregion

		// Shared instance Constructor.
		public YouAppiClientiOS ()
		{
			Debug.Log ("Creating YouAppiClient for iOS");
			ptrToInterstialAdDict = new Dictionary<int, YAInterstitialAdClientiOS> ();

			this.youAppiPtr = _youAppiShared ();
		}

		// Initialize YouAppi shared instance with given token.
		public void initialize (string token)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_youAppiInitialize (token);
			}
		}

		// Set log level.
		public void setLogLevel (YALogLevel logLevel)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_setLogLevel ((int)logLevel);
			}
		}

		// Display log screen.
		public void showLog ()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_showLog ();
			}
		}

		// Create an interstitial ad.
		public YAInterstitialAd interstitialAd (string adUnitID)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return new YAInterstitialAd (new YAInterstitialAdClientiOS (_interstitialAd (adUnitID)));
			} else {
				return null;
			}
		}

		// Create an video ad.
		public YAInterstitialVideoAd interstitialVideoAd (string adUnitID)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return new YAInterstitialVideoAd (new YAAdInterstitialVideoClientiOS (_interstitialVideoAd (adUnitID)));
			} else {
				return null;
			}
		}

		// Create a rewarded video ad.
		public YARewardedVideoAd rewardedVideoAd (string adUnitID)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return new YARewardedVideoAd (new YARewardedVideoAdClientiOS (_rewardedVideoAd (adUnitID)));
			} else {
				return null;
			}
		}
	}
}
#endif
