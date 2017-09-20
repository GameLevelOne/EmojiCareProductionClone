using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour {
	public GameObject buttonGroupNotice;
	public GameObject buttonGroupConfirmation;
	public GameObject buttonShop;
	public Text textContent;
	public Animator popupAnim;

	static PopupManager instance = null;
	string popupOpenTrigger = "PopupOpen";
	string popupCloseTrigger = "PopupClose";

	public static PopupManager Instance {
		get{ return instance;}
	}

	// Use this for initialization
	void Awake () {
		if(instance != null && instance != this){
			Destroy(this.gameObject);
		}else{
			instance = this;
		}
	}

	public void ShowPopup(string text,bool isNotice,bool isShop){
		if(isNotice){
			buttonGroupNotice.SetActive(true);
			buttonGroupConfirmation.SetActive(false);
		} else{
			buttonGroupNotice.SetActive(false);
			buttonGroupConfirmation.SetActive(true);
			if(isShop){
				buttonShop.SetActive(true);
			}else{
				buttonShop.SetActive(false);
			}
		}
		textContent.text = text;
		popupAnim.SetTrigger(popupOpenTrigger);
	}

	public void ClosePopup(){
		popupAnim.SetTrigger(popupCloseTrigger);
	}

}
