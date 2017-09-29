using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
	static PlayerData instance;
	public static PlayerData Instance{ get{return instance;} }

	public string AuthToken{
		get{return PlayerPrefs.GetString(PlayerPrefKeys.Player.PLAYER_AUTH_TOKEN);}
		set{PlayerPrefs.SetString(PlayerPrefKeys.Player.PLAYER_AUTH_TOKEN,value);}
	}

	void Awake()
	{
		if(instance != null && instance != this) Destroy(this.gameObject);
		else instance = this;
		DontDestroyOnLoad(this.gameObject);
	}
}
