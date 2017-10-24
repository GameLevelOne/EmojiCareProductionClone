using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StorkType{
	Normal,
	Happy,
	Sad,
	Carry
}

public class PrologueDialogManager : MonoBehaviour {
	public Fader fader;
	public SceneLoader sceneLoader;
	public Text dialogTextBox;
	public ProloguePopupName popupName;
	public Image storkImage;
	public GameObject panelYesNo;

	public List<string> dialogList = new List<string>();
	public Sprite[] storkSprites;

	int dialogCount=0;
	int userChoice = 0; //yes=-1,no1=0,no2=1
	bool loadNextDialog = true;

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
			
		}
	}

	public void OnClickNext ()
	{
		if (dialogCount == 0) {
			ShowPopup ();
			loadNextDialog = false;
		} else if (dialogCount == 2) {
			dialogTextBox.text = dialogList [dialogCount] + popupName.username +",";
		} else if (dialogCount == 8) {
			ChangeStorkSprite (StorkType.Happy);
		} else if (dialogCount == 9) {
			ChangeStorkSprite (StorkType.Normal);
		} else if (dialogCount == 16) {
			ChangeStorkSprite (StorkType.Carry);
		} else if (dialogCount == 17) {
			ChangeStorkSprite (StorkType.Normal);
		} else if (dialogCount == 21) {
			TogglePanelYesNo (true);
			loadNextDialog = false;
		} else if ((dialogCount >= 22 && dialogCount <= 24) || dialogCount >= 26 && dialogCount <= 27) {
			ChangeStorkSprite (StorkType.Sad);
		} else if (dialogCount == 25) {
			ChangeStorkSprite (StorkType.Normal);
			loadNextDialog=false;
			TogglePanelYesNo(true);
		} else if(dialogCount == 28){
			ChangeStorkSprite(StorkType.Normal);
		} else if (dialogCount == 29) {
			loadNextDialog = false;
			TogglePanelYesNo(true);
		} else if (dialogCount == 30 || dialogCount == 31) {
			ChangeStorkSprite (StorkType.Happy);
		} else if (dialogCount == 32) {
			ChangeStorkSprite (StorkType.Carry);
		} else {
			ChangeStorkSprite (StorkType.Normal);
		}

		if (dialogCount != 2) {
			dialogTextBox.text = dialogList [dialogCount];
		}

		if (dialogCount < (dialogList.Count-1)) {
			if (loadNextDialog) {
				dialogCount++;
			}
		} else{
			fader.FadeOut();
			dialogCount=0;
		}
	}

	void ShowPopup(){
		popupName.ShowUI(popupName.gameObject);
	}

	public void ClosePopup(){
		popupName.CloseUI(popupName.gameObject);
		dialogCount=1;
		loadNextDialog=true;
		OnClickNext();
	}

	void ChangeStorkSprite(StorkType type){
		storkImage.sprite = storkSprites[(int)type];
	}

	void TogglePanelYesNo(bool value){
		panelYesNo.SetActive(value);
	}

	public void OnClickYes(){
		dialogCount=30;
		loadNextDialog=true;
		OnClickNext();
		TogglePanelYesNo(false);
	}

	public void OnClickNo(){
		if(userChoice == 0){
			dialogCount=22;
			userChoice=1;
		} else if(userChoice == 1){
			dialogCount=26;
			userChoice=0;
		}
		loadNextDialog=true;
		OnClickNext();
		TogglePanelYesNo(false);
	}
}
