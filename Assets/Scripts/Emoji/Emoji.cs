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
	public EmojiStats hunger, hygiene,happiness,stamina, health;
	public EmojiExpression emojiExpressions;
	public EmojiActivity activity;
	public bool interactable = true;

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

	//interactions
	bool flagHold = false;
	bool flagStroke = false;

	bool isDoubleTap = false;
	int doubleTapCounter = 0;

	int shakeCounter = 0;
	float prevX = 0;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init()
	{
		if(!hasInit){
//			InitEmojiStats();
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
	#region event triggers
	public void PointerClick()
	{
//		if(interactable){
//			if(!isDoubleTap){
//				isDoubleTap = true;
//				StartCoroutine(_OnTap);
//			}else{
//				StopCoroutine(_OnTap);
//				doubleTapCounter++;
//				switch(doubleTapCounter){
//				case 1: break;
//				case 2: break;
//				case 3: break;
//				case 4: break;
//				case 5: 
//					//lock interactions
//					//lock roomchanging
//					//Cooldown
//					doubleTapCounter = 0;
//					StartCoroutine(_LockInteractions);
//					break;
//				default: break;
//				}
//			}
//		}

	}
		
	public void PointerEnter()
	{
//		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,8f);
//		Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
//		Vector3 newPos = new Vector3(touchWorldPosition.x,touchWorldPosition.y+0.5f,touchWorldPosition.z);
//
//		if(interactable) StartCoroutine(_HoldDelay,newPos);
		if(!flagStroke){
			StartCoroutine(_HoldDelay);
		}else{
			//set anim stroke
		}

	}
		
	public void PointerExit()
	{
//		StopCoroutine(_HoldDelay);
		if(flagStroke){
			//set anim default
		}
	}

	public void BeginDrag()
	{
//		if(interactable){
//			if(hold == false){ 
//				print("hold still false");
//				StopCoroutine(_HoldDelay);
//				stroking = true;
//			}
//			else{
//				print("hold is true");
//				thisRigidbody.simulated = false;
//				body.thisCollider.enabled = false;
//				triggerFall.ClearColliderList();
//
//				emojiExpressions.ResetExpressionDuration();
//			}
//		}

		if(flagHold == false){
			StopCoroutine(_HoldDelay);
			flagStroke = true;
		}else{
			//nothing
		}
	}
		
	public void Drag()
	{
//		if(interactable){
//			if(!hold){
//				Vector3 touchPosition = getTouchToWorldPosition();
//				float x = touchPosition.x;
//				if(x <= transform.localPosition.x){
//					//stroke left
//					Debug.Log("Stroke left");
//				}else{
//					//stroke right
//					Debug.Log("Stroke Right");
//				}
//			}else{
//				Vector3 touchPosition = getTouchToWorldPosition();
//				Vector3 newPos = new Vector3(touchPosition.x,touchPosition.y+0.5f,touchPosition.z);
//				transform.position = newPos;
//				float x = transform.position.x;
//
//			}
//		}

		if(flagHold == true){
			Hold();
		}else if(flagStroke == true){
			Stroke();
		}
	}

	public void EndDrag()
	{
//		if(interactable){
//			
//				
//				thisRigidbody.velocity = Vector2.zero;
//				thisRigidbody.simulated = true;
//
//				StartCoroutine(IgnoreCollision());
//
//		}
//		shakeCounter = 0;

	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanic
	//interactions
	void Stroke()
	{
		
	}

	void Hold()
	{
		transform.position = getEmojiPositionOnHold(getTouchToWorldPosition());
	}

	Vector3 getEmojiPositionOnHold(Vector3 touchWorldPosition)
	{
		return new Vector3(touchWorldPosition.x,touchWorldPosition.y+0.5f,touchWorldPosition.z);
	}

	Vector3 getTouchToWorldPosition()
	{
		//Emoji z position should be -2. Camera position is 0,0,-10f thus -10 + 8 = -2. 
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,8f);
		return Camera.main.ScreenToWorldPoint(tempMousePosition);
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
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public module
	public void ModAllStats(float[] mod)
	{
		hunger.ModStats(mod[0]);
		hygiene.ModStats(mod[1]);
		happiness.ModStats(mod[2]);
		stamina.ModStats(mod[3]);
		health.ModStats(mod[4]);
	}
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

	const string _OnTap = "OnTap";
	IEnumerator OnTap()
	{
		yield return new WaitForSeconds(0.2f);
		isDoubleTap = false;
	}

	//interactions
	const string _HoldDelay = "HoldDelay";
	IEnumerator HoldDelay(Vector3 destination)
	{
		yield return new WaitForSeconds(1f);
		flagHold = true;

		thisRigidbody.simulated = false;
		body.thisCollider.enabled = false;
		triggerFall.ClearColliderList(); //must be fixed!

		Vector3 currentPosition = transform.position;

		float t = 0;
		while(t < 1f){
			transform.position = Vector3.Lerp(currentPosition,getEmojiPositionOnHold(getTouchToWorldPosition()),t);
			t += Time.deltaTime*6f;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		transform.position = getEmojiPositionOnHold(getTouchToWorldPosition());
	}

	const string _LockInteractions = "LockInteractions";
	IEnumerator LockInteractions()
	{
		interactable = false;
		yield return new WaitForSeconds(5f);
		interactable = true;
	}

	const string _DelayResetShakeCounter = "DelayResetShakeCounter";
	IEnumerator DelayResetShakeCounter()
	{
		yield return new WaitForSeconds(0.35f);
		shakeCounter = 0;
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