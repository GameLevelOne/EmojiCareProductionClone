using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ShopType{
	GemStore,UtilStore,DecorStore
}

public enum ShopItems{
	Gem1,Gem2,Gem3,Gem4,Gem5,Gem6,
	Util1,Util2,Util3,Util4,Util5,Util6,
	Decor1,Decor2,Decor3,Decor4,Decor5,Decor6
}

public class ScreenShops : BaseUI {
	public GameObject parentShop;
	public GameObject parentShopContent;
	public GameObject[] panelShopContents = new GameObject[3];
	public GameObject[] displayCurrencies = new GameObject[3];
	public GameObject buttonNext;
	public GameObject buttonPrev;
	public GameObject shopDescriptionBox;
	public GameObject itemDescriptionBox;

	public ScreenPopup screenPopup;

	public Text storeDescriptionText;
	public Text itemDescriptionText;

	public Animator shopScrollAnim;

	CurrencyType currentItemCurrency;
	ItemType itemType;
	int currentItemPrice;
	int currentItemAmount;

	int selectedItem;
	ShopType selectedStore = ShopType.GemStore;

	string triggerRightToUtilStore = "rightToUtilStore";
	string triggerRightToDecorStore = "rightToDecorStore";
	string triggerLeftToUtilStore = "leftToUtilStore";
	string triggerLeftToGemStore = "leftToGemStore";

	string[] shopDescription = new string[3]{"Gem Store","Utility Store","Decoration Store"};

	void OnEnable (){
		InitShopDisplay();
		ShopItem.OnClickShopItem += OnClickShopItem;
		ScreenPopup.OnBuyCoin += OnBuyCoin;
	}

	void OnDisable(){
		ShopItem.OnClickShopItem -= OnClickShopItem;
		ScreenPopup.OnBuyCoin -= OnBuyCoin;
	}

	void OnBuyCoin ()
	{
		PlayerData.Instance.PlayerCoin += currentItemAmount;
	}

	void OnClickShopItem (CurrencyType itemCurrency, ItemType itemType, int itemPrice, int itemAmount,string itemDescription)
	{
		currentItemCurrency = itemCurrency;
		currentItemPrice = itemPrice;
		currentItemAmount = itemAmount;
		itemDescriptionText.text = itemDescription;
		this.itemType = itemType;
	}

	public void InitShopDisplay(){
		UpdateButtonDisplay();
		storeDescriptionText.text = shopDescription[(int)selectedStore];
	}

	public void OnClickShop(int shopIndex){
		parentShop.SetActive(false);
		parentShopContent.SetActive(true);
		parentShopContent.GetComponent<ScreenShopContent>().ShowUI(parentShopContent);
		StartCoroutine(WaitForAnim(shopIndex));
	}

	void DisplayShopContent(int shopIndex){
		DisplayShopDescription(false);
		selectedStore = (ShopType)shopIndex;
		panelShopContents[shopIndex].SetActive(true);
		for(int i=0;i<panelShopContents.Length;i++){
			if(i == shopIndex){
				panelShopContents[i].SetActive(true);
				displayCurrencies[i].SetActive(true);
			}else{
				panelShopContents[i].SetActive(false);
				displayCurrencies[i].SetActive(false);
			}
		}
	}

	public void DisplayShopDescription(bool display){
		buttonNext.SetActive(display);
		buttonPrev.SetActive(display);
		shopDescriptionBox.SetActive(display);
		itemDescriptionBox.SetActive(!display);
	}

	public void ConfirmToBuyItem(){
		if(itemType == ItemType.Coin){
			if(PlayerData.Instance.PlayerGem >= currentItemPrice){
				screenPopup.ShowPopup (PopupType.Confirmation, PopupEventType.AbleToBuyCoin);
			} else{
				screenPopup.ShowPopup (PopupType.Warning, PopupEventType.NotAbleToBuyCoin);
			}
		} else if(itemType == ItemType.Furniture){
			if(PlayerData.Instance.PlayerCoin >= currentItemPrice){
				screenPopup.ShowPopup (PopupType.Confirmation, PopupEventType.AbleToBuyFurniture);
			} else{
				screenPopup.ShowPopup (PopupType.Warning, PopupEventType.NotAbleToBuyFurniture);
			}
		} else if(itemType == ItemType.Gem){
			//IAP transaction here
			//TEMP local transaction
			PlayerData.Instance.PlayerGem += currentItemAmount;
		}
	}

	public void OnClickNext(){
		if(selectedStore == ShopType.GemStore){
			shopScrollAnim.SetTrigger(triggerRightToUtilStore);
			selectedStore = ShopType.UtilStore;
		} else if(selectedStore == ShopType.UtilStore){
			shopScrollAnim.SetTrigger(triggerRightToDecorStore);
			selectedStore = ShopType.DecorStore;
		}
		UpdateButtonDisplay();
	}

	public void OnClickPrev(){
		if(selectedStore == ShopType.DecorStore){
			shopScrollAnim.SetTrigger(triggerLeftToUtilStore);
			selectedStore = ShopType.UtilStore;
		} else if(selectedStore == ShopType.UtilStore){
			shopScrollAnim.SetTrigger(triggerLeftToGemStore);
			selectedStore = ShopType.GemStore;
		}
		UpdateButtonDisplay();
	}

	public void OnClickBack(){
		parentShopContent.SetActive(false);
		parentShop.SetActive(true);
		DisplayShopDescription(true);
	}

	public void UpdateButtonDisplay ()
	{
		if (selectedStore == ShopType.GemStore) {
			buttonNext.SetActive (true);
			buttonPrev.SetActive (false);
		} else if (selectedStore == ShopType.UtilStore){
			buttonNext.SetActive (true);
			buttonPrev.SetActive (true);
		} else{
			buttonNext.SetActive(false);
			buttonPrev.SetActive(true);
		}
	}

	IEnumerator WaitForAnim(int shopIndex){
		yield return new WaitForSeconds(0.16f);
		DisplayShopContent(shopIndex);
	}
}
