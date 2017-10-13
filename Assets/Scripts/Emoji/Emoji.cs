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
	public delegate void UpdateStatsToExpression(float hunger, float hygiene, float happiness, float stamina, float health);

	public event EmojiTickStats OnEmojiTickStats;
	public static event EmojiDead OnEmojiDead;
	public event UpdateStatsToExpression OnUpdateStatsToExpression;
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
	public EmojiStats hunger, hygiene,happiness,stamina, health;

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
			InitEmojiStats();
			hasInit = true;
		}
	}

	//stats
	void InitEmojiStats()
	{
		hunger = 	new EmojiStats( PlayerPrefKeys.Emoji.HUNGER, 	emojiBaseData.hungerModifier, 	 emojiBaseData.maxStatValue, emojiBaseData.hungerStart );
		hygiene = 	new EmojiStats( PlayerPrefKeys.Emoji.HYGENE, 	emojiBaseData.hygeneModifier, 	 emojiBaseData.maxStatValue, emojiBaseData.hygeneStart );
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
			if(!emojiDead) TickStats();
			else break;
		}

		if(!emojiDead){ 
			StartCoroutine(coroutineTickingStats);
			if(OnUpdateStatsToExpression != null) 
				OnUpdateStatsToExpression(
					hunger.StatValue	/ hunger.MaxStatValue,
					hygiene.StatValue	/ hygiene.MaxStatValue,
					happiness.StatValue	/ happiness.MaxStatValue,
					stamina.StatValue	/ stamina.MaxStatValue,
					health.StatValue	/ health.MaxStatValue
				);
		}

	}

	void InitEmojiExpression()
	{
		emojiExpressions.unlockedExpressions.Add(FaceExpression.Default);
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

			emojiExpressions.ResetExpressionDuration();
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

		StartCoroutine(coroutineTickingStats);
	}

	void TickStats()
	{
		hunger.TickStats();
		hygiene.TickStats();
		happiness.TickStats();
		stamina.TickStats();

		if(hunger.StatValue <= 0f || hygiene.StatValue <= 0f || happiness.StatValue <= 0f || stamina.StatValue <= 0f){
			health.TickStats();

			if(health.StatValue <= 0f){
				Debug.Log("DEAD");
				emojiDead = true;
				if(OnEmojiDead != null) OnEmojiDead();
				return;
			}
		}

		if(OnUpdateStatsToExpression != null) 
			OnUpdateStatsToExpression(
				hunger.StatValue	/ hunger.MaxStatValue,
				hygiene.StatValue	/ hygiene.MaxStatValue,
				happiness.StatValue	/ happiness.MaxStatValue,
				stamina.StatValue	/ stamina.MaxStatValue,
				health.StatValue	/ health.MaxStatValue
			);
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
	const string coroutineTickingStats = "_StartTickingStats";
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
			StopCoroutine(coroutineTickingStats);
			timeOnPause = DateTime.Now;
		}
		else{
			ResumeTickingStats();
		}
	}

	void OnApplicationQuit()
	{
		isTickingStat = false;
		StopCoroutine(coroutineTickingStats);
		lastTimePlayed = DateTime.Now;
	}
}