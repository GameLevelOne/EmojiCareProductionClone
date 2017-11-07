using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using admob;

public class AdmobManager : MonoBehaviour {
	static AdmobManager instance;
	string androidBannerID = "ca-app-pub-3940256099942544/6300978111";
	string iosBannerID;

	Admob ad;

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

	void OnEnable(){
	}

	void InitAdmob(){
		ad = Admob.Instance();
		#if UNITY_ANDROID
		ad.initAdmob(androidBannerID,"");
		#endif

		#if UNITY_IOS
		ad.initAdmob(iosBannerID,"");
		#endif

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
}
