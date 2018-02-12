using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PopupType{
	Warning,
	Confirmation,
	AdsOrGems,
	Gems
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
	EmptyName,
	SpeedUpPlant,
	WakeEmojiUp,
	FreeCoinAds,
	FreeGemAds,
	NotEnoughCoins,
	NotEnoughGems
}

public class ScreenPopup : BaseUI {
	public UICoin uiCoin;
	public GameObject popupObject;
	public GameObject buttonGroupWarning;
	public GameObject buttonGroupConfirmation;
	public GameObject buttonGroupAdsAndGems;
	public GameObject buttonGroupGems;
	public GameObject buttonOk;
	public GameObject buttonShop;
	public GameObject buttonTransfer;
	public Text popupText;
	public Text gemText1;
	public Text gemText2;

	Sprite tempEmojiSprite;
	string tempEmojiName;
	string tempMessage;

	int reviveCost = 100; //TODO: ADJUST THIS LATER
	int instantHarvestCost = 20;

	#region events
	public delegate void CelebrationNewEmoji(Sprite sprite,string emojiName);
	public static event CelebrationNewEmoji OnCelebrationNewEmoji;

	public delegate void SendOffEmoji(Sprite sprite,string emojiName);
	public static event SendOffEmoji OnSendOffEmoji;

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

	public delegate void InstantHarvestPlant();
	public event InstantHarvestPlant OnInstantHarvestPlant;

	#endregion

	PopupEventType currentEventType;
	PopupType currentPopupType;

	void OnEnable(){
		if(AdmobManager.Instance) AdmobManager.Instance.OnFinishLoadVideoAds += OnFinishLoadVideoAds;
	}

	void OnDisable(){
		if (AdmobManager.Instance) AdmobManager.Instance.OnFinishLoadVideoAds -= OnFinishLoadVideoAds;
	}

	public void ShowPopup (PopupType type, PopupEventType eventType, bool toShop = false, Sprite sprite = null, string emojiName = null, string message = null, int gemPrice = 0)
	{
		currentEventType = eventType;
		currentPopupType = type;
		tempMessage = message;
		popupText.text = SetPopupText (eventType);
		if (type == PopupType.Warning) {
			buttonGroupWarning.SetActive (true);
			buttonGroupConfirmation.SetActive (false);
			buttonGroupAdsAndGems.SetActive (false);
			buttonGroupGems.SetActive (false);
		} else if (type == PopupType.Confirmation) {
			buttonGroupConfirmation.SetActive (true);
			buttonGroupWarning.SetActive (false);
			buttonGroupAdsAndGems.SetActive (false);
			if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name != ShortCode.SCENE_GUIDED_TUTORIAL) {
				buttonGroupGems.SetActive (false);
			}
			if(toShop){
				buttonOk.SetActive(false);
			} else{
				buttonOk.SetActive(true);
			}
		} else if(type == PopupType.AdsOrGems){
			buttonGroupConfirmation.SetActive(false);
			buttonGroupWarning.SetActive(false);
			buttonGroupAdsAndGems.SetActive (true);
			buttonGroupGems.SetActive (false);
			gemText1.text = gemPrice.ToString ();
		} else if(type == PopupType.Gems){
			buttonGroupConfirmation.SetActive(false);
			buttonGroupWarning.SetActive(false);
			buttonGroupAdsAndGems.SetActive (false);
			buttonGroupGems.SetActive (true);
			gemText2.text = gemPrice.ToString ();
		}
		if(sprite!=null){
			tempEmojiSprite=sprite;
		}
		if(emojiName!=null){
			tempEmojiName=emojiName;
		}

