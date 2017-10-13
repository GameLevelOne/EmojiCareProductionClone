using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEmojiTransfer : BaseUI {
	public SceneLoader sceneLoader;
	public Fader fader;
	public Image emojiIcon;
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

	public void ShowUI(Sprite sprite,GameObject obj){
		base.ShowUI(obj);
		this.sceneLoader = sceneLoader;
		emojiIcon.sprite = sprite;

		screenAlbum.AddEmojiRecord();
	}

	public void OnClickContinue(){
		fader.FadeOut();
	}
}
