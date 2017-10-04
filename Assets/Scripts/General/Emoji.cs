using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EmojiStatss{
	Hunger,
	hygene,
	Happiness,
	Stamina,
	Health
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
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region delegate events
	public delegate void EmojiTickStats();
	public event EmojiTickStats OnEmojiTickStats;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region attribute
	public string emojiName;
	public GameObject emojiObject;

	[HideInInspector]
	public EmojiStats hunger, hygene,happiness,stamina, health;

	public List<FaceAnimation> unlockedExpression = new List<FaceAnimation>();

	DateTime lastTimePlayed{
		get{return DateTime.Parse(PlayerPrefs.GetString(PlayerPrefKeys.Player.LAST_TIME_PLAYED));}
		set{PlayerPrefs.SetString(PlayerPrefKeys.Player.LAST_TIME_PLAYED,value.ToString());}
	}

	DateTime timeOnPause{
		get{return DateTime.Parse(PlayerPrefs.GetString(PlayerPrefKeys.Player.TIME_ON_PAUSE));}
		set{PlayerPrefs.SetString(PlayerPrefKeys.Player.TIME_ON_PAUSE,value.ToString());}
	}

	public EmojiSO emojiBaseData;

	bool hasInit = false;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanic

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public module
	public void Init()
	{
		if(!hasInit){
			hasInit = true;
//			emojiName = name;

			InitEmojiStats();

//			for(int i = 0;i<expressions.Length;i++) unlockedExpression.Add((FaceAnimation)expressions[i]);
		}
	}

	//stats
	void InitEmojiStats()
	{
		hunger = new EmojiStats(
			PlayerPrefKeys.Emoji.HUNGER,
			emojiBaseData.hungerModifier,
			emojiBaseData.maxStatValue,
			emojiBaseData.hungerStart
		);

		hygene = new EmojiStats(
			PlayerPrefKeys.Emoji.HYGENE,
			emojiBaseData.hygeneModifier,
			emojiBaseData.maxStatValue,
			emojiBaseData.hygeneStart
		);

		happiness = new EmojiStats(
			PlayerPrefKeys.Emoji.HAPPINESS,
			emojiBaseData.happinessModifier,
			emojiBaseData.maxStatValue,
			emojiBaseData.happinessStart
		);

		stamina = new EmojiStats(
			PlayerPrefKeys.Emoji.STAMINA,
			emojiBaseData.staminaModifier,
			emojiBaseData.maxStatValue,
			emojiBaseData.staminaStart
		);

		health = new EmojiStats(
			PlayerPrefKeys.Emoji.HEALTH,
			emojiBaseData.healthModifier,
			emojiBaseData.maxStatValue,
			emojiBaseData.healthStart
		);

		hunger.UpdateStatsAfterLogin(lastTimePlayed);
		hygene.UpdateStatsAfterLogin(lastTimePlayed);
		happiness.UpdateStatsAfterLogin(lastTimePlayed);
		stamina.UpdateStatsAfterLogin(lastTimePlayed);
		health.UpdateStatsAfterLogin(lastTimePlayed);
	}

	void StopTickingStats()
	{
		hunger.StopAllCoroutines();
		hygene.StopAllCoroutines();
		happiness.StopAllCoroutines();
		stamina.StopAllCoroutines();
		health.StopAllCoroutines();
	}

	void ResumeTickingStats()
	{
		hunger.UpdateStatsAfterPause(timeOnPause);
		hygene.UpdateStatsAfterPause(timeOnPause);
		happiness.UpdateStatsAfterPause(timeOnPause);
		stamina.UpdateStatsAfterPause(timeOnPause);
		health.UpdateStatsAfterPause(timeOnPause);
	}

	public void OnEditMode(bool editMode)
	{
		if(editMode) emojiObject.SetActive(false);
		else emojiObject.SetActive(true);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	void OnApplicationPause(bool isPaused)
	{
		if(isPaused){ 
			StopTickingStats();
			timeOnPause = DateTime.Now;
		}
		else{
			ResumeTickingStats();
		}
	}

	void OnApplicationQuit()
	{
		StopTickingStats();
		lastTimePlayed = DateTime.Now;
		PlayerPrefs.Save();
	}
}