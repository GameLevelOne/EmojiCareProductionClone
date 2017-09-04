using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EmojiSelectionData : MonoBehaviour, IPointerClickHandler {
	public EmojiSO currEmojiData;
	public GameObject priceBox;
	public PopupSelection confirmationPopup;
	public Text textPrice;

	public delegate void EmojiClicked();
	public static event EmojiClicked OnEmojiClicked;

	public void InitEmoji(EmojiSO emojiData){
		currEmojiData = emojiData;
		if(emojiData.isUnlocked){
			priceBox.SetActive(false);	
		} else{
			priceBox.SetActive(true);
			textPrice.text = emojiData.emojiPrice.ToString();
		}
		gameObject.name = emojiData.emojiName;
		//gameObject.GetComponent<Image>().sprite = emojiSO[idx].emojiSelectionIcon;
	}

	public void OnPointerClick(PointerEventData ped){
		PopupSelection.Instance.ShowPopup(currEmojiData);
	}

}
