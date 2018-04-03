using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif
using UnityEngine.SocialPlatforms;

public class GooglePlayGamesManager : MonoBehaviour {
	private static GooglePlayGamesManager instance;

	public static GooglePlayGamesManager Instance{ get { return instance; } }

	public delegate void FinishLogin();
	public event FinishLogin OnFinishLogin;

	// Use this for initialization
	void Awake () {
		if(instance!=null && instance!=this){
			Destroy (this.gameObject);
		} else{
			instance = this;
		}
		DontDestroyOnLoad (this.gameObject);
	}

	#if UNITY_ANDROID
	void Start(){
		InitGPGS ();
	}

	void InitGPGS(){
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate ();
	}
	#endif

	public void GPGSLogin(){
		Social.localUser.Authenticate((bool success) =>{
			Debug.Log("success login gpgs");
			if(OnFinishLogin!=null) OnFinishLogin();
		});
	}
}
