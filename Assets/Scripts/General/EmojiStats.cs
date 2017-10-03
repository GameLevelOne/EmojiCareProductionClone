using System.Collections;
using UnityEngine;
using System;

public class EmojiStats : MonoBehaviour {
	#region attributes
	public delegate void StatsUpdated();
	public event StatsUpdated OnStatsUpdated;

	float statValue;
	float maxStatValue;

	float tickModifier = -1f;
	float tickDelay = 60f;

	bool isTicking = false;

	public float Value{
		get{return statValue;}
		set{this.statValue = value;}
	}

	public float MaxValue{
		get{return maxStatValue;}
		set{maxStatValue = value;}
	}

	public float TickModifier{
		get{return tickModifier;}
		set{tickModifier = value;}
	}

	public float TickDelay{
		get{return tickDelay;}
		set{tickDelay = value;}
	}

	DateTime lastTimePlayed{
		get{return DateTime.Parse(PlayerPrefs.GetString(PlayerPrefKeys.Player.LAST_TIME_PLAYED));}
		set{PlayerPrefs.SetString(PlayerPrefKeys.Player.LAST_TIME_PLAYED,value.ToString());}
	}

	DateTime timeOnPause{
		get{return DateTime.Parse(PlayerPrefs.GetString(PlayerPrefKeys.Player.TIME_ON_PAUSE));}
		set{PlayerPrefs.SetString(PlayerPrefKeys.Player.TIME_ON_PAUSE,value.ToString());}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init()
	{
		UpdateStatsAfterLogin();
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void UpdateStatsAfterLogin()
	{
		if(PlayerPrefs.HasKey(PlayerPrefKeys.Player.LAST_TIME_PLAYED)){
			if(DateTime.Now.CompareTo(lastTimePlayed) < 0) return;

			else if(DateTime.Now.CompareTo(lastTimePlayed) > 0){
				float totalTicks = GetTotalTicks(DateTime.Now - lastTimePlayed);
			}
		}

		StartCoroutine(StartTicking());
	}

	public void UpdateStatsAfterPause()
	{
		if(PlayerPrefs.HasKey(PlayerPrefKeys.Player.TIME_ON_PAUSE)){
			if(DateTime.Now.CompareTo(timeOnPause) < 0) return;

			else if(DateTime.Now.CompareTo(timeOnPause) > 0){
				float totalTicks = GetTotalTicks(DateTime.Now - timeOnPause);

			}
		}

		StartCoroutine(StartTicking());
	}

	float GetTotalTicks(TimeSpan duration)
	{
		float dayToSec = duration.Days * 24 * 60 * 60;
		float hourToSec = duration.Hours * 60 * 60;
		float minToSec = duration.Minutes * 60;
		float sec = duration.Seconds;

		float totalSec = dayToSec + hourToSec + minToSec + sec;

		return Mathf.Floor(totalSec/tickDelay);
	}
		
	void OnApplicationPause(bool isPaused)
	{
		if(isPaused){
			StopAllCoroutines();
			isTicking = false;
			timeOnPause = DateTime.Now;
		}else{
			if(PlayerPrefs.HasKey(PlayerPrefKeys.Player.TIME_ON_PAUSE)){
				UpdateStatsAfterPause();
			}
		}
	}

	void OnApplicationQuit()
	{
		StopAllCoroutines();
		isTicking = false;
		lastTimePlayed = DateTime.Now;
		PlayerPrefs.Save();
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void TickStats()
	{
		if(statValue <= 0) statValue += tickModifier;

		//if value is more than max value, readjust to max value
		if(OnStatsUpdated != null) OnStatsUpdated();
	}

	public void ModStats(float mod)
	{
		statValue += mod;

		if(statValue <= 0) statValue = 0;
		//else if(value >= maxStatValue) value = maxStatValue;

		if(OnStatsUpdated != null) OnStatsUpdated();
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