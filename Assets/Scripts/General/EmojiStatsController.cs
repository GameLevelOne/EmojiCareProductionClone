using System.Collections;
using System;
using UnityEngine;

public class EmojiStatsController : MonoBehaviour {
	private static EmojiStatsController instance = null;
	public static EmojiStatsController Instance{ get{return instance;} }
	#region attributes
	public RoomController roomController;
	public float tickDelay = 60f;

	public DateTime lastTimePlayed{
		set{ PlayerPrefs.SetString(PlayerPrefKeys.Player.LAST_TIME_PLAYED,value.ToString());}
		get{ return DateTime.Parse(PlayerPrefs.GetString(PlayerPrefKeys.Player.LAST_TIME_PLAYED));}
	}

	public DateTime TimeOnPause{
		set{ PlayerPrefs.SetString(PlayerPrefKeys.Player.TIME_ON_PAUSE,value.ToString());}
		get{ return DateTime.Parse(PlayerPrefs.GetString(PlayerPrefKeys.Player.TIME_ON_PAUSE));}
	}

	bool isTicking = false;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Awake()
	{
		if(instance != null && instance != this) Destroy(this.gameObject);
		else instance = this;
	}

	void OnDestroy()
	{
		StopAllCoroutines();
	}


	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	float GetTotalTicks(TimeSpan duration)
	{
		float dayToSec = duration.Days * 24 * 60 * 60;
		float hourToSec = duration.Hours * 60 * 60;
		float minToSec = duration.Minutes * 60;
		float sec = duration.Seconds;

		float totalSec = dayToSec + hourToSec + minToSec + sec;

		return Mathf.Floor(totalSec/tickDelay);
	}

	void OnApplicationPause(bool pauseStatus){
		if(pauseStatus){
			StopAllCoroutines();
			isTicking = false;
			TimeOnPause = DateTime.Now;
		}else{
			if(PlayerPrefs.HasKey(PlayerPrefKeys.Player.TIME_ON_PAUSE)){
				CalculateTicksAfterPause();
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
	public void CalculateTicksOnLogin()
	{
		if(PlayerPrefs.HasKey(PlayerPrefKeys.Player.LAST_TIME_PLAYED)){
			if(DateTime.Now.CompareTo(lastTimePlayed) < 0) return;

			else if(DateTime.Now.CompareTo(lastTimePlayed) > 0){

				float totalTicks = GetTotalTicks(DateTime.Now - lastTimePlayed);
				Emoji.Instance.TickStats(totalTicks);
				PlayerPrefs.DeleteKey(PlayerPrefKeys.Player.LAST_TIME_PLAYED);

			}
		}
	}

	public void CalculateTicksAfterPause()
	{
		if(PlayerPrefs.HasKey(PlayerPrefKeys.Player.TIME_ON_PAUSE)){
			if(DateTime.Now.CompareTo(TimeOnPause) < 0) return;

			else if(DateTime.Now.CompareTo(TimeOnPause) > 0){
				float totalTicks = GetTotalTicks(DateTime.Now - TimeOnPause);
				Emoji.Instance.TickStats(totalTicks);
				PlayerPrefs.DeleteKey(PlayerPrefKeys.Player.TIME_ON_PAUSE);
			}
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	#region coroutines
	IEnumerator StartTicking()
	{
		while(true){
			yield return new WaitForSeconds(tickDelay);
			Emoji.Instance.TickStats();
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
