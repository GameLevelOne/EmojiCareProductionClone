using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenNewEmoji : BaseUI {
	public Fader fader;
	public SceneLoader sceneLoader;

	public Image emojiIcon;
	public Text emojiNameText;

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
		sceneLoader.NextScene = "SceneMain";
	}

	public void OnClickContinue(){
		fader.FadeOut();
	}

	public void ShowUI(Sprite sprite,string emojiName,GameObject obj){
		emojiIcon.sprite = sprite;
		emojiNameText.text = emojiName;
		base.ShowUI(obj);
	}
}
