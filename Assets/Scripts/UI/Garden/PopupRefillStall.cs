using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupRefillStall : MonoBehaviour {
	public ScreenPopup screenPopup;

	void OnEnable(){
		screenPopup.ShowPopup (PopupType.AdsOrGems, PopupEventType.RefillStall);
		ScreenPopup.OnRefillStallWithAds += OnRefillStallWithAds;
		ScreenPopup.OnRefillStallWithGems += OnRefillStallWithGems;
	}

	void OnDisable(){
		ScreenPopup.OnRefillStallWithAds -= OnRefillStallWithAds;
		ScreenPopup.OnRefillStallWithGems -= OnRefillStallWithGems;
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
		UnityAdsManager.Instance.ShowAds (AdsEventType.RefillStall);
	}
	

}
