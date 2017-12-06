using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingStatsManager : MonoBehaviour {
	public PopupStatsMeter[] statsMeterObj;
	public Sprite[] barSprites; //green,yellow,orange,red

	public void RegisterEvents(){
		Emoji.OnShowFloatingStatsBar += ShowMultipleMeters;
		EmojiStats.OnShowSingleStatBar += ShowSingleMeter;
		PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent += OnEmojiSleepEvent;
		PlayerData.Instance.PlayerEmoji.playerInput.OnEmojiWake += OnEmojiWake;
		ShowerTrigger.OnEnterShower += OnEnterShower;
		ShowerTrigger.OnExitShower += OnExitShower;
	}

	void OnDisable(){
		Emoji.OnShowFloatingStatsBar -= ShowMultipleMeters;
		EmojiStats.OnShowSingleStatBar -= ShowSingleMeter;
		PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent -= OnEmojiSleepEvent;
		PlayerData.Instance.PlayerEmoji.playerInput.OnEmojiWake -= OnEmojiWake;
	}

	void OnEmojiSleepEvent (bool sleeping)
	{
		StartCoroutine (UpdateMeterDisplay ((int)EmojiStatsState.Stamina, PlayerData.Instance.PlayerEmoji.stamina.totalModifier));
	}

	void OnEmojiWake (float startStamina, float mod)
	{
		statsMeterObj [(int)EmojiStatsState.Stamina].HideMeter ();
	}

	void OnEnterShower ()
	{
		StartCoroutine (UpdateMeterDisplay ((int)EmojiStatsState.Hygiene, PlayerData.Instance.PlayerEmoji.hygiene.totalModifier));
	}

	void OnExitShower ()
	{
		statsMeterObj [(int)EmojiStatsState.Hygiene].HideMeter ();
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

		statsMeterObj [type].gameObject.SetActive (true);
		statsMeterObj[type].ShowMeter((EmojiStatsState)type,false,currentValue,targetValue,GetCurrentBarSprite(currentValue));
	}

	public void ShowStatsFromMagnifyingGlass(){
		int counter = 0;
		for(int i=0;i<5;i++){
			statsMeterObj [i].gameObject.SetActive (true);
			statsMeterObj [i].transform.localPosition = new Vector3 (0, 452 - 100 * counter);
			float value = GetCurrentStatValue (i);
			statsMeterObj [i].ShowMeter((EmojiStatsState)i,false,GetCurrentStatValue(i),-1,GetCurrentBarSprite(value));
			counter++;
		}
	}

	public void HideStatsFromMagnifyingGlass(){
		int counter = 0;
		for (int i=0;i<5;i++){
			statsMeterObj [i].HideMeter ();
		}
	}

	public void ShowMultipleMeters(float[] mod){
		int counter = 0;
		for(int i=0;i<mod.Length;i++){
			if(mod[i] != 0){
				statsMeterObj [i].gameObject.SetActive (true);
				statsMeterObj [i].transform.localPosition = new Vector3 (0, 452 - 100 * counter);
				ShowSingleMeter (i, mod [i]);
				counter++;
			} else{
				statsMeterObj [i].gameObject.SetActive (false);
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

	Sprite GetCurrentBarSprite(float value){
		if(value>=0.9f){
			return barSprites [0];
		} else if(value>=0.4f && value<0.9f){
			return barSprites [1];
		} else if(value>=0.2f && value<0.4f){
			return barSprites [2];
		} else{
			return barSprites [3];
		}
	}

	IEnumerator UpdateMeterDisplay(int type,float mod){
		ShowSingleMeter (type, mod, 0);
		yield return null;
	}
}
