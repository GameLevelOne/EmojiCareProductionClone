using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChangeHat : MonoBehaviour {
	public Animator animator;
	string UIHatAnimTrigger = "UIHatOpen";
	GameObject currentHat;

	void OnEnable(){
		HatUIItem.OnClickHatUIItem += OnClickHatUIItem;
	}

	void OnDisable(){
		HatUIItem.OnClickHatUIItem -= OnClickHatUIItem;
	}

	void OnClickHatUIItem (string hatID, int price, bool isBought, GameObject hatObj)
	{
		currentHat = hatObj;
		
		PlayerData.Instance.PlayerEmoji.body.WearHat (hatID, hatObj);
	}

	public void ShowHatUI(){
		animator.SetBool (UIHatAnimTrigger, true);
	}

	public void CloseHatUI(){
		StartCoroutine (WaitForAnim ());
	}

	IEnumerator WaitForAnim(){
		animator.SetBool (UIHatAnimTrigger, false);
		yield return new WaitForSeconds (0.16f);
		this.gameObject.SetActive (false);
	}
}