		ShowUI(popupObject);
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
		} else if(eventType == PopupEventType.NotAbleToBuyEmoji || eventType == PopupEventType.NotAbleToReviveEmoji || 
			eventType == PopupEventType.NotAbleToRestock || eventType == PopupEventType.NotAbleToBuyCoin || eventType == PopupEventType.NotEnoughGems){
			return "Not enough gems";
		} else if(eventType == PopupEventType.AlbumLocked){
			return "Finish your first emoji to unlock this menu";
		} else if(eventType == PopupEventType.AbleToBuyFurniture || eventType == PopupEventType.ShopAbleToBuyFurniture){
			return "Do you want to buy this furniture?";
		} else if(eventType == PopupEventType.NotAbleToBuyFurniture || eventType == PopupEventType.ShopNotAbleToBuyFurniture || 
			eventType == PopupEventType.NotEnoughCoins){
			return "Not enough coins";
		} else if(eventType == PopupEventType.AbleToBuyCoin){
			return "Are you sure?";
		} else if(eventType == PopupEventType.RestockSeeds || eventType == PopupEventType.RestockStall){
			return "Instantly restock with 10 gems?";
		} else if(eventType == PopupEventType.ResetEmoji){
			return "Do you want to start over? You will lose all your progress";
		} else if(eventType == PopupEventType.ReviveEmoji){
			return "Do you want to revive " + PlayerData.Instance.EmojiName + " ? You will not lose your progress";
		} else if(eventType == PopupEventType.IAPFail){
			return tempMessage;
		} else if(eventType == PopupEventType.EmptyName){
			return "Emoji name cannot be empty";
		} else if(eventType == PopupEventType.SpeedUpPlant){
			return "Watch ads to speed up for 10 minutes OR instantly harvest using 20 gems?";
		} else if(eventType == PopupEventType.WakeEmojiUp){
			return "Watch ads to get bonus stamina for "+PlayerData.Instance.EmojiName+" ?";
		} else if(eventType == PopupEventType.FreeCoinAds){
			return "Watch ads to get free 20 coins?";
		} else if(eventType == PopupEventType.FreeGemAds){
			return "Watch ads to get free 1 gem?";
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
				OnCelebrationNewEmoji (tempEmojiSprite, tempEmojiName);
			} else if (currentEventType == PopupEventType.AbleToSendOff) {
//				Debug.Log("send off");
				OnSendOffEmoji (tempEmojiSprite, tempEmojiName);
			} else if (currentEventType == PopupEventType.AbleToBuyFurniture) {
//				Debug.Log ("buy furniture edit room");
				OnBuyFurniture ();
			} else if (currentEventType == PopupEventType.ShopAbleToBuyFurniture) {
//				Debug.Log ("buy furniture shop");
				OnShopBuyFurniture ();
			} else if (currentEventType == PopupEventType.ResetEmoji) {
//				Debug.Log ("reset emoji");
				OnResetEmoji ();
			} else if (currentEventType == PopupEventType.ReviveEmoji) {
//				Debug.Log ("revive emoji");
				if (PlayerData.Instance.PlayerGem >= reviveCost) {
					PlayerData.Instance.PlayerGem -= reviveCost;
					OnReviveEmoji ();
				} else {
					ShowPopup (PopupType.Warning, PopupEventType.NotAbleToReviveEmoji);
				}
			} else if (currentEventType == PopupEventType.AbleToBuyCoin) {
				OnBuyCoin ();
			} else if (currentEventType == PopupEventType.SpeedUpPlant || currentEventType == PopupEventType.WakeEmojiUp 
						|| currentEventType == PopupEventType.FreeCoinAds || currentEventType == PopupEventType.FreeGemAds) {
				WatchAds ();
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

		ClosePopup(this.gameObject);
	}

	public void WatchAds ()
	{
		if (AdmobManager.Instance) {
			if (currentEventType == PopupEventType.SpeedUpPlant) {
				AdmobManager.Instance.ShowRewardedVideo (AdEvents.SpeedUpPlant);
			} else if(currentEventType == PopupEventType.WakeEmojiUp){
				AdmobManager.Instance.ShowRewardedVideo (AdEvents.WakeEmojiUp);
			} else if(currentEventType == PopupEventType.FreeCoinAds){
				AdmobManager.Instance.ShowRewardedVideo (AdEvents.FreeCoin);
			} else if(currentEventType == PopupEventType.FreeGemAds){
				AdmobManager.Instance.ShowRewardedVideo (AdEvents.FreeGem);
			} 
		}
	}

	public void OnClickButtonGem(){
		
		if(currentEventType == PopupEventType.SpeedUpPlant){
			uiCoin.CloseUI (false);
			ClosePopup (this.gameObject);
			if(PlayerData.Instance.PlayerGem >= instantHarvestCost){
				if (OnInstantHarvestPlant != null)
					OnInstantHarvestPlant ();
			} else{
				ShowPopup (PopupType.Warning, PopupEventType.NotAbleToRestock);
			}

		}
	}

	void OnFinishLoadVideoAds ()
	{
		ClosePopup (this.gameObject);
	}
}
