using System;
using YouAPPiSDK.Api;

namespace YouAPPiSDK.Common
{
    public interface IYAInterstitialAdClient
	{
        bool isAdAvailable();

		void loadAd ();

		void showAd ();

        event EventHandler<AdUnitEventArg> AdStarted;
        event EventHandler<AdUnitEventArg> AdEnded;
        event EventHandler<ErrorEventArgs> LoadFailure;
        event EventHandler<ErrorEventArgs> ShowFailure;
        event EventHandler<AdUnitEventArg> LoadSuccess;
        event EventHandler<AdUnitEventArg> CardShow;
        event EventHandler<AdUnitEventArg> CardClose;
        event EventHandler<AdUnitEventArg> CardClick;
    }
}