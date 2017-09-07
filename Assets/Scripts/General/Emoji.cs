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
	static Emoji instance;
	public EmojiSO currEmojiData; //temp

	float currEmojiHunger;
	float currEmojiHygiene;
	float currEmojiHappiness;
	float currEmojiStamina;
	float currEmojiHealth;

	float maxEmojiHunger;
	float maxEmojiHygiene;
	float maxEmojiHappiness;
	float maxEmojiStamina;
	float maxEmojiHealth;

	void Awake(){
		if(instance != null && instance != this){
			Destroy(this.gameObject);
		} else{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);

		InitEmojiData();
	}

	public void InitEmojiData(){
		maxEmojiHunger = currEmojiHunger = currEmojiData.maxHunger;
		maxEmojiHygiene = currEmojiHygiene = currEmojiData.maxHygiene;
		maxEmojiHappiness = currEmojiHappiness = currEmojiData.maxHappiness;
		maxEmojiStamina =  currEmojiStamina = currEmojiData.maxStamina;
		maxEmojiHealth = currEmojiHealth = currEmojiData.maxHealth;
	}

	public static Emoji Instance {
		get{ return instance;}
	}

	public EmojiSO CurrEmojiData {
		set{ currEmojiData = value; }
		get{ return currEmojiData;}
	}
	public float CurrEmojiHunger{
		set{currEmojiHunger = value;}
		get{return currEmojiHunger;}
	}
	public float CurrEmojiHygiene{
		set{currEmojiHygiene = value;}
		get{return currEmojiHygiene;}
	}
	public float CurrEmojiHappiness{
		set{currEmojiHappiness = value;}
		get{return currEmojiHappiness;}
	}
	public float CurrEmojiStamina{
		set{currEmojiStamina = value;}
		get{return currEmojiStamina;}
	}
	public float CurrEmojiHealth{
		set{currEmojiHealth = value;}
		get{return currEmojiHealth;}
	}
	public float MaxEmojiHunger{
		set{maxEmojiHunger = value;}
		get{return maxEmojiHunger;}
	}
	public float MaxEmojiHygiene{
		set{maxEmojiHygiene = value;}
		get{return maxEmojiHygiene;}
	}
	public float MaxEmojiHappiness{
		set{maxEmojiHappiness = value;}
		get{return maxEmojiHappiness;}
	}
	public float MaxEmojiStamina{
		set{maxEmojiStamina = value;}
		get{return maxEmojiStamina;}
	}
	public float MaxEmojiHealth{
		set{maxEmojiHealth = value;}
		get{return maxEmojiHealth;}
	}

}
