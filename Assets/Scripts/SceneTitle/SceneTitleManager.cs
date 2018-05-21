using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTitleManager : MonoBehaviour {
	public Fader fader;
	public SceneLoader sceneLoader;
	public GameObject tapArea;

	string nextScene;

	void Awake(){
		Application.targetFrameRate = 60;
	}

	void Start(){
		SoundManager.Instance.PlayBGM(BGMList.BGMTitle);

		Input.multiTouchEnabled = false;

//		PlayerData.Instance.PlayerFirstPlay = 0;

		if(PlayerData.Instance.PlayerFirstPlay == 0) 
			PlayerPrefs.DeleteAll();
		Fader.OnFadeOutFinished += HandleFadeOutFinished;
//		GameSparkManager.Instance.OnLoginSuccessful += GoToSceneMain;
		GooglePlayGamesManager.Instance.OnFinishLogin += OnFinishLogin;

//		if(! string.IsNullOrEmpty(PlayerData.Instance.PlayerAuthToken)){
//			//authenticate with playertoken.(BUT HOW????)
//		}

		if (GooglePlayGamesManager.Instance)
			GooglePlayGamesManager.Instance.GPGSLogin ();
	}

	void OnFinishLogin ()
	{
		GooglePlayGamesManager.Instance.OnFinishLogin -= OnFinishLogin;
		fader.FadeIn();
	}

	void HandleFadeOutFinished(){
//		panelLoadingBar.gameObject.SetActive(true);
//		panelLoadingBar.NextScene = nextScene;
		sceneLoader.gameObject.SetActive(true);

		if(PlayerData.Instance.PlayerFirstPlay == 0){
			sceneLoader.NextScene = "ScenePrologue";
		} else{
			sceneLoader.NextScene = "SceneMain";
		}

		Fader.OnFadeOutFinished -= HandleFadeOutFinished;

		//SceneManager.LoadScene("SceneMain");

	}

	public void TapToStart ()
	{
		tapArea.SetActive (false);

		if (SoundManager.Instance)
			SoundManager.Instance.PlaySFXOneShot (SFXList.TapToStart);

		fader.FadeOut ();
	}

//	public void DownloadEmojiObject()
//	{
////		GameSparkManager.Instance.GetDownloadableURL(PlayerData.Instance.PlayerEmojiType);
//	}

	void GoToSceneMain()
	{
		fader.gameObject.SetActive(true);
		fader.FadeOut();
	}

}