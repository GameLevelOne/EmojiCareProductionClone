using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuidedTutorialStork : BaseUI {
	public GameObject tutorialObj;
	public string[] storkDialogs;
	public GameObject[] highlightPanels;
	public Vector3[] dialogBoxPositions; //top,middle,bottom
	public Transform dialogBox;
	public Text dialogText;
	public GameObject buttonNext;
	public Pan pan;

	int dialogCounter = 0;

	void OnDisable(){
		UIBowl.OnTutorialBowlFull -= OnTutorialBowlFull;
		Bowl.OnBowlOutsideFridge -= OnBowlOutsideFridge;
		pan.OnCookingStart -= OnCookingStart;
		pan.OnCookingDone -= OnCookingDone;
		Food.OnFoodEaten -= OnFoodEaten;
	}

	public void RegisterBowlEvent(){
		UIBowl.OnTutorialBowlFull += OnTutorialBowlFull;
		Bowl.OnBowlOutsideFridge += OnBowlOutsideFridge;
		pan.OnCookingStart += OnCookingStart;
		pan.OnCookingDone += OnCookingDone;
		Food.OnFoodEaten += OnFoodEaten;
	}

	void OnFoodEaten ()
	{
		ShowFirstDialog (12);
	}

	void OnCookingDone ()
	{
		ShowFirstDialog (10);
	}

	void OnCookingStart ()
	{
		ShowFirstDialog (9);
	}

	void OnBowlOutsideFridge (){
		ShowFirstDialog (8);
	}

	void OnTutorialBowlFull(){
		ShowFirstDialog (7);
	}

	public void ShowFirstDialog(int idx){
		transform.GetChild (0).GetComponent<Image> ().raycastTarget = true;
		buttonNext.SetActive (true);
		dialogCounter = idx;
		dialogText.text = storkDialogs [dialogCounter];
		SetMisc ();
		ShowUI (tutorialObj);
	}

	public void OnNextDialog(){
		transform.GetChild (0).GetComponent<Image> ().raycastTarget = true;
		buttonNext.SetActive (true);
		dialogCounter++;
		dialogText.text = storkDialogs [dialogCounter];
		SetMisc ();
	}

	public void SetMisc(){
		SetDialogBoxPosition ();
		SetHighlightPanels ();
		SetEvents ();
	}

	public void SetDialogBoxPosition(){
//		if(dialogCounter >= 0 && dialogCounter <= 6){
//			
//		} 
		if(dialogCounter == 7){
			dialogBox.localPosition = dialogBoxPositions [0];
		} else{
			dialogBox.localPosition = dialogBoxPositions [2];
		}
	}

	public void SetHighlightPanels (){
		TurnOffHighlightPanels ();

		if (dialogCounter == 1)
			highlightPanels [0].SetActive (true);
		else if (dialogCounter == 2)
			highlightPanels [1].SetActive (true);
		else if (dialogCounter == 4)
			highlightPanels [3].SetActive (true);
		else if (dialogCounter == 6)
			highlightPanels [4].SetActive (true);
		else if (dialogCounter == 7)
			highlightPanels [5].SetActive (true);
		else if (dialogCounter == 8)
			highlightPanels [6].SetActive (true);
		else if (dialogCounter == 11)
			highlightPanels [7].SetActive (true);
		else if (dialogCounter == 13)
			highlightPanels [8].SetActive (true);
		else if (dialogCounter == 16)
			highlightPanels [9].SetActive (true);

		foreach (GameObject obj in highlightPanels) {
			if (obj.activeSelf) {
				buttonNext.SetActive (false);
				transform.GetChild (0).GetComponent<Image> ().raycastTarget = false;
				break;
			}
		}
	}

	public void SetEvents(){
		float tresholdLow = 0.19f;
		Emoji playerEmoji = PlayerData.Instance.PlayerEmoji;
		if(dialogCounter == 0){
			playerEmoji.hunger.SetStats (tresholdLow*playerEmoji.hunger.MaxStatValue);
			//playerEmoji.hygiene.SetStats (tresholdLow * playerEmoji.hygiene.MaxStatValue);
		}
	}

	public void TurnOffHighlightPanels ()
	{
		foreach (GameObject obj in highlightPanels) {
			if (obj.activeSelf) {
				obj.SetActive (false);
			}
		}
	}

}
