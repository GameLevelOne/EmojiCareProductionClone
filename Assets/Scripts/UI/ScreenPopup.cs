using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PopupType{
	Warning,
	Confirmation
}

public enum PopupEventType{
	SelectEmoji,
	BuyEmoji,
	SendOff,
	BuyItem
}

public class ScreenPopup : BaseUI {
	public GameObject popupObject;
	public GameObject buttonGroupWarning;
	public GameObject buttonGroupConfirmation;
	public GameObject buttonOk;
	public GameObject buttonShop;
	public Text popupText;

	public GameObject temp;

	PopupEventType currentEventType;

	public void ShowUI(PopupType type,PopupEventType eventType,bool toShop){
		currentEventType = eventType;
		popupText.text = SetPopupText(eventType);
		if(type == PopupType.Warning){
			buttonGroupWarning.SetActive(true);
		} else{
			buttonGroupConfirmation.SetActive(true);
			if(toShop){
				buttonShop.SetActive(true);
				buttonOk.SetActive(false);
			} else{
				buttonShop.SetActive(false);
				buttonOk.SetActive(true);
			}
		}
		base.ShowUI(popupObject);
	}

	string SetPopupText(PopupEventType eventType){
		if(eventType == PopupEventType.SelectEmoji){
			return "Do you want to choose this emoji?";
		} else if(eventType == PopupEventType.BuyEmoji){
			return "Do you want to buy this emoji?";
		} else{
			return "";
		}
	} 

	public void OnClickButtonOk(){
		if(currentEventType == PopupEventType.SelectEmoji || currentEventType == PopupEventType.BuyEmoji){
			CloseUI(this.gameObject);
			//base.ShowUI(base.UICelebrationPanels[0]);
			base.ShowUI(temp);
		} else{
			CloseUI(this.gameObject); //temp
		}

	}

	public void OnClickButtonCancel(){
		CloseUI(this.gameObject);
	}


}
