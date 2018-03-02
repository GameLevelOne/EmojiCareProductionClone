using System;
using YouAPPiSDK.Api;
using YouAPPiSDK.Common;

namespace YouAPPiSDK.Api
{
   
    public class YAInterstitialVideoAd : YAInterstitialAd
    {

        public YAInterstitialVideoAd(IYAInterstitialVideoAdClient adClient) : base(adClient)
        {
            adClient.VideoStart += (sender, args) =>
            {
                if (this.VideoStart != null)
                {
                    this.VideoStart(sender, args);
                }
            };
            adClient.VideoEnd += (sender, args) =>
            {
                if (this.VideoEnd != null)
                {
                    this.VideoEnd(sender, args);
                }
            };
            adClient.VideoSkipped += (sender, args) =>
            {
                if (this.VideoSkipped != null)
                {
                    this.VideoSkipped(sender, args);
                }
            };
        }

        /**
       * This callback is called when a video ad starts playing.
       */
        event EventHandler<AdUnitEventArg> VideoStart;

        /**
		 * This callback is called when a video ad has been watched to completion by the user.
		 */
        event EventHandler<AdUnitEventArg> VideoEnd;

        /**
		 * This callback is called when the user skipped the video
		 *
		 * @param seconds - the video position in seconds when the user pressed skip
		 */
        event EventHandler<VideoSkipEventArgs> VideoSkipped;
    }
}
