using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EmojiStats{
	Hunger,
	Hygiene,
	Happiness,
	Stamina,
	Health
}

public class Emoji : MonoBehaviour {
	private static Emoji instance;
	private EmojiSO currentEmojiData;

	void Awake(){
		if(instance != null && instance != this){
			Destroy(this.gameObject);
		} else{
			instance = this;
		}
	}

	public static Emoji Instance {
		get{ return instance;}
	}

	public EmojiSO CurrentEmojiData {
		set{ currentEmojiData = value; }
		get{ return currentEmojiData;}
	}
}
