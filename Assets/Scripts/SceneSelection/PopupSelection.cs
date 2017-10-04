using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSelection : MonoBehaviour {
	public Animator anim;
	public PanelCelebration panelCelebration;
	public GameObject buttonOk;
	public GameObject buttonPay;
	public Text textPrice;

	static PopupSelection instance = null;
	static EmojiSO currEmojiData;
	string triggerPopupOpen = "PopupOpen";
	string triggerPopupClose = "PopupClose";

	public static PopupSelection Instance{
		get{return instance;}
	}

	void Awake(){
		if(instance != null && instance !=this){
			Destroy(this.gameObject);
		} else{
			instance =this;
		}
	}

	public void ShowPopup(EmojiSO data){
		currEmojiData=data;
//		if(data.isUnlocked){
			buttonOk.SetActive(true);
			buttonPay.SetActive(false);
//		} else{
			buttonOk.SetActive(false);
			buttonPay.SetActive(true);
//			textPrice.text = data.emojiPrice.ToString();
//		}
		anim.SetTrigger(triggerPopupOpen);
	}

	public void ClosePopup(){
		anim.SetTrigger(triggerPopupClose);
	}

	public void ChooseEmoji(){
//		Debug.Log("Name: "+currEmojiData.emojiName);
//		Debug.Log("Price: "+currEmojiData.emojiPrice);
		anim.SetTrigger(triggerPopupClose);
		panelCelebration.ShowNewEmoji(currEmojiData);
	}
}
