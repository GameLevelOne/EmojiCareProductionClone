using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTitleManager : MonoBehaviour {
	public Fader fader;
	public LoadingBar panelLoadingBar;
	string nextScene = "ScenePrologue";

//	void OnEnable(){
//		Fader.OnFadeOutFinished += HandleFadeOutFinished;
//	}
//
//	void OnDisable(){
//		Fader.OnFadeOutFinished -= HandleFadeOutFinished;
//	}

	void HandleFadeOutFinished(){
		
	}

	public void TapToStart ()
	{
		//fader.FadeOut();
		panelLoadingBar.gameObject.SetActive(true);
		panelLoadingBar.ChangeScene(nextScene);
	}

}
