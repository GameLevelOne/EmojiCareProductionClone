using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using admob;

public class AdmobManager : MonoBehaviour {
	static AdmobManager instance;
	string androidBannerID;
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
		ad.showBannerRelative(AdSize.SmartBanner,AdPosition.BOTTOM_CENTER,0);
	}

	public void HideBanner(){
		ad.removeBanner();
	}
}
