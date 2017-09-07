using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour {
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
		asop.allowSceneActivation = true;
	}

	public string NextScene{
		set{nextScene = value;}
	}

	public void ChangeScene(string nextScene){
		this.nextScene = nextScene;
		StartCoroutine(LoadNextScene());
	}

	IEnumerator LoadNextScene(){
		asop = SceneManager.LoadSceneAsync(nextScene);
		asop.allowSceneActivation = false;

		while(!barIsFilled){
			progressBar.fillAmount +=0.05f;
			if(progressBar.fillAmount == 1f){
				barIsFilled = true;
			}
			yield return null;
		}

		if(barIsFilled){
			fader.FadeOut();
		}
	}

}
