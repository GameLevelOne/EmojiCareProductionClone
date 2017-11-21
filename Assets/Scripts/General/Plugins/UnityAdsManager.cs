using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public enum AdsEventType{
	RefillStall
}

public class UnityAdsManager : MonoBehaviour {
	static UnityAdsManager instance;
	string adsPlacement = "rewardedVideo";

	AdsEventType currentEventType;

	public delegate void RefillStall();
	public static event RefillStall OnRefillStall;

	public static UnityAdsManager Instance{
		get{return instance;}
	}

	void Awake(){
		if(instance != null && instance != this){
			Destroy (this.gameObject);
		} else{
			instance = this;

		}
		DontDestroyOnLoad (this.gameObject);
	}

	public void ShowAds(AdsEventType eventType){
		currentEventType = eventType;
		ShowOptions options = new ShowOptions ();
		options.resultCallback = HandleShowResult;

		Advertisement.Show (adsPlacement,options);
		//StartCoroutine (WaitForAds (options));
	}

	void HandleShowResult (ShowResult result)
	{
		if(result == ShowResult.Finished){
			Debug.Log ("give reward");
			ProcessReward ();
		} else if(result == ShowResult.Skipped){
			Debug.Log ("video skipped");
		} else if(result == ShowResult.Failed){
			Debug.Log ("video failed to show");
		}
	}

	void ProcessReward(){
		if(currentEventType == AdsEventType.RefillStall){
			OnRefillStall ();
		}
	}

	IEnumerator WaitForAds(ShowOptions options){
		bool adsReady = false;

		while(!adsReady){
			if(Advertisement.IsReady()){
				adsReady = true;
			}
			yield return null;
		}

		if(adsReady){
			Advertisement.Show (adsPlacement,options);
		}
	}


}
