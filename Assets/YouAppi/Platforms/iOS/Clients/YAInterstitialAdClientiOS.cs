using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using System;
using YouAPPiSDK.Api;
using YouAPPiSDK.Common;

#if UNITY_IOS
namespace YouAPPiSDK.iOS
{
	public class YAInterstitialAdClientiOS : IYAInterstitialAdClient
	{
		#region iOS Hooks

		[DllImport ("__Internal")]
		protected static extern bool _isAdAvailable (System.IntPtr adPtr);

		[DllImport ("__Internal")]
		protected static extern void _loadAd (System.IntPtr adPtr);

		[DllImport ("__Internal")]
		protected static extern void _showAd (System.IntPtr adPtr);

		[DllImport ("__Internal")]
		protected static extern void _subscribeToAdCallbacks (System.IntPtr adPtr, _LoadAdSuccess onAdLoadSuccess,
			_LoadAdFail onAdLoadFail, _AdStarted onAdStart, _AdEnded onAdEnd, _ShowFailure onAdShowFail, _CardShow onAdShow, 
			_CardClose onAdClose, _VideoStarted onAdWillLeaveApplication, _VideoEnded onPreloadFail, _Rewarded onRewarded);

		[DllImport ("__Internal")]
		protected static extern void _unsubscribeFromAdCallbacks (System.IntPtr adPtr);
		
		#endregion

		#region iOS Callback Definitions

		public delegate void _LoadAdSuccess (System.IntPtr adPtr, string adUnitID);
		public delegate void _LoadAdFail (System.IntPtr adPtr, string adUnitID, int errorCode, string errorMessage);
		public delegate void _AdStarted (System.IntPtr adPtr, string adUnitID);
		public delegate void _AdEnded (System.IntPtr adPtr, string adUnitID);
		public delegate void _ShowFailure (System.IntPtr adPtr, string adUnitID, int errorCode, string errorMessage);
		public delegate void _CardShow (System.IntPtr adPtr, string adUnitID);
		public delegate void _CardClose (System.IntPtr adPtr, string adUnitID);

		public delegate void _VideoStarted (System.IntPtr adPtr, string adUnitID);
		public delegate void _VideoEnded (System.IntPtr adPtr, string adUnitID);

		public delegate void _Rewarded (System.IntPtr adPtr, string adUnitID);

		#endregion

		#region Event Handler Definitions

		public event EventHandler<AdUnitEventArg> AdStarted;
		public event EventHandler<AdUnitEventArg> AdEnded;
		public event EventHandler<ErrorEventArgs> LoadFailure;
		public event EventHandler<ErrorEventArgs> ShowFailure;
		public event EventHandler<AdUnitEventArg> LoadSuccess;
		public event EventHandler<AdUnitEventArg> CardShow;
		public event EventHandler<AdUnitEventArg> CardClose;
		public event EventHandler<AdUnitEventArg> CardClick;

		#endregion

		protected System.IntPtr adPtr;

		public YAInterstitialAdClientiOS ()
		{
		}

		public YAInterstitialAdClientiOS (System.IntPtr adPtr)
		{
			this.adPtr = adPtr;

			//IntPtr classHandle = Class.GetHandle("NSObject");
			//var selector = new Selector("alloc");


			Debug.Log ("Subscribing to Ad Load Success");
			YouAppiClientiOS.ptrToInterstialAdDict [this.adPtr.GetHashCode ()] = this;
			_subscribeToAdCallbacks (this.adPtr, didLoadSuccess, didLoadFail, didAdStarted, didAdEnded, 
				didshowFailure, didCardShow, didCardClose, null, null, null);
		}

		~YAInterstitialAdClientiOS()
		{
			_unsubscribeFromAdCallbacks (this.adPtr);
		}

		public bool isAdAvailable ()
		{
			return _isAdAvailable (adPtr);
		}

