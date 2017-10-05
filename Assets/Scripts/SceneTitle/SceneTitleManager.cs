using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTitleManager : MonoBehaviour {
	public Fader fader;
	public SceneLoader sceneLoader;

	string nextScene;

	void Start(){
		Fader.OnFadeOutFinished += HandleFadeOutFinished;
		GameSparkManager.Instance.OnLoginSuccessful += GoToSceneMain;
	}

	void HandleFadeOutFinished(){
//		panelLoadingBar.gameObject.SetActive(true);
//		panelLoadingBar.NextScene = nextScene;
		sceneLoader.gameObject.SetActive(true);
		sceneLoader.NextScene = "SceneMain";
		Fader.OnFadeOutFinished -= HandleFadeOutFinished;

		//SceneManager.LoadScene("SceneMain");

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

	public void DownloadEmojiObject()
	{
//		GameSparkManager.Instance.GetDownloadableURL(PlayerData.Instance.PlayerEmojiType);
	}

	void GoToSceneMain()
	{
		fader.gameObject.SetActive(true);
		fader.FadeOut();
	}

}