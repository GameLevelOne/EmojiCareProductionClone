using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using admob;
using GoogleMobileAds.Api;

public enum AdEvents{
	RestockStall,
	RestockSeeds,
	ShuffleEmoji,
	SpeedUpPlant,
	WakeEmojiUp,
	FreeCoin,
	FreeGem
}

public class AdmobManager : MonoBehaviour {
	static AdmobManager instance;

	#region adsIds
	//TODO: REPLACE WITH PRODUCTION IDs LATER
	string androidBannerID = "ca-app-pub-3940256099942544/6300978111";
	string iosBannerID;
	string androidRewardedVideoID = "ca-app-pub-3940256099942544/5224354917";
	string iosRewardedVideoID;
	#endregion

	#region events
	public delegate void FinishLoadVideoAds();
	public delegate void FinishWatchVideoAds(AdEvents eventName);
	public event FinishLoadVideoAds OnFinishLoadVideoAds;
	public event FinishWatchVideoAds OnFinishWatchVideoAds;
	#endregion

	//Admob ad;
	AdEvents currentEvent;
	bool videoAdsReady = false;

	private RewardBasedVideoAd rewardedVideo;
	private BannerView bannerView;

	public static AdmobManager Instance{
		get{return instance;}
	}

	void Awake(){
		if(instance != null && instance != this){
			Destroy(this.gameObject);
		} else{
			instance=this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	void Start(){
		InitAdmob();
	}

	void OnDisable(){
		//ad.rewardedVideoEventHandler -= rewardedVideoEventHandler;
		rewardedVideo.OnAdLoaded -= OnAdLoaded;
		rewardedVideo.OnAdRewarded -= OnAdRewarded;
		bannerView.Destroy ();
	}

	void InitAdmob(){
//		ad = Admob.Instance();
//
//		#if UNITY_ANDROID
//		ad.initAdmob(androidBannerID,"");
//		ad.loadRewardedVideo (androidRewardedVideoID);
//		#endif
//
//		#if UNITY_IOS
//		ad.initAdmob(iosBannerID,"");
//		ad.loadRewardedVideo (iosRewardedVideoID);
//		#endif
//
//		ad.rewardedVideoEventHandler += rewardedVideoEventHandler;
		
//		MobileAds.Initialize ("ca-app-pub-3940256099942544~3347511713"); //temp AppID
		rewardedVideo = RewardBasedVideoAd.Instance;
		rewardedVideo.OnAdLoaded += OnAdLoaded;
		rewardedVideo.OnAdRewarded += OnAdRewarded;
		RequestBannerAd ();
	}

	void OnAdLoaded (object sender, System.EventArgs e)
	{
		if(rewardedVideo.IsLoaded()) videoAdsReady = true;
	}

	void OnAdRewarded (object sender, Reward e)
	{
		videoAdsReady = false;
		if(OnFinishWatchVideoAds!=null) OnFinishWatchVideoAds (currentEvent);
	}

	void rewardedVideoEventHandler (string eventName, string msg)
	{
//		Debug.Log ("eventName:" + eventName + " msg:" + msg);
//		if(eventName == AdmobEvent.onRewarded){
//			videoAdsReady = false;
//			OnFinishWatchVideoAds (currentEvent);
//		} else if(eventName == AdmobEvent.onAdLoaded){
//			if(ad.isRewardedVideoReady()){
//				videoAdsReady = true;
//			}
//		}
	}

	void RequestBannerAd(){
		bannerView = new BannerView (androidBannerID, AdSize.SmartBanner, AdPosition.Bottom);
		AdRequest req = new AdRequest.Builder ().Build ();
		bannerView.LoadAd (req);
		HideBanner ();
	}

	public void RequestRewardedVideo(){
		// Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        rewardedVideo.LoadAd(request, androidRewardedVideoID);
	}

	public void ShowBanner(){
		#if UNITY_EDITOR
		Debug.Log("show banner");
		#endif
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name == ShortCode.SCENE_MAIN)
			//ad.showBannerRelative(AdSize.SmartBanner,AdPosition.BOTTOM_CENTER,0);
			bannerView.Show ();
	}

	public void HideBanner(){
		#if UNITY_EDITOR
		Debug.Log("hide banner");
		#endif
		bannerView.Hide ();
	}

	public void ShowRewardedVideo(AdEvents eventName){
		currentEvent = eventName;
		//ad.loadRewardedVideo (androidRewardedVideoID);
		RequestRewardedVideo ();
		StartCoroutine (WaitForAds ());
	}

	IEnumerator WaitForAds(){
		bool adsReady = false;
		Debug.Log ("Start waiting. adsReady:" + adsReady);
		while(!adsReady){ 
			if(videoAdsReady){
				adsReady = true;
			}
			Debug.Log ("adsReady:" + adsReady);
			yield return null;
		}
		if(adsReady){
			Debug.Log ("ads is ready, playing video");
			adsReady = false;
			if(OnFinishLoadVideoAds!=null) OnFinishLoadVideoAds ();
			//ad.showRewardedVideo ();
			rewardedVideo.Show ();
		}
	}

}
