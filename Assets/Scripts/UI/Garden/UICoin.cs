using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICoin : MonoBehaviour {
	public Text textCurrentCoin;
	public Text textCurrentGem;
	public Text textItemPrice;
	public GameObject coinIcon;
	public GameObject gemIcon;

	string boolOpenBox = "coinBoxOpen";
	int currentCoin=0;
	int currentGem=0;
	int currentPrice=0;
	bool isBought = false;

	public void ShowUI (int price, bool isCoin)
	{
		if (isCoin) {
			coinIcon.SetActive (true);
			gemIcon.SetActive (false);
			textCurrentCoin.gameObject.SetActive (true);
			textCurrentGem.gameObject.SetActive (false);
			currentCoin = PlayerData.Instance.PlayerCoin;
			UpdateDisplay (currentCoin, price,isCoin);
		} else {
			coinIcon.SetActive (false);
			gemIcon.SetActive (true);
			textCurrentCoin.gameObject.SetActive (false);
			textCurrentGem.gameObject.SetActive (true);
			currentGem = PlayerData.Instance.PlayerGem;
			UpdateDisplay (currentGem, price,isCoin);
		}
		currentPrice = price;

		GetComponent<Animator> ().SetBool (boolOpenBox,true);
	}

	IEnumerator AutoCloseUI(){
		GetComponent<Animator> ().SetBool (boolOpenBox,false);
		yield return new WaitForSeconds(0.16f);
	}

	void UpdateDisplay(int currentCoin,int itemPrice,bool isCoin){
		if(isCoin){
			textCurrentCoin.text = currentCoin.ToString("N0");
			if(currentCoin < itemPrice){
				textCurrentCoin.color = Color.red;
			} else{
				textCurrentCoin.color = Color.black;
			}
		} else{
			textCurrentGem.text = currentCoin.ToString("N0");
			if(currentGem < itemPrice){
				textCurrentGem.color = Color.red;
			} else{
				textCurrentGem.color = Color.black;
			}
		}
		textItemPrice.text = "-"+itemPrice.ToString();
	}

	public void CloseUI(bool isBought){
//		Debug.Log ("close ui");
		if(isBought){
//			Debug.Log ("bought");
			PlayerData.Instance.PlayerCoin -= currentPrice;
			currentCoin -= currentPrice;
			StartCoroutine (AnimateCoin ());
		} else{
//			Debug.Log ("back");
			GetComponent<Animator> ().SetBool (boolOpenBox,false);
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
		textCurrentCoin.text = currentCoin.ToString ();
		GetComponent<Animator> ().SetBool (boolOpenBox,false);
	}
}
