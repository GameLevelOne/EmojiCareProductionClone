using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupRefillStall : MonoBehaviour {
	public ScreenPopup screenPopup;

	void OnEnable(){
		screenPopup.ShowPopup (PopupType.AdsOrGems, PopupEventType.RefillStall);
		ScreenPopup.OnRefillStallWithAds += OnRefillStallWithAds;
		ScreenPopup.OnRefillStallWithGems += OnRefillStallWithGems;
		AdmobManager.Instance.OnFinishLoadVideoAds += OnFinishLoadVideoAds;
		AdmobManager.Instance.OnFinishWatchVideoAds += OnFinishWatchVideoAds;
	}

	void OnDisable(){
		ScreenPopup.OnRefillStallWithAds -= OnRefillStallWithAds;
		ScreenPopup.OnRefillStallWithGems -= OnRefillStallWithGems;
		AdmobManager.Instance.OnFinishLoadVideoAds -= OnFinishLoadVideoAds;
		AdmobManager.Instance.OnFinishWatchVideoAds -= OnFinishWatchVideoAds;
	}

	void OnRefillStallWithAds ()
	{
		Debug.Log ("ads");
	}

	void OnRefillStallWithGems ()
	{
		Debug.Log ("gems");
	}

	public void OnClickButtonAds(){
		//show loading?
		AdmobManager.Instance.ShowRewardedVideo ();
	}

	void OnFinishLoadVideoAds(){
		//disable loading
	}

	void OnFinishWatchVideoAds(){
		//refill event
	}

}
