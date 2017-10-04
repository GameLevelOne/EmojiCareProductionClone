using System.Collections;
using UnityEngine;
using System;

public class EmojiStats : MonoBehaviour {
	#region attributes

	//constructor
	public EmojiStats(string prefKey, float emojiModifier, float maxStatValue, float startValue){
		this.prefKey = prefKey;
		this.emojiModifier = emojiModifier;
		this.maxStatValue = maxStatValue;
		if(PlayerPrefs.HasKey(prefKey) == false) this.StatValue = startValue;
	}

	string prefKey;

	float maxStatValue;
	float emojiModifier;
	float roomModifier = 0f;

	float tickDelay = 60f;

	bool isTicking = false;

	/// <summary>
	/// for PanelStatsManager, GET.
	/// </summary>
	public float StatValue{
		get{return PlayerPrefs.GetFloat(prefKey);}
		set{PlayerPrefs.SetFloat(prefKey,value);}
	}

	public float MaxStatValue{
		get{return maxStatValue;}
	}

	public float totalModifier{
		get{return (emojiModifier + roomModifier);}
	}

	public float RoomModifier{
		set{roomModifier = value;}
	}

	public float TickDelay{
		set{tickDelay = value;}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void UpdateStatsAfterLogin(DateTime lastTimePlayed)
	{
		if(PlayerPrefs.HasKey(PlayerPrefKeys.Player.LAST_TIME_PLAYED)){
			
			if(DateTime.Now.CompareTo(lastTimePlayed) < 0) return;

			else if(DateTime.Now.CompareTo(lastTimePlayed) > 0){
				float totalTicks = GetTotalTicks(DateTime.Now - lastTimePlayed);
				ModStats(totalTicks);
			}
		}

		StartCoroutine(StartTicking());
	}

	public void UpdateStatsAfterPause(DateTime timeOnPause)
	{
		if(PlayerPrefs.HasKey(PlayerPrefKeys.Player.TIME_ON_PAUSE)){
			if(DateTime.Now.CompareTo(timeOnPause) < 0) return;

			else if(DateTime.Now.CompareTo(timeOnPause) > 0){
				float totalTicks = GetTotalTicks(DateTime.Now - timeOnPause);
				ModStats(totalTicks);
			}
		}

		StartCoroutine(StartTicking());
	}

	float GetTotalTicks(TimeSpan duration)
	{
		float dayToSec = duration.Days * 24 * 60 * 60;
		float hourToSec = duration.Hours * 60 * 60;
		float minToSec = duration.Minutes * 60;

		float totalSec = dayToSec + hourToSec + minToSec;

		return -1f * (Mathf.Floor(totalSec/tickDelay));
	}
		
	void OnApplicationPause(bool isPaused)
	{
		if(isPaused) isTicking = false;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void TickStats()
	{
		StatValue += (emojiModifier + roomModifier);

		if(StatValue <= 0) StatValue = 0;
		else if(StatValue >= maxStatValue) StatValue = maxStatValue;
	}

	/// <summary>
	/// negative value is written with '-' operator e.g. -1f.
	/// </summary>
	public void ModStats(float mod)
	{
		StatValue += mod;

		if(StatValue <= 0) StatValue = 0;
		else if(StatValue >= maxStatValue) StatValue = maxStatValue;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	#region coroutines
	IEnumerator StartTicking()
	{
		isTicking = true;

		while(true){
			yield return new WaitForSeconds(tickDelay);
			TickStats();
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}