using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICoin : MonoBehaviour {
	public Text textCurrentCoin;
	public Text textItemPrice;

	string boolOpenBox = "coinBoxOpen";
	int currentCoin=0;
	int currentPrice=0;
	bool isBought = false;

	public void ShowUI(int price){
		currentCoin = PlayerData.Instance.PlayerCoin;
		currentPrice = price;
		UpdateDisplay (currentCoin, currentPrice);
		GetComponent<Animator> ().SetBool (boolOpenBox,true);
	}

	IEnumerator AutoCloseUI(){
		GetComponent<Animator> ().SetBool (boolOpenBox,false);
		yield return new WaitForSeconds(0.16f);
	}

	void UpdateDisplay(int currentCoin,int itemPrice){
		textCurrentCoin.text = currentCoin.ToString();
		textItemPrice.text = itemPrice.ToString();

		if(currentCoin < itemPrice){
			textCurrentCoin.color = Color.red;
		} else{
			textCurrentCoin.color = Color.black;
		}
	}

	public void CloseUI(){
		if(isBought){
			PlayerData.Instance.PlayerCoin -= currentPrice;
			StartCoroutine (AnimateCoin ());
		} else{
			StartCoroutine (AutoCloseUI ());
		}
	}

	IEnumerator AnimateCoin ()
	{
		int current = currentCoin;
		int target = currentCoin - currentPrice;

		while (current > target) {
			current = (int)Mathf.Lerp (current, target, Time.time);
			textCurrentCoin.text = current.ToString ();
			yield return null;
		}

		StartCoroutine (AutoCloseUI ());
	}
}
