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

public enum EmojiExpression{
	Default = 0,//00
	Smile,		//01
	Yummy,		//02
	Hungry,		//03
	Starving,	//04
	Blush,		//05
	Embarassed,	//06
	Worried,	//07
	Excited,	//08
	Upset,		//09
	Cry,		//10
	Lively,		//11
	Fidget,		//12
	Sick,		//13
	Energized,	//14
	Weary,		//15
	Exhausted,	//16
	Eat,		//17
	Oh,			//18
	Content,	//19
	Eyeroll,	//20
	Whistle,	//21
	Amused,		//22
	Blessed,	//23
	Sleep,		//24
	Nerd,		//25
	Cool,		//26
	Happy,		//27
	Calm,		//28
	Hearty,		//29
	Mouthzip,	//30
	Terrified,	//31
	Kisswink,	//32
	Lick,		//33
	Overjoyed,	//34
	Glee,		//35
	Angry,		//36
	Drool,		//37
	Dizzy,		//38
	Surprised	//39
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
	public EmojiSO emojiSO;
	public GameObject emojiObject;
	public List<EmojiExpression> unlockedExpression = new List<EmojiExpression>();
	Animator emojiObjectAnimation;

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
	public void InitEmojiData(EmojiExpression[] unlockedExpression, GameObject emojiObject)
	{

	}

	public void ChangeExpression(EmojiExpression expression)
	{
		emojiObjectAnimation.SetInteger(AnimatorParameters.Ints.STATE,(int)expression);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}