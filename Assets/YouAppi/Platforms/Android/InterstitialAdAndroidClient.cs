using System;
using UnityEngine;
using YouAPPiSDK.Api;
using YouAPPiSDK.Common;
#if UNITY_ANDROID
namespace YouAPPiSDK.Android
{
	public class InterstitialAdAndroidClient: AndroidJavaProxy,IYAInterstitialAdClient
    {
        private AndroidJavaObject _interstitialAd;
        private IUIExecutor _uiRunner;

        public InterstitialAdAndroidClient(AndroidJavaObject interstitialNativeObject,string nativeListenerClassName,string setListenerMethodName,IUIExecutor uiRunner) :base(nativeListenerClassName)
        {
            this._interstitialAd = interstitialNativeObject;
            _interstitialAd.Call(setListenerMethodName, new object[] {this});
            _uiRunner = uiRunner;
        }

        public bool isAdAvailable()
        {
          return  _interstitialAd.Call<bool>("isAvailable", new object[] {});
        }

        public event EventHandler<AdUnitEventArg> AdStarted;
        public event EventHandler<AdUnitEventArg> AdEnded;
        public event EventHandler<ErrorEventArgs> LoadFailure;
        public event EventHandler<ErrorEventArgs> ShowFailure;
        public event EventHandler<ErrorEventArgs> PreloadFailed;
        public event EventHandler<AdUnitEventArg> LoadSuccess;
        public event EventHandler<AdUnitEventArg> CardShow;
        public event EventHandler<AdUnitEventArg> CardClose;
        public event EventHandler<AdUnitEventArg> CardClick;
        public event EventHandler<AdUnitEventArg> CardWillLeaveApplication;

        public void loadAd()
        {
            this._uiRunner.runOnUI(() =>
            {
                _interstitialAd.Call("load", new object[] { });
            });
        }

        public void showAd()
        {
            this._uiRunner.runOnUI(() =>
            {
                _interstitialAd.Call<bool>("show", new object[] { });
            });
        }

        /**
	 * This callback is called when the ad was successfully loaded, and ready to be shown
	 *
	 * @param adUnitId
	 */
        public void onLoadSuccess(string adUnitId) {
            if (this.LoadSuccess!=null)
            {
                this.LoadSuccess(this,new AdUnitEventArg(adUnitId));
            }
        }

        /**
         * * This callback will be called when Ad has failed to load.
         *
         * @param adUnitId
         * @param yaErrorCode
         * @param e           An exception related to the load error or Null in case the error does not contain an exception.
         */
        public void onLoadFailure(string adUnitId, AndroidJavaObject yaErrorCode, AndroidJavaObject e) {
            if (this.LoadFailure != null)
            {
                int errorCodeOrdinal = yaErrorCode.Call<int>("ordinal");
                string msg = "";
                if (e != null)
                {
                    msg = e.Call<string>("getMessage");
                }
                this.LoadFailure(this, new ErrorEventArgs(adUnitId,(YAErrorCode)errorCodeOrdinal,msg));
            }
        }


        /**
         * * This callback will be called when Ad has failed due to show.
         *
         * @param adUnitId
         * @param yaErrorCode
         * @param e           An exception related to the load error or Null in case the error does not contain an exception.
         */
        public void onShowFailure(string adUnitId, AndroidJavaObject yaErrorCode, AndroidJavaObject e) {
            if (this.ShowFailure != null)
            {
                int errorCodeOrdinal=yaErrorCode.Call<int>("ordinal");
                string msg = "";
                if (e != null)
                {
                    msg = e.Call<string>("getMessage");
                }
                this.ShowFailure(this, new ErrorEventArgs(adUnitId, (YAErrorCode)errorCodeOrdinal, msg));
            }
        }

        /**
         * This callback is invoked when the Ad is shown
         *
         * @param adUnitId
         */
        public void onAdStarted(string adUnitId) {
            if (this.AdStarted != null)
            {
                this.AdStarted(this,new AdUnitEventArg(adUnitId));
            }
        }

        /**
         * This callback is invoked when the Ad is closed
         *
         * @param adUnitId
         */
       public void onAdEnded(string adUnitId) {
            if (this.AdEnded != null)
            {
                this.AdEnded(this, new AdUnitEventArg(adUnitId));
            }
        }

       public void onCardShow(string adUnitId)
        {
            if (this.CardShow!=null)
            {
                this.CardShow(this, new AdUnitEventArg(adUnitId));
            }
        }

       public void onCardClose(string adUnitId)
        {
            if (this.CardClose != null)
            {
                this.CardClose(this, new AdUnitEventArg(adUnitId));
            }
        }

        public void onCardClick(string adUnitId)
        {
            if (this.CardClick != null)
            {
                this.CardClick(this, new AdUnitEventArg(adUnitId));
            }
        }

    }
}
#endif

