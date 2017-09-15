using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTitleManager : MonoBehaviour {
	public Fader fader;
	public LoadingBar panelLoadingBar;
	string nextScene;

	void OnEnable(){
		Fader.OnFadeOutFinished += HandleFadeOutFinished;
	}

	void OnDisable(){
		
	}

	void HandleFadeOutFinished(){
//		panelLoadingBar.gameObject.SetActive(true);
//		panelLoadingBar.NextScene = nextScene;
//		Fader.OnFadeOutFinished -= HandleFadeOutFinished;
	}

	public void TapToStart ()
	{
		fader.FadeOut();

//		if(PlayerData.Instance.FirstPlay){
//			nextScene = "ScenePrologue";
//		} else {
//			nextScene = "SceneSelection";
//		}
	}

}
