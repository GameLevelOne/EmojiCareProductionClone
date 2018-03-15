using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	public Fader fader;
	public Image progressBar;
	string nextScene;
	bool barIsFilled = false;
	AsyncOperation asop;

	void OnEnable(){
		Fader.OnFadeOutFinished += HandleFadeOutFinished;
		Fader.OnFadeInFinished += HandleFadeInFinished;
		fader.FadeIn();
	}

	void OnDisable(){
		Fader.OnFadeOutFinished -= HandleFadeOutFinished;
		Fader.OnFadeInFinished -= HandleFadeInFinished;
	}

	void HandleFadeInFinished(){
		ChangeScene(nextScene);
	}

	void HandleFadeOutFinished ()
	{
		if(asop != null) asop.allowSceneActivation = true;
	}

	public string NextScene{
		set{nextScene = value;}
	}

	public void ChangeScene(string nextScene){
		this.nextScene = nextScene;
		StartCoroutine(LoadNextScene());
	}

	IEnumerator LoadNextScene(){
//		Debug.Log ("start load level async. loading scene: "+nextScene);
		asop = SceneManager.LoadSceneAsync(nextScene);
		asop.allowSceneActivation = false;

		while(!barIsFilled){
			float progress = Mathf.Clamp01 (asop.progress / 0.9f);
//			Debug.Log ("progress: " + progress);
			progressBar.fillAmount = progress;
			if(asop.progress >= 0.9f){
				barIsFilled = true;
			}
			yield return null;
		}

		if(barIsFilled){
//			Debug.Log ("finish load level async");
			fader.FadeOut();
		}
	}

}
