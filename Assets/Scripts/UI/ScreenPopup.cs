using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PopupType{
	Warning,
	Confirmation
}

public class ScreenPopup : BaseUI {
	public GameObject buttonGroupWarning;
	public GameObject buttonGroupConfirmation;
	public GameObject popupObject;

	public void ShowUI(PopupType type){
		if(type == PopupType.Warning){
			buttonGroupWarning.SetActive(true);
		} else{
			buttonGroupConfirmation.SetActive(true);
		}
		base.ShowUI(popupObject);
	}
}
