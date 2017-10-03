using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
	static PlayerData instance;
	public static PlayerData Instance{ get{return instance;} }

	int defaultCoin = 100;
	int defulatGem = 0;

	bool hasInitEmojiObject = false;

	Emoji playerEmoji;

	public Emoji PlayerEmoji{
		get{return playerEmoji;}
	}

	//example
	public EmojiType PlayerEmojiType{
		get{return (EmojiType) PlayerPrefs.GetInt(PlayerPrefKeys.Player.PLAYER_EMOJI_TYPE);}
		set{PlayerPrefs.SetInt(PlayerPrefKeys.Player.PLAYER_EMOJI_TYPE,(int)value);}
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

		//example
		PlayerEmojiType = EmojiType.Emoji;
	}

	public void InitEmojiObject(GameObject emojiObject)
	{
		if(!hasInitEmojiObject){
			hasInitEmojiObject = true;

			GameObject tempObj = emojiObject;
			playerEmoji = Instantiate(tempObj,this.transform).GetComponent<Emoji>();

//			hunger = hygene = happiness = stamina = 50f;
//			health = 100f;

//			if(OnEmojiDoneLoading != null) OnEmojiDoneLoading();
		}
	}
}