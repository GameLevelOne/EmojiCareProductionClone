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
	public Text userCoinText;
	public Text userGemText;
	public Text itemPriceText;

	public ScreenPopup screenPopup;

	public Text storeDescriptionText;
	public Text itemDescriptionText;

	public Animator shopScrollAnim;

	CurrencyType currentItemCurrency = CurrencyType.Gem;
	ItemType itemType;
	int currentItemPrice = 10;
	int currentItemAmount = 1000;
	string currentItemDescription = "1000 coins";
	string currentItemID;

	int selectedItem;
	ShopType selectedStore = ShopType.GemStore;
	ShopItem currentItem;

	string triggerRightToUtilStore = "rightToUtilStore";
	string triggerRightToDecorStore = "rightToDecorStore";
	string triggerLeftToUtilStore = "leftToUtilStore";
	string triggerLeftToGemStore = "leftToGemStore";

	int gemAmount1 = 100;
	int gemAmount2 = 500;
	int gemAmount3 = 1000;
	int gemAmount4 = 5000;

	string[] shopDescription = new string[3]{"Gem Store","Utility Store","Decoration Store"};

	void OnEnable (){
		InitShopDisplay();
		ShopItem.OnClickShopItem += OnClickShopItem;
		ScreenPopup.OnBuyCoin += OnBuyCoin;
		ScreenPopup.OnShopBuyFurniture += OnShopBuyFurniture;
		UnityIAPManager.Instance.OnFinishBuyProduct += OnFinishBuyProduct;
		UnityIAPManager.Instance.OnFailToBuyProduct += OnFailToBuyProduct;
	}

	void OnDisable(){
		ShopItem.OnClickShopItem -= OnClickShopItem;
		ScreenPopup.OnBuyCoin -= OnBuyCoin;
		ScreenPopup.OnShopBuyFurniture -= OnShopBuyFurniture;
		UnityIAPManager.Instance.OnFinishBuyProduct -= OnFinishBuyProduct;
		UnityIAPManager.Instance.OnFailToBuyProduct -= OnFailToBuyProduct;
	}

	void OnShopBuyFurniture ()
	{
		PlayerData.Instance.PlayerCoin -= currentItemPrice;
		UpdateCurrencyDisplay ();
		PlayerPrefs.SetInt (PlayerPrefKeys.Game.FURNITURE_VARIANT + currentItemID, 1);
		currentItem.SetOverlay ();
	}

	void OnBuyCoin ()
	{
		PlayerData.Instance.PlayerCoin += currentItemAmount;
		UpdateCurrencyDisplay ();
	}

	//void OnClickShopItem (CurrencyType itemCurrency, ItemType itemType, int itemPrice, int itemAmount,string itemDescription,string itemID)
	void OnClickShopItem (ShopItem thisItem){
		currentItem = thisItem;
		currentItemCurrency = thisItem.itemCurrency;
		currentItemPrice = thisItem.itemPrice;
		currentItemAmount = thisItem.itemAmount;
		currentItemDescription = thisItem.itemDescrption;
		currentItemID = thisItem.itemID;
		this.itemType = itemType;
		UpdateItemDescription ();
	}

	void OnFailToBuyProduct (string message)
	{
		screenPopup.ShowPopup (PopupType.Warning, PopupEventType.IAPFail, false,false,null,null,message);
	}

	void OnFinishBuyProduct (string productId)
	{
		if(productId == ShortCode.ProductIDs.id_Gem1){
			PlayerData.Instance.PlayerGem += gemAmount1;
		} else if(productId == ShortCode.ProductIDs.id_Gem2){
			PlayerData.Instance.PlayerGem += gemAmount2;
		} else if(productId == ShortCode.ProductIDs.id_Gem3){
			PlayerData.Instance.PlayerGem += gemAmount3;
		} else if(productId == ShortCode.ProductIDs.id_Gem4){
			PlayerData.Instance.PlayerGem += gemAmount4;
		}
		UpdateCurrencyDisplay ();
	}

	void UpdateCurrencyDisplay(){
		userGemText.text = PlayerData.Instance.PlayerGem.ToString ("N0");
		userCoinText.text = PlayerData.Instance.PlayerCoin.ToString ("N0");
	}

	void UpdateItemDescription(){
		itemDescriptionText.text = currentItemDescription;
		itemPriceText.text = currentItemPrice.ToString ("N0");
		if(currentItemCurrency == CurrencyType.Cash){
			displayCurrencies [0].SetActive (true);
			displayCurrencies [1].SetActive (false);
			displayCurrencies [2].SetActive (false);
		} else if(currentItemCurrency == CurrencyType.Gem){
			displayCurrencies [0].SetActive (false);
			displayCurrencies [1].SetActive (true);
			displayCurrencies [2].SetActive (false);
		} else if(currentItemCurrency == CurrencyType.Coin){
			displayCurrencies [0].SetActive (false);
			displayCurrencies [1].SetActive (false);
			displayCurrencies [2].SetActive (true);
		}
	}

	public void InitShopDisplay(){
		UpdateButtonDisplay();
		storeDescriptionText.text = shopDescription[(int)selectedStore];
	}

	public void OnClickShop(int shopIndex){
		parentShop.SetActive(false);
		parentShopContent.SetActive(true);
		parentShopContent.GetComponent<ScreenShopContent>().ShowUI(parentShopContent);
		UpdateItemDescription ();
		UpdateCurrencyDisplay ();
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
			string productID = "";
			if(currentItemCurrency == CurrencyType.Cash){
				if (currentItemPrice == gemAmount1)
					productID = ShortCode.ProductIDs.id_Gem1;
				else if (currentItemPrice == gemAmount2)
					productID = ShortCode.ProductIDs.id_Gem2;
				else if (currentItemPrice == gemAmount3)
					productID = ShortCode.ProductIDs.id_Gem3;
				else if (currentItemPrice == gemAmount4)
					productID = ShortCode.ProductIDs.id_Gem4;

				UnityIAPManager.Instance.BuyConsumable (productID);
			} 	
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
		UpdateButtonDisplay ();
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
