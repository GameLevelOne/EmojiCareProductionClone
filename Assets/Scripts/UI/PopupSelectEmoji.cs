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
		int minRange = 0;
		int maxRange = 0;
		int sentOffEmoji = PlayerData.Instance.PlayerEmojiType;

//		//TODO: ADJUST THIS
//		if(PlayerData.Instance.PlayerSendOffCount == 1){
//			maxRange = 3;
//		} else if(PlayerData.Instance.PlayerSendOffCount == 2){
//			maxRange = 5;
//		} else if(PlayerData.Instance.PlayerSendOffCount == 3){
//			maxRange = 7;
//		} else if(PlayerData.Instance.PlayerSendOffCount == 4){
//			maxRange = 9;
//		} else if(PlayerData.Instance.PlayerSendOffCount == 5){
//			maxRange = 11;
//		} else {
//			maxRange = (int)EmojiType.COUNT;
//		}

		switch(sentOffEmoji){
		case 0:
			minRange = 0;
			maxRange = 2;
			break;
		case 1:
			minRange = 0;
			maxRange = 2;
			break;
		case 2:
			minRange = 2;
			maxRange = 4;
			break;
		case 3:
			minRange = 0;
			maxRange = 3;
			break;
		case 4:
			minRange = 4;
			maxRange = 6;
			break;
		case 5:
			minRange = 0;
			maxRange = 5;
			break;
		case 6:
			minRange = 6;
			maxRange = 8;
			break;
		case 7:
			minRange = 0;
			maxRange = 7;
			break;
		case 8:
			minRange = 8;
			maxRange = 10;
			break;
		case 9:
			minRange = 0;
			maxRange = 9;
			break;
		case 10:
			minRange = 0;
			maxRange = 10;
			break;
		}

		for(int i=0;i<optionList.Length;i++){
			optionList [i] = Random.Range (minRange, (maxRange+1));
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
