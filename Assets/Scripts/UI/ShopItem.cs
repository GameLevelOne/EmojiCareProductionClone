using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrencyType{
	Cash,Gem,Coin
}

public enum ItemType{
	Gem,Coin,Furniture
}

public class ShopItem : MonoBehaviour {
	public delegate void ClickShopItem(CurrencyType itemCurrency,ItemType itemType,int itemPrice,int itemAmount,string itemDescription);
	public static event ClickShopItem OnClickShopItem;

	public CurrencyType itemCurrency;
	public ItemType itemType;
	public int itemPrice;
	public int itemAmount;
	public string itemDescrption;

	public void OnClick(){
		OnClickShopItem(itemCurrency,itemType,itemPrice,itemAmount,itemDescrption);
	}
}
