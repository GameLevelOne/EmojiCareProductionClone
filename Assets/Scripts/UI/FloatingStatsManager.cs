using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingStatsManager : MonoBehaviour {
	public GameObject[] statsMeterObj;

	void OnDisable(){
		Emoji.OnShowFloatingStatsBar -= ShowMultipleMeters;
		EmojiStats.OnShowSingleStatBar -= ShowSingleMeter;
		PlayerData.Instance.PlayerEmoji.playerInput.OnEmojiWake -= OnEmojiWake;
		Shower.OnFinishShower -= OnFinishShower;
	}

	public void RegisterEvents(){
		Emoji.OnShowFloatingStatsBar += ShowMultipleMeters;
		EmojiStats.OnShowSingleStatBar += ShowSingleMeter;
		PlayerData.Instance.PlayerEmoji.playerInput.OnEmojiWake += OnEmojiWake;
		Shower.OnFinishShower += OnFinishShower;
	}

	void OnFinishShower (float mod, float startValue)
	{
		ShowSingleMeter ((int)EmojiStatsState.Hygiene, mod, startValue);
	}

	void OnEmojiWake (float startStamina, float mod)
	{
		ShowSingleMeter ((int)EmojiStatsState.Stamina, mod, startStamina);
	}

	public void ShowSingleMeter(int type,float mod,float startValue = 0f){
		float currentValue = 0;
		if(startValue==0){
			currentValue = GetCurrentStatValue (type);
		} else{
			currentValue = startValue;
		}

		float maxValue = PlayerData.Instance.PlayerEmoji.emojiBaseData.maxStatValue;
		float targetValue = currentValue + mod/maxValue;

		if(targetValue > 1){
			targetValue = 1;
		}

		statsMeterObj [type].SetActive (true);
		statsMeterObj[type].GetComponent<PopupStatsMeter>().ShowUI((EmojiStatsState)type,currentValue,targetValue,1);
	}

	public void ShowStatsFromMagnifyingGlass(){
		int counter = 0;
		for(int i=0;i<5;i++){
			statsMeterObj [i].SetActive (true);
			statsMeterObj [i].transform.localPosition = new Vector3 (0, 400 - 100 * counter);
			statsMeterObj [i].GetComponent<PopupStatsMeter> ().ShowStaticMeter (GetCurrentStatValue (i));
			counter++;
		}
	}

	public void HideStatsFromMagnifyingGlass(){
		int counter = 0;
		for (int i=0;i<5;i++){
			statsMeterObj [i].GetComponent<PopupStatsMeter> ().HideMeter ();
		}
	}

	public void ShowMultipleMeters(float[] mod){
		int counter = 0;
		for(int i=0;i<mod.Length;i++){
			if(mod[i] != 0){
				statsMeterObj [i].SetActive (true);
				statsMeterObj [i].transform.localPosition = new Vector3 (0, 400 - 100 * counter);
				ShowSingleMeter (i, mod [i]);
				counter++;
			} else{
				statsMeterObj [i].SetActive (false);
			}
		}
	}

	float GetCurrentStatValue(int type){
		Emoji emojiData = PlayerData.Instance.PlayerEmoji;
		if (type == 0) {
			return emojiData.hunger.StatValue/emojiData.hunger.MaxStatValue;
		} else if (type == 1) {
			return emojiData.hygiene.StatValue/emojiData.hygiene.MaxStatValue;
		} else if (type == 2) {
			return emojiData.happiness.StatValue/emojiData.happiness.MaxStatValue;
		} else if (type == 3) {
			return emojiData.stamina.StatValue/emojiData.stamina.MaxStatValue;
		} else if (type == 4) {
			return emojiData.health.StatValue/emojiData.health.MaxStatValue;
		} else
			return 0;
	}

}
