using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
	public delegate void ShowDialog();
	public static event ShowDialog OnShowDialog;

	public Text dialogText;

	string[] tempDialogs = new string[5]{"test1","test2","test3","test4","test5"};
	int dialogIdx = 0;

	void Start(){
		dialogText.text = tempDialogs[0];
	}

	 public void OnClickSingleDialog(){
	 	//destroy dialog instance?
	 }

	 public void OnClickMultipleDialogs(){
	 	//receive array as paramter?
	 	//check when all dialogs are shown, then destroy the instance

	 	int dialogCount = tempDialogs.Length;
		dialogIdx++;
	 	if(dialogIdx < dialogCount){
	 		dialogText.text = tempDialogs[dialogIdx];
	 	}
	 }
}
