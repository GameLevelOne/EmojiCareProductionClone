using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emoji : MonoBehaviour {
	private Emoji instance;
	private EmojiSO currentEmojiData;

	public Emoji Instance {
		get{ return instance;}
	}

	void Awake(){
		if(instance != null && instance != this){
			Destroy(this.gameObject);
		} else{
			instance = this;
		}
	}
}
