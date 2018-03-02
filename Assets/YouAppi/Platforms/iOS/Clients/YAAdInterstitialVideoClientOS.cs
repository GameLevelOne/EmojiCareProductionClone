using UnityEngine;
using System;
using System.Runtime.InteropServices;
using AOT;
using YouAPPiSDK.Api;
using YouAPPiSDK.Common;

#if UNITY_IOS
namespace YouAPPiSDK.iOS
{
	public class YAAdInterstitialVideoClientiOS: YAInterstitialAdClientiOS, IYAInterstitialVideoAdClient
	{
		#region Event Handler Definitions

		public event EventHandler<AdUnitEventArg> VideoStart;
		public event EventHandler<AdUnitEventArg> VideoEnd;
		public event EventHandler<VideoSkipEventArgs> VideoSkipped;

		#endregion


		public YAAdInterstitialVideoClientiOS (){}

		public YAAdInterstitialVideoClientiOS (System.IntPtr adPtr)
		{
			this.adPtr = adPtr;
			Debug.Log ("Subscribing to Ad Load Success");
			YouAppiClientiOS.ptrToInterstialAdDict [this.adPtr.GetHashCode ()] = this;
			_subscribeToAdCallbacks (this.adPtr, didLoadSuccess, didLoadFail, didAdStarted, didAdEnded, 
				didshowFailure, didCardShow, didCardClose, didVideoStart, didVideoEnd, null);
		}

		~YAAdInterstitialVideoClientiOS ()
		{
			_unsubscribeFromAdCallbacks (this.adPtr);
		}

		#region iOS Callback Implementations

		[MonoPInvokeCallbackAttribute (typeof(_VideoStarted))]
		protected static void didVideoStart (System.IntPtr adPtr, string adUnitID)
		{
			Debug.Log ("Video Ad loaded successfully, firing unity event." + adPtr + adUnitID + YouAppiClientiOS.ptrToInterstialAdDict);
			if (YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()] != null) {
				YAAdInterstitialVideoClientiOS ad = (YAAdInterstitialVideoClientiOS) YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()];
				Debug.Log ("Found coresponding Video Ad:" + ad);
				ad.onVideoEnd (adUnitID);
			} else {
				Debug.Log ("Failed to get corresponding video ad pointer.");
			}
		}

		[MonoPInvokeCallbackAttribute (typeof(_VideoEnded))]
		protected static void didVideoEnd (System.IntPtr adPtr, string adUnitID)
		{
			Debug.Log ("Video Ad Started");
			if (YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()] != null) {
				YAAdInterstitialVideoClientiOS ad = (YAAdInterstitialVideoClientiOS) YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()];
				Debug.Log ("Found coresponding Video Ad:" + ad);
				ad.onVideoStart (adUnitID);
			} else {
				Debug.Log ("Failed to get corresponding video ad pointer.");
			}
		}

		#endregion

		#region Event Implementations

		/**
		 * This callback is called when a video ad starts playing.
		 */
		public void onVideoStart(String adUnitId) {
			if (this.VideoStart != null) {
				this.VideoStart(this,new AdUnitEventArg(adUnitId));
			}
		}

		/**
		 * This callback is called when a video ad has been watched to completion by the user.
		 */
		public  void onVideoEnd(String adUnitId) {
			if (this.VideoEnd != null)
			{
				this.VideoEnd(this, new AdUnitEventArg(adUnitId));
			}
		}

		/**
		 * This callback is called when the user skipped the video
		 *
		 * @param seconds - the video position in seconds when the user pressed skip
		 */
		public void onVideoSkipped(String adUnitId, int seconds) {
			if (this.VideoSkipped != null)
			{
				this.VideoSkipped(this, new VideoSkipEventArgs(adUnitId,seconds));
			}
		}

		#endregion
	}
}
#endif
