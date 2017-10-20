using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
	public PopupManager popupManager;

	public Text textItemDescription;
	public Text textOwnedEmojiCount;
	public Text textInventoryCount;
	public Text textItemPrice;

	string[] tempItemDescription = new string[6]{"10 gems","20 gems","50 gems","100 gems","250 gems","500 gems"};
	string[] tempPrice = new string[6]{"10000","25000","50000","100000","250000","500000"};

	void Start(){

	}


	public void OnClickBuy(){
		bool enoughMoney = true;

		if(enoughMoney){
			popupManager.ShowPopup("Are you sure?",false,false);
		} else{
			popupManager.ShowPopup("Not Enough Money",false,true);
		}
	}




}
