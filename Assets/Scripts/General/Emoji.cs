using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EmojiStats{
	Hunger,
	Hygene,
	Happiness,
	Stamina,
	Health
}

public enum EmojiStatus{
	Alive,
	Dead,
	Abandoned,
	SentOff
}

public enum BodyAnimation{
	Idle,
	Bounce,
	Play,
	HappyBounce,
	Falling
}

public enum FaceAnimation{
	Default = 1,//01
	Smile,		//02
	Yummy,		//03
	Hungry,		//04
	Starving,	//05
	Blushed,	//06
	Embarrassed,//07
	Worried,	//08
	Excited,	//09
	Upset,		//10
	Cry,		//11
	Lively,		//12
	Fidget,		//13
	Sick,		//14
	Energized,	//15
	Weary,		//16
	Exhausted,	//17
	Eat,		//18
	Oh,			//19
	Content,	//20
	Eyeroll,	//21
	Whistle,	//22
	Amused,		//23
	Blessed,	//24
	Sleep,		//25
	Nerd,		//26
	Cool,		//27
	Happy,		//28
	Calm,		//29
	Hearty,		//30
	Mouthzip,	//31
	Terrified,	//32
	Kisswink,	//33
	Lick,		//34
	Overjoyed,	//35
	Glee,		//36
	Angry,		//37
	Drool,		//38
	Dizzy,		//39
	Surprised	//40
}

public class Emoji : MonoBehaviour {
	#region singleton
	private static Emoji instance = null;
	public static Emoji Instance {
		get{ return instance;}
	}

	void Awake()
	{
		if(instance != null && instance != this) Destroy(this.gameObject);
		else instance = this;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region delegate events
	public delegate void EmojiTickStats();
	public event EmojiTickStats OnEmojiTickStats;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region attribute
	[Header("Data")]
	public EmojiSO[] emojiSOs;

	[Header("Reference")]
	public string emojiName;
	public EmojiType emojiType;
	public List<FaceAnimation> unlockedExpression = new List<FaceAnimation>();
	public EmojiStatus emojiStatus;

	GameObject emojiObject;
	Animator bodyAnimation, faceAnimation;

	public float[] statsFactor;

	public float hunger{
		get{return PlayerPrefs.GetFloat(PlayerPrefKeys.Emoji.HUNGER);}
		set{PlayerPrefs.SetFloat(PlayerPrefKeys.Emoji.HUNGER,value);}
	}
	public float hygene{
		get{return PlayerPrefs.GetFloat(PlayerPrefKeys.Emoji.HYGENE);}
		set{PlayerPrefs.SetFloat(PlayerPrefKeys.Emoji.HYGENE,value);}
	}
	public float happiness{
		get{return PlayerPrefs.GetFloat(PlayerPrefKeys.Emoji.HAPPINESS);}
		set{PlayerPrefs.SetFloat(PlayerPrefKeys.Emoji.HAPPINESS,value);}
	}
	public float stamina{
		get{return PlayerPrefs.GetFloat(PlayerPrefKeys.Emoji.STAMINA);}
		set{PlayerPrefs.SetFloat(PlayerPrefKeys.Emoji.STAMINA,value);}
	}
	public float health{
		get{return PlayerPrefs.GetFloat(PlayerPrefKeys.Emoji.HEALTH);}
		set{PlayerPrefs.SetFloat(PlayerPrefKeys.Emoji.HEALTH,value);}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanic
	public void TickStats(float tick = 1f, float[] roomMod = null)
	{
		if(hunger > 0f) 	hunger 		-= ( tick * ( statsFactor[(int)EmojiStats.Hunger]    + roomMod[(int)EmojiStats.Hunger] ));
		if(hygene > 0f) 	hygene 		-= ( tick * ( statsFactor[(int)EmojiStats.Hygene]    + roomMod[(int)EmojiStats.Hygene] ));
		if(happiness > 0f)  happiness 	-= ( tick * ( statsFactor[(int)EmojiStats.Happiness] + roomMod[(int)EmojiStats.Happiness] ));
		if(stamina > 0f) 	stamina 	-= ( tick * ( statsFactor[(int)EmojiStats.Stamina]   + roomMod[(int)EmojiStats.Stamina] ));

		if(hunger <= 0f || hygene <= 0f || happiness <= 0f || stamina <= 0f){
			if(health > 0f) health -= ( tick * ( statsFactor[(int)EmojiStats.Health] + roomMod[(int)EmojiStats.Health] ));
		}

		if(OnEmojiTickStats != null) OnEmojiTickStats();
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public module
	public void InitEmojiData()
	{
		
	}

	public void InitEmojiObject(GameObject emojiObject)
	{
		this.emojiObject = emojiObject;
		bodyAnimation = emojiObject.transform.Find("Body").GetComponent<Animator>();
		faceAnimation = emojiObject.transform.GetChild(0).Find("Face").GetComponent<Animator>();
	}

	public void ChangeBodyAnimation(BodyAnimation anim)
	{
		bodyAnimation.SetInteger(AnimatorParameters.Ints.STATE,(int)anim);
	}

	public void ChangeExpression(FaceAnimation anim)
	{
		faceAnimation.SetInteger(AnimatorParameters.Ints.STATE,(int)anim);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}