using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingStatsManager : MonoBehaviour {
	public PopupStatsMeter[] statsMeterObj;
	public Sprite[] barSprites; //green,yellow,orange,red
	bool barIsShowing = false;

	public void Init(){
		ShowerTrigger.OnEnterShower += OnEnterShower;
		ShowerTrigger.OnExitShower += OnExitShower;
	}

	public void RegisterEmojiEvents()
	{
		Emoji.OnShowFloatingStatsBar += ShowMultipleMeters;
		EmojiStats.OnShowSingleStatBar += ShowSingleMeter;
		PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent += OnEmojiSleepEvent;
		PlayerData.Instance.PlayerEmoji.playerInput.OnEmojiWake += OnEmojiWake;
	}

	public void UnregisterEmojiEvents()
	{
		Emoji.OnShowFloatingStatsBar -= ShowMultipleMeters;
		EmojiStats.OnShowSingleStatBar -= ShowSingleMeter;
		PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent -= OnEmojiSleepEvent;
		PlayerData.Instance.PlayerEmoji.playerInput.OnEmojiWake -= OnEmojiWake;
	}

	void OnDestroy(){
		UnregisterEmojiEvents();

		ShowerTrigger.OnEnterShower -= OnEnterShower;
		ShowerTrigger.OnExitShower -= OnExitShower;
	}

	public void OnEmojiSleepEvent (bool sleeping)
	{
		if (sleeping) {
			Debug.Log ("sleeping");
			barIsShowing = true;
			statsMeterObj [(int)EmojiStatsState.Stamina].gameObject.SetActive (true);
			StartCoroutine(UpdateMeterDisplay ((int)EmojiStatsState.Stamina));
		}
	}

	void OnEmojiWake ()
	{
		Debug.Log ("WAKEUP");
		barIsShowing = false;
		//StopCoroutine ("UpdateMeterDisplay");
		//statsMeterObj [(int)EmojiStatsState.Stamina].HideMeter ();
	}

	void OnEnterShower ()
	{
		Debug.Log ("bathing");
		barIsShowing = true;
		statsMeterObj [(int)EmojiStatsState.Hygiene].gameObject.SetActive (true);
		StartCoroutine(UpdateMeterDisplay ((int)EmojiStatsState.Hygiene));
	}

	void OnExitShower ()
	{
		Debug.Log ("FINISH BATHING");
		barIsShowing = false;
		//StopCoroutine ("UpdateMeterDisplay");
		//statsMeterObj [(int)EmojiStatsState.Hygiene].HideMeter ();
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

	IEnumerator UpdateMeterDisplay(int type){
		while(barIsShowing){
			float value = GetCurrentStatValue (type);
			statsMeterObj [type].ShowMeter ((EmojiStatsState)type, true,value, -1, GetCurrentBarSprite (value));
			yield return null;
		}

		if(!barIsShowing){
			statsMeterObj [type].HideMeter ();
			StopCoroutine ("UpdateMeterDisplay");
		}
	}

}
