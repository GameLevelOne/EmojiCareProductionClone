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

		screenAlbum.AddEmojiRecord();
	}

	void OnReviveEmoji ()
	{
		ResetEmojiStats ();
		base.CloseUI (this.gameObject);
	}

	void OnResetEmoji ()
	{
		ResetEmojiStats ();
		ResetExpressionProgress ();
		base.CloseUI (this.gameObject);
	}

	public void OnClickRevive(){
		uiCoin.ShowUI (100, false);
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
	}

	void ResetExpressionProgress(){
		for(int i=0;i<PlayerData.Instance.PlayerEmoji.emojiExpressions.totalExpression;i++){
			PlayerData.Instance.PlayerEmoji.emojiExpressions.expressionDataInstances [i].SetCurrentProgress (0);
		}
	}
}
