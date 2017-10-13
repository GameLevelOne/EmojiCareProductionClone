﻿using System.Collections;
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
	AbleToSendOff,
	NotAbleToSendOff,
	BuyItem
}

public class ScreenPopup : BaseUI {
	public GameObject popupObject;
	public GameObject buttonGroupWarning;
	public GameObject buttonGroupConfirmation;
	public GameObject buttonOk;
	public GameObject buttonShop;
	public GameObject buttonTransfer;
	public Text popupText;

	Sprite tempEmojiSprite;
	string tempEmojiName;

	bool emojiTransfer = false;

	#region events
	public delegate void CelebrationNewEmoji(Sprite sprite,string emojiName);
	public static event CelebrationNewEmoji OnCelebrationNewEmoji;

	public delegate void SendOffEmoji(Sprite sprite,string emojiName);
	public static event SendOffEmoji OnSendOffEmoji;

	public delegate void TransferEmoji();
	public static event TransferEmoji OnTransferEmoji;
	#endregion

	PopupEventType currentEventType;
	PopupType currentPopupType;

	public void ShowPopup(PopupType type,PopupEventType eventType,bool toShop,bool toTransfer,Sprite sprite = null,string emojiName = null){
		currentEventType = eventType;
		currentPopupType = type;
		emojiTransfer = toTransfer;
		popupText.text = SetPopupText(eventType);
		if(type == PopupType.Warning){
			buttonGroupWarning.SetActive(true);
			buttonGroupConfirmation.SetActive(false);
		} else{
			buttonGroupConfirmation.SetActive(true);
			buttonGroupWarning.SetActive(false);
			if(toShop){
				buttonShop.SetActive(true);
				buttonTransfer.SetActive(false);
				buttonOk.SetActive(false);
			} else if(toTransfer){
				buttonShop.SetActive(false);
				buttonTransfer.SetActive(true);
				buttonOk.SetActive(false);
			} else{
				buttonShop.SetActive(false);
				buttonTransfer.SetActive(false);
				buttonOk.SetActive(true);
			}
		}

		if(sprite!=null){
			tempEmojiSprite=sprite;
		}
		if(emojiName!=null){
			tempEmojiName=emojiName;
		}

		base.ShowUI(popupObject);
	}

	string SetPopupText (PopupEventType eventType)
	{
		if (eventType == PopupEventType.SelectEmoji) {
			return "Do you want to choose this emoji?";
		} else if (eventType == PopupEventType.BuyEmoji) {
			return "Do you want to buy this emoji?";
		} else if (eventType == PopupEventType.AbleToSendOff) {
			return "Send off this emoji?";
		} else if (eventType == PopupEventType.NotAbleToSendOff) {
			return "Cannot send off yet";
		} else{
			return "";
		}
	} 

	public void OnClickButtonOk ()
	{
		if (currentPopupType == PopupType.Confirmation) {
			CloseUI (this.gameObject);
			if (currentEventType == PopupEventType.SelectEmoji || currentEventType == PopupEventType.BuyEmoji) {
				Debug.Log("new emoji");
				OnCelebrationNewEmoji(tempEmojiSprite,tempEmojiName);
			} else if(currentEventType == PopupEventType.AbleToSendOff){
				Debug.Log("send off");
				OnSendOffEmoji(tempEmojiSprite,tempEmojiName);
			} else if(currentEventType == PopupEventType.NotAbleToSendOff && emojiTransfer){
				Debug.Log("transfer");
				OnTransferEmoji();
			}
		} else{
			Debug.Log("3");
			CloseUI(this.gameObject);
		}

	}

	public void OnClickButtonCancel(){
		CloseUI(this.gameObject);
	}


}
