using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EmojiStatsTick : MonoBehaviour {

	const string KeyLastTimePlayed = "LastTimePlayed";
	const string KeyLastTimePaused = "LastTimePaused";

	public DateTime lastTimePlayed{
		set{ PlayerPrefs.SetString(KeyLastTimePlayed,value.ToString());}
		get{ return DateTime.Parse(PlayerPrefs.GetString(KeyLastTimePlayed));}
	}

	public DateTime lastTimePaused{
		set{ PlayerPrefs.SetString(KeyLastTimePaused,value.ToString());}
		get{ return DateTime.Parse(PlayerPrefs.GetString(KeyLastTimePaused));}
	}

	float tickDelay = 60;
	bool statsIsTicking = false;

	public void CalculateEmojiStats(){
		if(PlayerPrefs.HasKey(KeyLastTimePlayed)){
			if(DateTime.Now.CompareTo(lastTimePlayed) < 0) return;
			else if(DateTime.Now.CompareTo(lastTimePlayed) > 0){
				float totalTicks = GetTotalTicks(DateTime.Now - lastTimePlayed);
				PlayerPrefs.DeleteKey(KeyLastTimePlayed);
				Emoji.Instance.TickStats(totalTicks);
			}
		}

		if(!statsIsTicking){
			StartCoroutine(StatsTickOvertime());
		}
	}

	void CalculateEmojiStatsAfterPause(){
		if(PlayerPrefs.HasKey(KeyLastTimePaused)){
			if(DateTime.Now.CompareTo(lastTimePaused) < 0) return;
			else if(DateTime.Now.CompareTo(lastTimePlayed) > 0){
				float totalTicks = GetTotalTicks(DateTime.Now - lastTimePaused);
				PlayerPrefs.DeleteKey(KeyLastTimePaused);
				Emoji.Instance.TickStats(totalTicks);
			}
		}
		if(!statsIsTicking){
			StartCoroutine(StatsTickOvertime());
		}
	}

	float GetTotalTicks(TimeSpan duration){
		float dayToMin = duration.Days * 24 * 60;
		float hourToMin = duration.Hours * 60;
		float min = duration.Minutes;
		float secToMin = Mathf.Floor(duration.Seconds / tickDelay);
		return (dayToMin + hourToMin + min + secToMin);
	}

	IEnumerator StatsTickOvertime(){
		statsIsTicking = true;
		Emoji playerEmoji = Emoji.Instance;
		while(true){
			yield return new WaitForSeconds(tickDelay);
			playerEmoji.TickStats();
		}
	}

	void OnApplicationPause(bool pauseStatus){
		if(pauseStatus){
			StopAllCoroutines();
			statsIsTicking=false;
			lastTimePaused = DateTime.Now;
		} else{
			if(PlayerPrefs.HasKey(KeyLastTimePaused)){
				CalculateEmojiStatsAfterPause();
			}
		}
	}

	void OnApplicationQuit(){
		StopAllCoroutines();
		statsIsTicking = false;
		lastTimePlayed = DateTime.Now;
		PlayerPrefs.Save();
	}
}
