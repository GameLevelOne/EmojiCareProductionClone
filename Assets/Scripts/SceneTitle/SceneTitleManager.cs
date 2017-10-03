using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTitleManager : MonoBehaviour {
	public Fader fader;
	public LoadingBar panelLoadingBar;
	public GameObject loading;

	string nextScene;

	void Start(){
		Fader.OnFadeOutFinished += HandleFadeOutFinished;
		GameSparkManager.Instance.OnLoginSuccessful += GoToSceneMain;
	}

	void HandleFadeOutFinished(){
//		panelLoadingBar.gameObject.SetActive(true);
//		panelLoadingBar.NextScene = nextScene;
		Fader.OnFadeOutFinished -= HandleFadeOutFinished;

		SceneManager.LoadScene("SceneMain");
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
		GameSparkManager.Instance.GetDownloadableURL(PlayerData.Instance.PlayerEmojiType);
	}

	public void GoToSceneMain()
	{
		loading.SetActive(false);
		fader.FadeOut();
	}

}