using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuidedTutorialStork : BaseUI {
	public string[] storkDialogs;
	public GameObject[] highlightPanels;
	public Vector3[] dialogBoxPositions; //top,middle,bottom
	public Transform dialogBox;
	public Text dialogText;
	public GameObject buttonNext;

	int dialogCounter = 0;

	void Start(){
		Init ();
	}

	void Init(){
		SetEvents ();
		dialogText.text = storkDialogs [0];
	}

	public void OnNextDialog(){
		transform.GetChild (0).GetComponent<Image> ().raycastTarget = true;
		dialogCounter++;
		dialogText.text = storkDialogs [dialogCounter];
		SetDialogBoxPosition ();
		SetHighlightPanels ();
		SetEvents ();
	}

	public void SetDialogBoxPosition(){
		if(dialogCounter >= 0 && dialogCounter <= 2){
			dialogBox.localPosition = dialogBoxPositions [2];
		} 
	}

	public void SetHighlightPanels (){
		foreach (GameObject obj in highlightPanels) {
			if (obj.activeSelf) {
				obj.SetActive (false);
			}
		}
		if (dialogCounter == 1)
			highlightPanels [0].SetActive (true);
		else if (dialogCounter == 2)
			highlightPanels [1].SetActive (true);

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
			playerEmoji.hygiene.SetStats (tresholdLow * playerEmoji.hygiene.MaxStatValue);
		}
	}

}
