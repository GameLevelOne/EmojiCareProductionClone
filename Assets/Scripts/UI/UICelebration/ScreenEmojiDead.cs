using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEmojiDead : BaseUI {
	public ScreenAlbum screenAlbum;
	public SceneLoader sceneLoader;
	public Fader fader;
	public Image emojiIcon;

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

	public void TestEmojiDead(){
		PlayerData.Instance.PlayerEmoji.hunger.StatValue = 0;
		PlayerData.Instance.PlayerEmoji.hygiene.StatValue = 0;
		PlayerData.Instance.PlayerEmoji.happiness.StatValue = 0;
		PlayerData.Instance.PlayerEmoji.stamina.StatValue = 0;
		PlayerData.Instance.PlayerEmoji.health.StatValue = 0;
	}
}
