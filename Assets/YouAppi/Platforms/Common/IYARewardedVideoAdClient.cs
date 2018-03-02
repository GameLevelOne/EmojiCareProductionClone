using System;
using YouAPPiSDK.Api;

namespace YouAPPiSDK.Common
{

    public interface IYARewardedVideoAdClient:IYAInterstitialVideoAdClient
	{
        event EventHandler<AdUnitEventArg> Rewarded;
    }
}