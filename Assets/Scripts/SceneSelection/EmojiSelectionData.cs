using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EmojiSelectionData : MonoBehaviour {

	public EmojiSO currEmojiData;
	public GameObject priceBox;
	public PopupSelection confirmationPopup;
	public Text textPrice;

	public delegate void EmojiClicked(bool needToBuy);
	public static event EmojiClicked OnEmojiClicked;

	public bool needToBuy=false;

	public void InitEmoji(EmojiSO emojiData){
		//currEmojiData = emojiData;
//		if(emojiData.isUnlocked){
			priceBox.SetActive(false);	
//		} else{
			priceBox.SetActive(true);
//			textPrice.text = emojiData.emojiPrice.ToString();
//		}
//		gameObject.name = emojiData.emojiName;
//		gameObject.GetComponent<Image>().sprite = emojiData.emojiSelectionIcon;
	}

	public void OnClick(){
		OnEmojiClicked(needToBuy);
	}

}
