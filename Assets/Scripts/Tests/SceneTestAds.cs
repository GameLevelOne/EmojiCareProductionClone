using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTestAds : MonoBehaviour {

	public Text buttonVideoAds;

	void OnEnable(){
		AdmobManager.Instance.OnFinishLoadVideoAds += OnFinishLoadVideoAds;
		AdmobManager.Instance.OnFinishWatchVideoAds += OnFinishWatchVideoAds;
	}

	void OnDisable(){
		AdmobManager.Instance.OnFinishLoadVideoAds -= OnFinishLoadVideoAds;
		AdmobManager.Instance.OnFinishWatchVideoAds -= OnFinishWatchVideoAds;
	}

	void OnFinishLoadVideoAds ()
	{
		buttonVideoAds.text = "Video Loaded";
	}

	void OnFinishWatchVideoAds (AdEvents eventName)
	{
		buttonVideoAds.text = "Got reward";
	}

	public void OnClickVideoAds(){
		buttonVideoAds.text = "Loading...";
		AdmobManager.Instance.ShowRewardedVideo (AdEvents.RestockSeeds);
	}
}
