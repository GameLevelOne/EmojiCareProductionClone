using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenNewEmoji : MonoBehaviour {
	public Fader fader;
	public SceneLoader sceneLoader;

	void OnEnable(){
		Fader.OnFadeOutFinished += OnFadeOutFinished;
	}

	void OnDisable(){
		
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
}
