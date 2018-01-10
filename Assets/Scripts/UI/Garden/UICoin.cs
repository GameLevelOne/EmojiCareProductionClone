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
	string boolShowPrice = "showPrice";
	int currentCoin=0;
	int currentGem=0;
	int currentPrice=0;
	bool isBought = false;
	bool priceIsShown = false;

	/// <summary>
	/// <para>Shows the UICoin. </para>
	/// <para>p1 = price / coins to get </para>
	/// <para>p2 = coin or gem </para>
	/// <para>p3 = show price text or not </para>
	/// <para>p4 = addition or subtraction</para>
	/// </summary>
	public void ShowUI (int coinAmount, bool isCoin, bool showPrice, bool addition)
	{
		Debug.Log ("show coin box");
		GetComponent<Animator> ().SetBool (boolOpenBox,true);
		priceIsShown = showPrice;

		if (isCoin) {
			coinIcon.SetActive (true);
			gemIcon.SetActive (false);
			textCurrentCoin.gameObject.SetActive (true);
			textCurrentGem.gameObject.SetActive (false);
			currentCoin = PlayerData.Instance.PlayerCoin;
			if (addition) {
				StartCoroutine (AnimateCoin (currentCoin, (currentCoin + coinAmount)));
			} else {
				UpdateDisplay (currentCoin, coinAmount, isCoin);
			}
		} else {
			coinIcon.SetActive (false);
			gemIcon.SetActive (true);
			textCurrentCoin.gameObject.SetActive (false);
			textCurrentGem.gameObject.SetActive (true);
			currentGem = PlayerData.Instance.PlayerGem;
			if(addition){
				currentGem++;
				PlayerData.Instance.PlayerGem = currentGem;
				StartCoroutine (AutoCloseUI ());
			}
			UpdateDisplay (currentGem, coinAmount,isCoin);
		}
		currentPrice = coinAmount;
		if(priceIsShown){
			Debug.Log ("showprice");
			Debug.Log (textItemPrice.transform.parent.GetComponent<Animator> ());
			StartCoroutine (ShowPrice ());
		}
	}

	IEnumerator ShowPrice(){
		yield return new WaitForSeconds (0.1f);
		textItemPrice.transform.parent.GetComponent<Animator> ().SetBool (boolShowPrice, true);
	}

	IEnumerator AutoCloseUI(){
		yield return new WaitForSeconds (1);
		GetComponent<Animator> ().SetBool (boolOpenBox,false);
		if(priceIsShown){
			textItemPrice.transform.parent.GetComponent<Animator> ().SetBool (boolShowPrice, false);
		}
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
		Debug.Log ("close ui");
		if(isBought){
//			Debug.Log ("bought");
			StartCoroutine (AnimateCoin (currentCoin,(currentCoin-currentPrice)));
		} else{
//			Debug.Log ("back");
			GetComponent<Animator> ().SetBool (boolOpenBox,false);
			if(priceIsShown){
				textItemPrice.transform.parent.GetComponent<Animator> ().SetBool (boolShowPrice, false);
			}
		}
	}

	IEnumerator AnimateCoin (int current,int target)
	{
		float timer = 0;

		while (timer<1) {
			current = (int)Mathf.Lerp (current, target, timer);
			textCurrentCoin.text = current.ToString ("N0");
			timer += Time.deltaTime;
			yield return null;
		}
		timer = 0;
		currentCoin = current;
		PlayerData.Instance.PlayerCoin = currentCoin;
		textCurrentCoin.text = currentCoin.ToString ("N0");
		GetComponent<Animator> ().SetBool (boolOpenBox,false);
		if(priceIsShown){
			textItemPrice.transform.parent.GetComponent<Animator> ().SetBool (boolShowPrice, false);
		}
	}
}
