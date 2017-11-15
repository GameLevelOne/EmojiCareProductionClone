using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingStatsManager : MonoBehaviour {
	public GameObject[] statsMeterObj;

	void OnEnable(){
		PlayerData.Instance.PlayerEmoji.OnShowFloatingStatsBar += ShowMultipleMeters;
		EmojiStats.OnShowSingleStatBar += ShowSingleMeter;
	}

	void OnDisable(){
		PlayerData.Instance.PlayerEmoji.OnShowFloatingStatsBar -= ShowMultipleMeters;
		EmojiStats.OnShowSingleStatBar -= ShowSingleMeter;
	}

	public void ShowSingleMeter(int type,float mod){
		float currentValue = GetCurrentStatValue (type);
		float targetValue = currentValue + mod;
		float maxValue = PlayerData.Instance.PlayerEmoji.emojiBaseData.maxStatValue;

		if(targetValue > maxValue){
			targetValue = maxValue;
		}

		statsMeterObj [type].SetActive (true);
		statsMeterObj[type].GetComponent<PopupStatsMeter>().ShowUI((EmojiStatsState)type,currentValue,targetValue,maxValue);
	}

	public void ShowMultipleMeters(float[] mod){
		int counter = 0;
		for(int i=0;i<mod.Length;i++){
			if(mod[i] != 0){
				statsMeterObj [i].SetActive (true);
				statsMeterObj [i].transform.localPosition = new Vector3 (0, 400 - 100 * counter);
				counter++;
			} else{
				statsMeterObj [i].SetActive (false);
			}
		}
	}

	float GetCurrentStatValue(int type){
		Emoji emojiData = PlayerData.Instance.PlayerEmoji;
		if (type == 0) {
			return emojiData.hunger.StatValue;
		} else if (type == 1) {
			return emojiData.hygiene.StatValue;
		} else if (type == 2) {
			return emojiData.happiness.StatValue;
		} else if (type == 3) {
			return emojiData.stamina.StatValue;
		} else if (type == 4) {
			return emojiData.health.StatValue;
		} else
			return 0;
	}
}
