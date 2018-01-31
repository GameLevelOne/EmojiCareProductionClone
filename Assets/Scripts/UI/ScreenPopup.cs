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
	ShopAbleToBuyFurniture,
	ShopNotAbleToBuyFurniture,
	RestockStall,
	RestockSeeds,
	NotAbleToRestock,
	ResetEmoji,
	ReviveEmoji,
	NotAbleToReviveEmoji,
	AbleToBuyCoin,
	NotAbleToBuyCoin,
	IAPFail,
	EmptyName
}

public class ScreenPopup : BaseUI {
	public UICoin uiCoin;
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
	string tempMessage;

	bool emojiTransfer = false;

	int reviveCost = 100; //TODO: ADJUST THIS LATER

	#region events
	public delegate void CelebrationNewEmoji(Sprite sprite,string emojiName);
	public static event CelebrationNewEmoji OnCelebrationNewEmoji;

	public delegate void SendOffEmoji(Sprite sprite,string emojiName);
	public static event SendOffEmoji OnSendOffEmoji;

	public delegate void TransferEmoji();
	public static event TransferEmoji OnTransferEmoji;

	public delegate void ResetEmoji();
	public static event ResetEmoji OnResetEmoji;

	public delegate void ReviveEmoji();
	public static event ReviveEmoji OnReviveEmoji;

	public delegate void BuyFurniture();
	public static event BuyFurniture OnBuyFurniture;

	public delegate void CancelBuyFurniture();
	public static event CancelBuyFurniture OnCancelBuyFurniture;

	public delegate void ShopBuyFurniture();
	public static event ShopBuyFurniture OnShopBuyFurniture;

	public delegate void BuyCoin();
	public static event BuyCoin OnBuyCoin;

	#endregion

	PopupEventType currentEventType;
	PopupType currentPopupType;

	public void ShowPopup(PopupType type,PopupEventType eventType,bool toShop=false,bool toTransfer=false,Sprite sprite = null,string emojiName = null,string message = null){
		currentEventType = eventType;
		currentPopupType = type;
		emojiTransfer = toTransfer;
		tempMessage = message;
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
				//buttonTransfer.SetActive(false);
				buttonOk.SetActive(false);
			} else if(toTransfer){
				//buttonShop.SetActive(false);
				//buttonTransfer.SetActive(true);
				buttonOk.SetActive(false);
			} else{
				//buttonShop.SetActive(false);
				//buttonTransfer.SetActive(false);
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
		} else if(eventType == PopupEventType.NotAbleToBuyEmoji || eventType == PopupEventType.NotAbleToReviveEmoji || eventType == PopupEventType.NotAbleToRestock){
			return "Not enough gems";
		} else if(eventType == PopupEventType.AlbumLocked){
			return "Finish your first emoji to unlock this menu";
		} else if(eventType == PopupEventType.AbleToBuyFurniture || eventType == PopupEventType.ShopAbleToBuyFurniture){
			return "Do you want to buy this furniture?";
		} else if(eventType == PopupEventType.NotAbleToBuyFurniture || eventType == PopupEventType.ShopNotAbleToBuyFurniture){
			return "Not enough coins";
		} else if(eventType == PopupEventType.AbleToBuyCoin){
			return "Are you sure?";
		} else if(eventType == PopupEventType.NotAbleToBuyCoin){
			return "Not enough gems";
		} else if(eventType == PopupEventType.RestockSeeds || eventType == PopupEventType.RestockStall){
			return "Do you want to refill?";
		} else if(eventType == PopupEventType.ResetEmoji){
			return "Do you want to start over? You will lose all your progress";
		} else if(eventType == PopupEventType.ReviveEmoji){
			return "Do you want to revive " + PlayerData.Instance.EmojiName + " ? You will not lose your progress";
		} else if(eventType == PopupEventType.IAPFail){
			return tempMessage;
		} else if(eventType == PopupEventType.EmptyName){
			return "Emoji name cannot be empty";
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
//				Debug.Log("new emoji");
				OnCelebrationNewEmoji(tempEmojiSprite,tempEmojiName);
			} else if(currentEventType == PopupEventType.AbleToSendOff){
//				Debug.Log("send off");
				OnSendOffEmoji(tempEmojiSprite,tempEmojiName);
			} else if(currentEventType == PopupEventType.NotAbleToSendOff && emojiTransfer){
//				Debug.Log("transfer");
				OnTransferEmoji();
			} else if(currentEventType == PopupEventType.AbleToBuyFurniture){
//				Debug.Log ("buy furniture edit room");
				OnBuyFurniture ();
			} else if(currentEventType == PopupEventType.ShopAbleToBuyFurniture){
//				Debug.Log ("buy furniture shop");
				OnShopBuyFurniture ();
			} else if(currentEventType == PopupEventType.ResetEmoji){
//				Debug.Log ("reset emoji");
				OnResetEmoji ();
			} else if(currentEventType == PopupEventType.ReviveEmoji){
//				Debug.Log ("revive emoji");
				if(PlayerData.Instance.PlayerGem>=reviveCost){
					PlayerData.Instance.PlayerGem -= reviveCost;
					OnReviveEmoji ();
				} else{
					ShowPopup (PopupType.Warning, PopupEventType.NotAbleToReviveEmoji);
				}

			} else if(currentEventType == PopupEventType.AbleToBuyCoin){
				OnBuyCoin ();
			}
		} else{
			base.ClosePopup(this.gameObject);
		}

	}

	public void OnClickButtonCancel(){
		if(currentEventType == PopupEventType.AbleToBuyFurniture){
			OnCancelBuyFurniture ();
		} else if(currentEventType == PopupEventType.RestockSeeds || currentEventType == PopupEventType.RestockStall){
			uiCoin.CloseUI (false);
		}

		base.ClosePopup(this.gameObject);
	}
}
