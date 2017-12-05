using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using admob;

public enum AdEvents{
	RestockStall,
	RestockSeeds,
	ShuffleEmoji
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

	Admob ad;
	AdEvents currentEvent;

	public static AdmobManager Instance{
		get{return instance;}
	}

	void Start(){
		if(instance != null && instance != this){
			Destroy(this.gameObject);
		} else{
			instance=this;
		}
		DontDestroyOnLoad(this.gameObject);

		InitAdmob();
	}

	void OnDisable(){
		ad.rewardedVideoEventHandler -= rewardedVideoEventHandler;
	}

	void InitAdmob(){
		ad = Admob.Instance();

		#if UNITY_ANDROID
		ad.initAdmob(androidBannerID,"");
		ad.loadRewardedVideo (androidRewardedVideoID);
		#endif

		#if UNITY_IOS
		ad.initAdmob(iosBannerID,"");
		ad.loadRewardedVideo (iosRewardedVideoID);
		#endif

		ad.rewardedVideoEventHandler += rewardedVideoEventHandler;
	}

	void rewardedVideoEventHandler (string eventName, string msg)
	{
		if(eventName == AdmobEvent.onRewarded){
			OnFinishWatchVideoAds (currentEvent);
			ad.loadRewardedVideo (androidRewardedVideoID);
		}
	}

	public void ShowBanner(){
		#if UNITY_EDITOR
		Debug.Log("show banner");
		#endif
		ad.showBannerRelative(AdSize.SmartBanner,AdPosition.BOTTOM_CENTER,0);
	}

	public void HideBanner(){
		#if UNITY_EDITOR
		Debug.Log("hide banner");
		#endif
		ad.removeBanner();
	}

	public void ShowRewardedVideo(AdEvents eventName){
		currentEvent = eventName;
		StartCoroutine (WaitForAds ());
	}

	IEnumerator WaitForAds(){
		bool adsReady = false;
		while(!adsReady){
			if(ad.isRewardedVideoReady()){
				adsReady = true;
			}
			yield return null;
		}


		if(adsReady){
			adsReady = false;
			//OnFinishLoadVideoAds ();
			ad.showRewardedVideo ();
		}
	}

}
