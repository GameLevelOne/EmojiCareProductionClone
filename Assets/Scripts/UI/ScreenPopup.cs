using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PopupType{
	Warning,
	Confirmation,
	AdsOrGems
}

public enum PopupEventType{
	SelectEmoji,
	BuyEmoji,
	AbleToSendOff,
	NotAbleToSendOff,
	BuyItem,
	NotAbleToBuyEmoji,
	AlbumLocked,
	AbleToBuyFurniture,
	NotAbleToBuyFurniture,
	RestockStall,
	RestockSeeds
}

public class ScreenPopup : BaseUI {
	public GameObject popupObject;
	public GameObject buttonGroupWarning;
	public GameObject buttonGroupConfirmation;
	public GameObject buttonGroupAdsAndGems;
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

	public delegate void BuyFurniture();
	public static event BuyFurniture OnBuyFurniture;

	public delegate void RefillStallWithAds();
	public static event RefillStallWithAds OnRefillStallWithAds;

	public delegate void RefillStallWithGems();
	public static event RefillStallWithGems OnRefillStallWithGems;
	#endregion

	PopupEventType currentEventType;
	PopupType currentPopupType;

	public void ShowPopup(PopupType type,PopupEventType eventType,bool toShop=false,bool toTransfer=false,Sprite sprite = null,string emojiName = null){
		currentEventType = eventType;
		currentPopupType = type;
		emojiTransfer = toTransfer;
		popupText.text = SetPopupText(eventType);
		if(type == PopupType.Warning){
			buttonGroupWarning.SetActive(true);
			buttonGroupConfirmation.SetActive(false);
			buttonGroupAdsAndGems.SetActive (false);
		} else if(type == PopupType.Confirmation){
			buttonGroupConfirmation.SetActive(true);
			buttonGroupWarning.SetActive(false);
			buttonGroupAdsAndGems.SetActive (false);
			if(toShop){
				//buttonShop.SetActive(true);
				buttonTransfer.SetActive(false);
				buttonOk.SetActive(false);
			} else if(toTransfer){
				//buttonShop.SetActive(false);
				buttonTransfer.SetActive(true);
				buttonOk.SetActive(false);
			} else{
				//buttonShop.SetActive(false);
				buttonTransfer.SetActive(false);
				buttonOk.SetActive(true);
			}
		} else{
			buttonGroupConfirmation.SetActive(false);
			buttonGroupWarning.SetActive(false);
			buttonGroupAdsAndGems.SetActive (true);
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
		} else if(eventType == PopupEventType.NotAbleToBuyEmoji){
			return "Not enough gems";
		} else if(eventType == PopupEventType.AlbumLocked){
			return "Finish your first emoji to unlock this menu";
		} else if(eventType == PopupEventType.AbleToBuyFurniture){
			return "Do you want to buy this furniture?";
		} else if(eventType == PopupEventType.NotAbleToBuyFurniture){
			return "Not enough coins";
		} else if(eventType == PopupEventType.RestockSeeds || eventType == PopupEventType.RestockStall){
			return "Do you want to refill?";
		}
		else{
			return "";
		}
	} 

	public void OnClickButtonOk ()
	{
		if (currentPopupType == PopupType.Confirmation) {
			base.ClosePopup (this.gameObject);
			if (currentEventType == PopupEventType.SelectEmoji || currentEventType == PopupEventType.BuyEmoji) {
				Debug.Log("new emoji");
				OnCelebrationNewEmoji(tempEmojiSprite,tempEmojiName);
			} else if(currentEventType == PopupEventType.AbleToSendOff){
				Debug.Log("send off");
				OnSendOffEmoji(tempEmojiSprite,tempEmojiName);
			} else if(currentEventType == PopupEventType.NotAbleToSendOff && emojiTransfer){
				Debug.Log("transfer");
				OnTransferEmoji();
			} else if(currentEventType == PopupEventType.AbleToBuyFurniture){
				Debug.Log ("buy furniture");
				OnBuyFurniture ();
			} 
		} else{
			Debug.Log("3");
			base.ClosePopup(this.gameObject);
		}

	}

	public void OnClickButtonCancel(){
		base.ClosePopup(this.gameObject);
	}

	public void RefillButtonWithAds ()
	{
		if (currentEventType == PopupEventType.RestockSeeds) {
			AdmobManager.Instance.ShowRewardedVideo (AdEvents.RestockSeeds);
		} else if(currentEventType == PopupEventType.RestockStall){
			AdmobManager.Instance.ShowRewardedVideo (AdEvents.RestockStall);
		}
		base.ClosePopup (this.gameObject);
	}

	public void RefillButtonWithGems(){
		OnRefillStallWithGems ();
	}


}
