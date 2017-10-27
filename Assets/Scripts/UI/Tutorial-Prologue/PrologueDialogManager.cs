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

	public void OnClickNext(){
		if((dialogCount>=1 && dialogCount<=2) || dialogCount == 5 || dialogCount == 8 || 
		(dialogCount>=20 && dialogCount<=23)){
			ChangeStorkSprite(StorkType.Happy);
		} else if(dialogCount == 3){
			ChangeStorkSprite(StorkType.Sad);
		} else if(dialogCount == 6 || dialogCount == 18){
			ChangeStorkSprite(StorkType.Carry);
		} else {
			ChangeStorkSprite(StorkType.Normal);
		}

		dialogTextBox.text = dialogList [dialogCount];

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
