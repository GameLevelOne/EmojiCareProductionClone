using YouAPPiSDK.Api;

namespace YouAPPiSDK.Common
{
	public interface IYouAppiClient
	{
		string environment { get; set; }

		string accessToken { get; set; }

		bool isInitialized { get; set; }

		void initialize (string token);

		void showLog ();

		void setLogLevel (YALogLevel level);

		YAInterstitialAd interstitialAd (string adUnitID);

		YAInterstitialVideoAd interstitialVideoAd (string adUnitID);

		YARewardedVideoAd rewardedVideoAd (string adUnitID);
	}
}