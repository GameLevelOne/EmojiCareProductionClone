using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSelectEmoji : MonoBehaviour {
	public Fader fader;
	public SceneLoader sceneLoader;

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

		if(PlayerData.Instance.PlayerSendOffCount == 1){
			maxType = 3;
		} else {
			maxType = (int)EmojiType.COUNT;
		}

		for(int i=0;i<optionList.Length;i++){
			optionList [i] = Random.Range (0, maxType);
		}
	}

	public void OnClickEmoji(int option){
		//insert stork dialog here?

		PlayerData.Instance.PlayerEmojiType = optionList [option];

		//Debug.Log (PlayerData.Instance.PlayerEmojiType);

		fader.FadeOut ();
	}
}
