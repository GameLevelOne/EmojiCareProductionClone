using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICoin : MonoBehaviour {
	public Text textCurrentCoin;
	public Text textItemPrice;

	string triggerOpenBox = "coinBoxOpen";
	string triggerCloseBox = "coinBoxClose";
	int currentCoin=0;
	int currentPrice=0;
	bool isBought = false;

	void ShowUI(){
		currentCoin = PlayerData.Instance.PlayerCoin;
		UpdateDisplay (currentCoin, currentPrice);
		GetComponent<Animator> ().SetTrigger (triggerOpenBox);
	}

	IEnumerator AutoCloseUI(){
		GetComponent<Animator> ().SetTrigger (triggerCloseBox);
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

	void OnEndDrag(){
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
