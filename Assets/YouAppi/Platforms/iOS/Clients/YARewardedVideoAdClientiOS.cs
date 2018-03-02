using UnityEngine;
using System.Runtime.InteropServices;
using AOT;
using System;
using YouAPPiSDK.Api;
using YouAPPiSDK.Common;

#if UNITY_IOS
namespace YouAPPiSDK.iOS
{
	public class YARewardedVideoAdClientiOS : YAAdInterstitialVideoClientiOS, IYARewardedVideoAdClient
	{
		#region Event Handler Definitions

		public event EventHandler<AdUnitEventArg> Rewarded;

		#endregion

		public YARewardedVideoAdClientiOS() {}

		public YARewardedVideoAdClientiOS (System.IntPtr adPtr)
		{
			this.adPtr = adPtr;
			Debug.Log ("Subscribing to Ad Load Success");
			YouAppiClientiOS.ptrToInterstialAdDict [this.adPtr.GetHashCode ()] = this;
			_subscribeToAdCallbacks (this.adPtr, didLoadSuccess, didLoadFail, didAdStarted, didAdEnded, 
				didshowFailure, didCardShow, didCardClose, didVideoStart, didVideoEnd, didReceiveRewarded);
		}

		~YARewardedVideoAdClientiOS()
		{
			_unsubscribeFromAdCallbacks (this.adPtr);
		}

		#region

		[MonoPInvokeCallbackAttribute (typeof(_Rewarded))]
		protected static void didReceiveRewarded (System.IntPtr adPtr, string adUnitID)
		{
			Debug.Log ("On rewarded Ad");
			if (YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()] != null) {
				YARewardedVideoAdClientiOS ad = (YARewardedVideoAdClientiOS) YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()];
				Debug.Log ("Found coresponding rewarded video Ad:" + ad);
				ad.onRewarded (adUnitID);
			} else {
				Debug.Log ("Failed to get corresponding rewarded video ad pointer.");
			}
		}

		#endregion

		#region Event Implementations

		public void onRewarded(String adUnitId) 
		{
			if (this.Rewarded!=null) {
				this.Rewarded(this,new AdUnitEventArg(adUnitId));
			}
		}

		#endregion
	}
}
#endif
