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

public class ShopManager : MonoBehaviour {
	public PopupManager popupManager;

	public Text textItemDescription;
	public Text textOwnedEmojiCount;
	public Text textInventoryCount;
	public Text textItemPrice;

	public GameObject buttonNext;
	public GameObject buttonPrev;

	public Animator shopScrollAnim;

	int selectedItem;
	ShopType selectedStore = ShopType.GemStore;

	string triggerRightToUtilStore = "rightToUtilStore";
	string triggerRightToDecorStore = "rightToDecorStore";
	string triggerLeftToUtilStore = "leftToUtilStore";
	string triggerLeftToGemStore = "leftToGemStore";

	string[] tempItemDescription = new string[6]{"10 gems","20 gems","50 gems","100 gems","250 gems","500 gems"};
	string[] tempPrice = new string[6]{"10000","25000","50000","100000","250000","500000"};

	void Start(){
		UpdateButtonDisplay();
	}

	public void OnClickShopItem(int itemId){
		selectedItem = itemId;
		if(itemId >=0 && itemId <= 5){
			selectedStore = ShopType.GemStore;
		} else if(itemId >= 6 && itemId <= 11){
			selectedStore = ShopType.UtilStore;
		} else if(itemId >= 12 && itemId <= 17){
			selectedStore = ShopType.DecorStore;
		}
		ShowItemDescription();
	}

	void ShowItemDescription(){
		textItemDescription.text = tempItemDescription[selectedItem];
		textItemPrice.text = tempPrice[selectedItem];
		if(selectedStore == ShopType.GemStore){
			textOwnedEmojiCount.gameObject.SetActive(false);
			textInventoryCount.gameObject.SetActive(false);
		} else if(selectedStore == ShopType.UtilStore){
			textOwnedEmojiCount.gameObject.SetActive(true);
			textInventoryCount.gameObject.SetActive(false);
		} else{
			textOwnedEmojiCount.gameObject.SetActive(false);
			textInventoryCount.gameObject.SetActive(true);
		}

		if(textOwnedEmojiCount.gameObject.activeSelf){
			//textOwnedExpr
		}
		if(textInventoryCount.gameObject.activeSelf){
			//textInv
		}
	}

	public void OnClickBuy(){
		bool enoughMoney = true;

		if(enoughMoney){
			popupManager.ShowPopup("Are you sure?",false,false);
		} else{
			popupManager.ShowPopup("Not Enough Money",false,true);
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

	void UpdateButtonDisplay ()
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
}
