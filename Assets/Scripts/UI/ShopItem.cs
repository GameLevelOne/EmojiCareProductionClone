using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CurrencyType{
	Cash,Gem,Coin
}

public enum ItemType{
	Gem,Coin,Furniture
}

public class ShopItem : MonoBehaviour {
	//public delegate void ClickShopItem(CurrencyType itemCurrency,ItemType itemType,int itemPrice,int itemAmount,string itemDescription,string itemID);
	public delegate void ClickShopItem(ShopItem thisItem);
	public static event ClickShopItem OnClickShopItem;

	public GameObject overlay;
	public CurrencyType itemCurrency;
	public ItemType itemType;
	public int itemPrice;
	public int itemAmount;
	public string itemDescrption;
	public string itemID;

	void OnEnable(){
		Init ();
	}

	public void Init(){
		if(overlay!=null){
			int pp = PlayerPrefs.GetInt (PlayerPrefKeys.Game.FURNITURE_VARIANT + itemID);
			if(pp == 0){
				overlay.SetActive (false);
				GetComponent<Button> ().interactable = true;
			} else{
				overlay.SetActive (true);
				GetComponent<Button> ().interactable = false;
			}
		}
	}

	public void OnClick(){
		//OnClickShopItem(itemCurrency,itemType,itemPrice,itemAmount,itemDescrption,itemID);
		OnClickShopItem (this);
	}

	public void SetOverlay(){
		overlay.SetActive (true);
	}
}
