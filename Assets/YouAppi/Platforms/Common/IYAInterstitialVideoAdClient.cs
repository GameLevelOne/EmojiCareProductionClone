using System;
using YouAPPiSDK.Api;

namespace YouAPPiSDK.Common
{

    public interface IYAInterstitialVideoAdClient:IYAInterstitialAdClient
	{

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