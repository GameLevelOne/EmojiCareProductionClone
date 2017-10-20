using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrologueDialogManager : MonoBehaviour {
	public Fader fader;
	public SceneLoader sceneLoader;
	public Text dialogTextBox;

	public List<string> dialogList = new List<string>();

	int dialogCount=0;

	void Start () {
		Fader.OnFadeOutFinished += OnFadeOutFinished;
		dialogTextBox.text = dialogList [dialogCount];
	}

	void OnFadeOutFinished ()
	{
		sceneLoader.gameObject.SetActive(true);
		sceneLoader.NextScene = "SceneMain";
		Fader.OnFadeOutFinished -= OnFadeOutFinished;
	}

	public void LoadNextDialog ()
	{
		dialogCount++;
		if (dialogCount < dialogList.Count) {
			dialogTextBox.text = dialogList [dialogCount];
		}else{
			fader.FadeOut();
			dialogCount=0;
		}
	}

}
