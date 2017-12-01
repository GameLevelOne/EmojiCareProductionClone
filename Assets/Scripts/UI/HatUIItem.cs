using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatUIItem : MonoBehaviour {
	public HatSO hatSO;
	public delegate void ClickHatUIItem(string hatID,int price,bool isBought,GameObject hatObj);
	public static event ClickHatUIItem OnClickHatUIItem;

	public void OnClickItem(){
		OnClickHatUIItem (hatSO.ID, hatSO.price, hatSO.isBought, hatSO.hatObject);
	}
}
