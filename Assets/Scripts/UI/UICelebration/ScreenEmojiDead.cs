using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEmojiDead : BaseUI {
	public ScreenAlbum screenAlbum;
	public SceneLoader sceneLoader;
	public ScreenPopup screenPopup;
	public UICoin uiCoin;
	public Fader fader;
	public Image emojiIcon;

	bool gemBoxIsShowing = false;

	void OnEnable(){
		Fader.OnFadeOutFinished += OnFadeOutFinished;
		ScreenPopup.OnResetEmoji += OnResetEmoji;
		ScreenPopup.OnReviveEmoji += OnReviveEmoji;
	}
		
	void OnDisable(){
		Fader.OnFadeOutFinished -= OnFadeOutFinished;
	}

	void OnFadeOutFinished ()
	{
		Fader.OnFadeOutFinished -= OnFadeOutFinished;
		sceneLoader.gameObject.SetActive(true);
		sceneLoader.NextScene = "SceneMain";
	}

	public void ShowUI(Sprite sprite,GameObject obj){
		base.ShowUI(obj);
		this.sceneLoader = sceneLoader;
		//emojiIcon.sprite = sprite;

//		screenAlbum.AddEmojiRecord();
	}

	void OnReviveEmoji ()
	{
		if(gemBoxIsShowing){
			uiCoin.CloseUI (false);
			gemBoxIsShowing = false;
		}

		ResetEmojiStats ();
		PlayerData.Instance.PlayerEmoji.emojiExpressions.ResetExpressionDuration ();
		base.CloseUI (this.gameObject);
	}

	void OnResetEmoji ()
	{
		if(gemBoxIsShowing){
			uiCoin.CloseUI (false);
			gemBoxIsShowing = false;
		}

		ResetEmojiStats ();
		ResetExpressionProgress ();
		PlayerData.Instance.PlayerEmoji.emojiExpressions.ResetExpressionDuration ();
		base.CloseUI (this.gameObject);
	}

	public void OnClickRevive(){
		uiCoin.ShowUI (100, false,true,false);
		gemBoxIsShowing = true;
		screenPopup.ShowPopup (PopupType.Confirmation, PopupEventType.ReviveEmoji);
	}

	public void OnClickReset(){
		screenPopup.ShowPopup (PopupType.Confirmation, PopupEventType.ResetEmoji);
	}

	void ResetEmojiStats(){
		PlayerData.Instance.PlayerEmoji.hunger.SetStats (PlayerData.Instance.PlayerEmoji.emojiBaseData.hungerStart);
		PlayerData.Instance.PlayerEmoji.hygiene.SetStats (PlayerData.Instance.PlayerEmoji.emojiBaseData.hygeneStart);
		PlayerData.Instance.PlayerEmoji.happiness.SetStats (PlayerData.Instance.PlayerEmoji.emojiBaseData.happinessStart);
		PlayerData.Instance.PlayerEmoji.stamina.SetStats (PlayerData.Instance.PlayerEmoji.emojiBaseData.staminaStart);
		PlayerData.Instance.PlayerEmoji.health.SetStats (PlayerData.Instance.PlayerEmoji.emojiBaseData.healthStart);
		PlayerData.Instance.PlayerEmoji.emojiDead = false;
	}

	void ResetExpressionProgress(){
		for(int i=0;i<PlayerData.Instance.PlayerEmoji.emojiExpressions.totalExpressionAvailable;i++){
			PlayerData.Instance.PlayerEmoji.emojiExpressions.expressionDataInstances [i].SetCurrentProgress (0);
			PlayerPrefs.SetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_STATUS +
			PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType.ToString (), 0);
		}
	}

	public void TestEmojiDead(){
		PlayerData.Instance.PlayerEmoji.hunger.SetStats (0);
		PlayerData.Instance.PlayerEmoji.hygiene.SetStats (0);
		PlayerData.Instance.PlayerEmoji.happiness.SetStats (0);
		PlayerData.Instance.PlayerEmoji.stamina.SetStats (0);
		PlayerData.Instance.PlayerEmoji.health.SetStats (0);
	}
}
