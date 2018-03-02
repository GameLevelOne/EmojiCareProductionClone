using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouAPPiSDK.Api;

public class YouAppiManager : MonoBehaviour {
	private YouAppi youAppi; 
	private YAInterstitialAd interstitialAd;
    private YAInterstitialVideoAd interstitialVideoAd;
    private YARewardedVideoAd rewardedVideoAd;
    private static YouAppiManager instance;
    public static YouAppiManager Instance{ get { return instance; }}

	private string adUnitIDSuffix = "test_ad_unit_id";
	private string adUnitIDChosenPrefix = "";

	public delegate void YouAppiFinishWatchVideoAds(AdEvents eventName);
	public event YouAppiFinishWatchVideoAds OnYouAppiFinishWatchAds;

	AdEvents currentEvent;

    void Awake(){
    	if(instance != this && instance != null){
			Destroy (this.gameObject);
    	} else{
			instance = this;
    	}
		DontDestroyOnLoad (this.gameObject);
    }

	// Use this for initialization
	void Start () {
		InitYouAppi ();
	}

	void InitYouAppi(){
		youAppi = new YouAppi ();

		Debug.Log("You Appi TestClient Object Started. \n Initializing... 2");

		this.youAppi.initialize("a42ea166-1299-4a09-bfb7-a2370d4ec466");

		Debug.Log ("Access Token Received: " + this.youAppi.accessToken);
		Debug.Log ("Shared Instance Environment: " + this.youAppi.environment);
		Debug.Log ("isInitialized: " + this.youAppi.isInitialized);

	}

	#region Ad Requests

	public void showInterstitialAd()
	{
		this.interstitialAd = this.youAppi.interstitialAd("Interstitial" + this.adUnitIDChosenPrefix + this.adUnitIDSuffix);
        this.interstitialAd.LoadSuccess += (sender,AdUnitEventArg)=> { 
			this.interstitialAd.show(); 
		};
        this.interstitialAd.load();
    }

    public void showInterstitialVideoAd()
    {
		this.interstitialVideoAd = this.youAppi.interstitialVideo("InterstitialVideo" + this.adUnitIDChosenPrefix + this.adUnitIDSuffix);
        this.interstitialVideoAd.LoadSuccess+=(sender,AdUnitEventArg)=> {
            this.interstitialVideoAd.show();
        };
        this.interstitialVideoAd.load();
    }

    public void showRewardedVideoAd(AdEvents eventName)
    {
		currentEvent = eventName;
		this.rewardedVideoAd = this.youAppi.rewardedVideo("RewardedVideo" + this.adUnitIDChosenPrefix + this.adUnitIDSuffix);
        this.rewardedVideoAd.LoadSuccess += (sender, AdUnitEventArg) => {
            this.rewardedVideoAd.show();
        };
		this.rewardedVideoAd.LoadFailure += (sender, ErrorEventArgs) => {
			Debug.Log ("Failed to load rewarded video. ");
		};
		this.rewardedVideoAd.Rewarded += (sender,AdUnitEventArg) => {
			Debug.Log("reward user");
			if (OnYouAppiFinishWatchAds != null)
				OnYouAppiFinishWatchAds (currentEvent);
		};
        this.rewardedVideoAd.load ();
    }

	#endregion
}