		public void loadAd ()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_loadAd (this.adPtr);
			}
		}

		public void showAd ()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_showAd (adPtr);
			}
		}

		#region iOS Callback Implementations

		[MonoPInvokeCallbackAttribute (typeof(_LoadAdFail))]
		protected static void didLoadFail (System.IntPtr adPtr, string adUnitID, int errorCode, string errorMessage)
		{
			Debug.Log ("Failed to load ad, firing unity event. UnitID: " + adUnitID + ", errorCode: " + errorCode + ", errorMessage: " + errorMessage);
			if (YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()] != null) {
				YAInterstitialAdClientiOS ad = YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()];
				ad.onLoadFailure (adUnitID, (YAErrorCode) errorCode, new System.Exception(errorMessage));
			}
		}

		[MonoPInvokeCallbackAttribute (typeof(_LoadAdSuccess))]
		protected static void didLoadSuccess (System.IntPtr adPtr, string adUnitID)
		{
			Debug.Log ("Ad loaded successfully, firing unity event." + adPtr + adUnitID + YouAppiClientiOS.ptrToInterstialAdDict);
			if (YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()] != null) {
				YAInterstitialAdClientiOS ad = YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()];
				Debug.Log ("Found coresponding ad:" + ad);
				ad.onLoadSuccess (adUnitID);
			} else {
				Debug.Log ("Failed to get corresponding ad pointer.");
			}
		}

		[MonoPInvokeCallbackAttribute (typeof(_AdStarted))]
		protected static void didAdStarted (System.IntPtr adPtr, string adUnitID)
		{
			Debug.Log ("Ad Started");
			if (YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()] != null) {
				YAInterstitialAdClientiOS ad = YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()];
				Debug.Log ("Found coresponding ad:" + ad);
				ad.onAdStarted (adUnitID);
			} else {
				Debug.Log ("Failed to get corresponding ad pointer.");
			}
		}

		[MonoPInvokeCallbackAttribute (typeof(_AdEnded))]
		protected static void didAdEnded (System.IntPtr adPtr, string adUnitID)
		{
			Debug.Log ("Ad Ended");
			if (YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()] != null) {
				YAInterstitialAdClientiOS ad = YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()];
				Debug.Log ("Found coresponding ad:" + ad);
				ad.onAdEnded (adUnitID);
			} else {
				Debug.Log ("Failed to get corresponding ad pointer.");
			}
		}

		[MonoPInvokeCallbackAttribute (typeof(_ShowFailure))]
		protected static void didshowFailure (System.IntPtr adPtr, string adUnitID, int errorCode, string errorMessage)
		{
			Debug.Log ("Show Failure, UnitID: " + adUnitID + ", errorCode: " + errorCode + ", errorMessage: " + errorMessage);
			if (YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()] != null) {
				YAInterstitialAdClientiOS ad = YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()];
				Debug.Log ("Found coresponding ad:" + ad);
				ad.onShowFailure (adUnitID, (YAErrorCode) errorCode, new System.Exception(errorMessage));
			} else {
				Debug.Log ("Failed to get corresponding ad pointer.");
			}
		}

		[MonoPInvokeCallbackAttribute (typeof(_CardShow))]
		protected static void didCardShow (System.IntPtr adPtr, string adUnitID)
		{
			Debug.Log ("Did show ad.");
			if (YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()] != null) {
				YAInterstitialAdClientiOS ad = YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()];
				Debug.Log ("Found coresponding ad:" + ad);
				ad.onCardShow (adUnitID);
			} else {
				Debug.Log ("Failed to get corresponding ad pointer.");
			}
		}

		[MonoPInvokeCallbackAttribute (typeof(_CardClose))]
		protected static void didCardClose (System.IntPtr adPtr, string adUnitID)
		{
			Debug.Log ("Did close ad.");
			if (YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()] != null) {
				YAInterstitialAdClientiOS ad = YouAppiClientiOS.ptrToInterstialAdDict [adPtr.GetHashCode ()];
				Debug.Log ("Found coresponding ad:" + ad);
				ad.onCardClose (adUnitID);
				_unsubscribeFromAdCallbacks (ad.adPtr);
			} else {
				Debug.Log ("Failed to get corresponding ad pointer.");
			}
		}

		#endregion

		#region Event Method Implementations
		/**
		 * This callback is called when the ad was successfully loaded, and ready to be shown
	 	 *
	 	 * @param adUnitId
	 	 */
		public void onLoadSuccess (String adUnitId)
		{
			Debug.Log ("OnLoadSuccess firing events.");
			if (this.LoadSuccess != null) {
				this.LoadSuccess (this, new AdUnitEventArg (adUnitId));
			}
		}

		/**
         * * This callback will be called when Ad has failed to load.
         *
         * @param adUnitId
         * @param yaErrorCode
         * @param e           An exception related to the load error or Null in case the error does not contain an exception.
         */
		public void onLoadFailure (String adUnitId, YAErrorCode yaErrorCode, Exception e)
		{
			if (this.LoadFailure != null) {
				this.LoadFailure (this, new ErrorEventArgs (adUnitId, yaErrorCode, e.Message));
			}
		}


		/**
         * * This callback will be called when Ad has failed due to show.
         *
         * @param adUnitId
         * @param yaErrorCode
         * @param e           An exception related to the load error or Null in case the error does not contain an exception.
         */
		public void onShowFailure (String adUnitId, YAErrorCode yaErrorCode, Exception e)
		{
			if (this.ShowFailure != null) {
				this.ShowFailure (this, new ErrorEventArgs (adUnitId, yaErrorCode, e.Message));
			}
		}

		/**
         * This callback is invoked when the Ad is shown
         *
         * @param adUnitId
         */
		public void onAdStarted (String adUnitId)
		{
			if (this.AdStarted != null) {
				this.AdStarted (this, new AdUnitEventArg (adUnitId));
			}
		}

		/**
         * This callback is invoked when the Ad is closed
         *
         * @param adUnitId
         */
		public void onAdEnded (String adUnitId)
		{
			if (this.AdEnded != null) {
				this.AdEnded (this, new AdUnitEventArg (adUnitId));
			}
		}

		public void onCardShow (String adUnitId)
		{
			if (this.CardShow != null) {
				this.CardShow (this, new AdUnitEventArg (adUnitId));
			}
		}

		public void onCardClose (String adUnitId)
		{
			if (this.CardClose != null) {
				this.CardClose (this, new AdUnitEventArg (adUnitId));
			}
		}

		public void onCardClick (String adUnitId)
		{
			if (this.CardClick != null) {
				this.CardClick (this, new AdUnitEventArg (adUnitId));
			}
		}

		#endregion
	}
}
#endif