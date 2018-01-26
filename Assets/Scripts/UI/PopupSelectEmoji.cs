using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSelectEmoji : BaseUI {
	public Fader fader;
	public SceneLoader sceneLoader;

	public GameObject popupInputName;
	public GameObject popupWarningEmptyName;
	public string username;

	int[] optionList = new int[3];

	void OnEnable(){
		Fader.OnFadeOutFinished += OnFadeOutFinished;
	}

	void OnDisable(){
		Fader.OnFadeOutFinished -= OnFadeOutFinished;
	}

	void OnFadeOutFinished(){
		Fader.OnFadeOutFinished -= OnFadeOutFinished;
		sceneLoader.gameObject.SetActive(true);
		sceneLoader.NextScene = "SceneMain";
	}

	void Start()
	{
		int maxType = 0;

//		if(PlayerData.Instance.PlayerSendOffCount == 1){
//			maxType = 3;
//		} else {
//			maxType = (int)EmojiType.COUNT;
//		}

		//TODO: ADJUST THIS
		if(PlayerData.Instance.PlayerSendOffCount == 1){
			maxType = 3;
		} else if(PlayerData.Instance.PlayerSendOffCount == 2){
			maxType = 5;
		} else if(PlayerData.Instance.PlayerSendOffCount == 3){
			maxType = 7;
		} else if(PlayerData.Instance.PlayerSendOffCount == 4){
			maxType = 10;
		} else if(PlayerData.Instance.PlayerSendOffCount == 5){
			maxType = 13;
		} else {
			maxType = (int)EmojiType.COUNT;
		}

		for(int i=0;i<optionList.Length;i++){
			optionList [i] = Random.Range (0, maxType);
		}
	}

	public void OnClickEmoji(int option){
		PlayerData.Instance.PlayerEmojiType = optionList [option];
		ShowUI (popupInputName);
	}

	public void OnInputName (UnityEngine.UI.InputField inputName)
	{
		username = inputName.text;
		PlayerData.Instance.EmojiName = username;
	}

	public void OnClickOK(){
		if(!string.IsNullOrEmpty(PlayerData.Instance.EmojiName)){
			fader.FadeOut ();
		} else{
			ShowUI (popupWarningEmptyName);
		}
	}
}
