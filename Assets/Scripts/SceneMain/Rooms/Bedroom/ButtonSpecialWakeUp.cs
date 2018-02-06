using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpecialWakeUp : MonoBehaviour {
	public ScreenPopup popup;
	public GameObject buttonObj;

	public void Init(){
		PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent += OnEmojiSleepEvent;
		PlayerData.Instance.PlayerEmoji.playerInput.OnEmojiWake += OnEmojiWake;
		if(AdmobManager.Instance) AdmobManager.Instance.OnFinishWatchVideoAds += OnFinishWatchVideoAds;
	}

	void OnDisable(){
		PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent -= OnEmojiSleepEvent;
		PlayerData.Instance.PlayerEmoji.playerInput.OnEmojiWake -= OnEmojiWake;
		if(AdmobManager.Instance) AdmobManager.Instance.OnFinishWatchVideoAds -= OnFinishWatchVideoAds;
	}

	void OnFinishWatchVideoAds (AdEvents eventName)
	{
		if(eventName == AdEvents.WakeEmojiUp){
			EmojiStats emojiStamina = PlayerData.Instance.PlayerEmoji.stamina;
			float currentStamina = emojiStamina.StatValue;
			emojiStamina.SetStats (currentStamina + (0.5f * currentStamina));
			PlayerData.Instance.PlayerEmoji.playerInput.Wake ();
		}
	}

	void OnEmojiWake ()
	{
		buttonObj.SetActive (false);
	}

	void OnEmojiSleepEvent (bool sleeping)
	{
		if(sleeping) buttonObj.SetActive (true);
	}

	public void OnClickButton(){
		#if UNITY_EDITOR
		OnFinishWatchVideoAds(AdEvents.WakeEmojiUp);
		#else
		popup.ShowPopup (PopupType.Confirmation, PopupEventType.WakeEmojiUp);
		#endif
	}
}
