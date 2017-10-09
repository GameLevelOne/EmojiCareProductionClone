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
	Falling,
	BounceFromLeft,
	BounceFromRight
}

public class Emoji : MonoBehaviour {
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region delegate events
	public delegate void EmojiTickStats();
	public delegate void EmojiDead();

	public event EmojiTickStats OnEmojiTickStats;
	public event EmojiDead OnEmojiDead;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region attribute
	[Header("Reference")]
	public Rigidbody2D thisRigidbody;
	public EmojiBody body;
	public EmojiTriggerFall triggerFall;
	public EmojiSO emojiBaseData;

	[Header("")]
	public string emojiName;
	public EmojiExpression emojiExpressions;
	public EmojiStats hunger, hygene,happiness,stamina, health;

	DateTime lastTimePlayed{
		get{return DateTime.Parse(PlayerPrefs.GetString(PlayerPrefKeys.Player.LAST_TIME_PLAYED));}
		set{PlayerPrefs.SetString(PlayerPrefKeys.Player.LAST_TIME_PLAYED,value.ToString());}
	}

	DateTime timeOnPause{
		get{return DateTime.Parse(PlayerPrefs.GetString(PlayerPrefKeys.Player.TIME_ON_PAUSE));}
		set{PlayerPrefs.SetString(PlayerPrefKeys.Player.TIME_ON_PAUSE,value.ToString());}
	}

	bool isTickingStat = false;
	bool hasInit = false;
	bool emojiDead = false;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init()
	{
		if(!hasInit){
			hasInit = true;
			InitEmojiStats();
		}
	}

	//stats
	void InitEmojiStats()
	{
		hunger = 	new EmojiStats( PlayerPrefKeys.Emoji.HUNGER, 	emojiBaseData.hungerModifier, 	 emojiBaseData.maxStatValue, emojiBaseData.hungerStart );
		hygene = 	new EmojiStats( PlayerPrefKeys.Emoji.HYGENE, 	emojiBaseData.hygeneModifier, 	 emojiBaseData.maxStatValue, emojiBaseData.hygeneStart );
		happiness = new EmojiStats( PlayerPrefKeys.Emoji.HAPPINESS, emojiBaseData.happinessModifier, emojiBaseData.maxStatValue, emojiBaseData.happinessStart );
		stamina = 	new EmojiStats( PlayerPrefKeys.Emoji.STAMINA, 	emojiBaseData.staminaModifier, 	 emojiBaseData.maxStatValue, emojiBaseData.staminaStart );
		health = 	new EmojiStats( PlayerPrefKeys.Emoji.HEALTH, 	emojiBaseData.healthModifier, 	 emojiBaseData.maxStatValue, emojiBaseData.healthStart );

		int totalTicks = 0;
		if(PlayerPrefs.HasKey(PlayerPrefKeys.Player.LAST_TIME_PLAYED)){
			if(DateTime.Now.CompareTo(lastTimePlayed) > 0){
				totalTicks = GetTotalTicks(DateTime.Now - lastTimePlayed);
			}
		}

		for(int i = 0;i<totalTicks;i++){ 
			if(emojiDead) break;
			TickStats();
		}

		if(!emojiDead) StartCoroutine(tickingStats);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanic


	//event trigger
	public void BeginDrag()
	{
		if(CanDragEmoji()){
			thisRigidbody.simulated = false;
			body.thisCollider.enabled = false;
			triggerFall.ClearColliderList();
		}
	}

	//event trigger
	public void Drag()
	{
		if(CanDragEmoji()){
			Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,8f);
			transform.position = Camera.main.ScreenToWorldPoint(tempMousePosition);
		}
	}

	//event trigger
	public void EndDrag()
	{
		if(CanDragEmoji()){
			thisRigidbody.velocity = Vector2.zero;
			thisRigidbody.simulated = true;

			StartCoroutine(IgnoreCollision());
		}
	}

	void ResumeTickingStats()
	{
		int totalTicks = 0;
		if(PlayerPrefs.HasKey(PlayerPrefKeys.Player.TIME_ON_PAUSE)){
			if(DateTime.Now.CompareTo(timeOnPause) > 0){
				totalTicks = GetTotalTicks(DateTime.Now - timeOnPause);
			}
		}

		for(int i = 0;i<totalTicks;i++){ 
			if(emojiDead) break;
			TickStats();
		}

		StartCoroutine(tickingStats);
	}

	void TickStats()
	{
		hunger.TickStats();
		hygene.TickStats();
		happiness.TickStats();
		stamina.TickStats();

		if(hunger.StatValue <= 0f || hygene.StatValue <= 0f || happiness.StatValue <= 0f || stamina.StatValue <= 0f){
			health.TickStats();

			if(health.StatValue <= 0f){
				emojiDead = true;
				if(OnEmojiDead != null) OnEmojiDead();
			}
		}
	}

	int GetTotalTicks(TimeSpan duration)
	{
		int dayToSec = duration.Days * 24 * 60 * 60;
		int hourToSec = duration.Hours * 60 * 60;
		int minToSec = duration.Minutes * 60;
		int sec = duration.Seconds;

		int totalSec = dayToSec + hourToSec + minToSec;

		return totalSec;
	}

	bool CanDragEmoji()
	{
		
		return true;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public module

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region coroutines
	const string tickingStats = "_StartTickingStats";
	IEnumerator _StartTickingStats()
	{
		isTickingStat = true;

		while(true){
			yield return new WaitForSeconds(1f);

			TickStats();
		}
	}

	IEnumerator IgnoreCollision()
	{
		triggerFall.isFalling = true;
		yield return null;
		triggerFall.IgnoreCollision();
		triggerFall.isFalling = false;
	}

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------

	void OnApplicationPause(bool isPaused)
	{
		if(isPaused){ 
			isTickingStat = false;
			StopCoroutine(tickingStats);
			timeOnPause = DateTime.Now;
		}
		else{
			ResumeTickingStats();
		}
	}

	void OnApplicationQuit()
	{
		isTickingStat = false;
		StopCoroutine(tickingStats);
		lastTimePlayed = DateTime.Now;
	}
}