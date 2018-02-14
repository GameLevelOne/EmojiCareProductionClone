using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayGamesManager : MonoBehaviour {
	private static GooglePlayGamesManager instance;

	public static GooglePlayGamesManager Instance{ get { return instance; } }

	// Use this for initialization
	void Start () {
		if(instance!=null && instance!=this){
			Destroy (this.gameObject);
		} else{
			instance = this;
		}
		DontDestroyOnLoad (this.gameObject);

		InitGPGS ();
	}
	
	void InitGPGS(){
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate ();
	}

	public void GPGSLogin(){
		Social.localUser.Authenticate((bool success) =>{
			Debug.Log("success login gpgs");
		});
	}
}
