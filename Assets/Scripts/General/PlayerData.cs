using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class PlayerData : MonoBehaviour {
	static PlayerData instance;
	public static PlayerData Instance{ get{return instance;} }

	int defaultCoin = 100;
	int defulatGem = 0;

	public Transform emojiParentTransform;

	public int playerEmojiType = 0;

	Emoji playerEmoji;

	bool hasInitEmojiObject = false;

	public Emoji PlayerEmoji{
		get{return playerEmoji;}
	}

	public int PlayerCoin{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.PLAYER_COIN,defaultCoin);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.PLAYER_COIN,value);}
	}

	public int PlayerGem{
		get{return PlayerPrefs.GetInt(PlayerPrefKeys.Player.PLAYER_GEM,defulatGem);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.PLAYER_GEM,value);}
	}

	public string PlayerAuthToken{
		get{return PlayerPrefs.GetString(PlayerPrefKeys.Player.PLAYER_AUTH_TOKEN);}
		set{PlayerPrefs.SetString(PlayerPrefKeys.Player.PLAYER_AUTH_TOKEN,value);}
	}

	void Awake()
	{
		if(instance != null && instance != this) Destroy(this.gameObject);
		else instance = this;
		DontDestroyOnLoad(this.gameObject);
	}

	public void InitPlayerEmoji(GameObject playerEmoji)
	{
		GameObject temp = (GameObject) Instantiate(playerEmoji);
		temp.transform.SetParent(emojiParentTransform,true);

		this.playerEmoji = temp.GetComponent<Emoji>();
		this.playerEmoji.Init();
	}
}