using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShopContent : BaseUI {
	public ScreenShops screenShop;
	public GameObject parentShopChoice;

	public void CloseShopContent(){
		screenShop.UpdateButtonDisplay();
		base.ShowUI(parentShopChoice);
		base.CloseUI(this.gameObject);
	} 
	
}
