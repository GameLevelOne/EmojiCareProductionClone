using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSendOff : BaseUI {
	public Fader fader;
	public SceneLoader sceneLoader;

	public Image emojiIcon;
	public Text expressionProgress;
	public Text reward;

	public ScreenAlbum screenAlbum;

	void OnEnable(){
		Fader.OnFadeOutFinished += OnFadeOutFinished;
	}

	void OnDisable(){
		Fader.OnFadeOutFinished -= OnFadeOutFinished;
	}

	void OnFadeOutFinished ()
	{
		Fader.OnFadeOutFinished -= OnFadeOutFinished;
		sceneLoader.gameObject.SetActive(true);
		sceneLoader.NextScene = "SceneSelection";
	}

	public void ShowUI(Sprite sprite,string emojiName,GameObject obj){
		base.ShowUI(obj);
		this.sceneLoader = sceneLoader;

		emojiIcon.sprite = sprite;
		expressionProgress.text = "Expression Progress: "+ 
		(PlayerData.Instance.PlayerEmoji.emojiExpressions.GetTotalExpressionProgress()*100).ToString()+"%";
		reward.text = "Reward: ";

		screenAlbum.AddEmojiRecord();
	}

	public void OnClickContinue(){
		fader.FadeOut();
	}

	public void OnClickShare(){
		
	}
}
