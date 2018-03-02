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
    public class InterstitialVideoAdAndoridClient : InterstitialAdAndroidClient,IYAInterstitialVideoAdClient

    {
        public InterstitialVideoAdAndoridClient(AndroidJavaObject interstitialNativeObject,string nativeListenerClassName,string setListenerMethodName, IUIExecutor uiRunner) : base(interstitialNativeObject, nativeListenerClassName,setListenerMethodName, uiRunner)
        {
        }

        public event EventHandler<AdUnitEventArg> VideoStart;
        public event EventHandler<AdUnitEventArg> VideoEnd;
        public event EventHandler<VideoSkipEventArgs> VideoSkipped;

        /**
		 * This callback is called when a video ad starts playing.
		 */
       public void onVideoStart(string adUnitId) {
            Debug.Log("onVideoStart " + adUnitId+" "+this.VideoStart);
            if (this.VideoStart != null) {
                this.VideoStart(this,new AdUnitEventArg(adUnitId));
            }
                }

        /**
		 * This callback is called when a video ad has been watched to completion by the user.
		 */
       public  void onVideoEnd(string adUnitId) {
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
       public void onVideoSkipped(string adUnitId, int seconds) {
            if (this.VideoSkipped != null)
            {
                this.VideoSkipped(this, new VideoSkipEventArgs(adUnitId,seconds));
            }
        }
    }
}

#endif