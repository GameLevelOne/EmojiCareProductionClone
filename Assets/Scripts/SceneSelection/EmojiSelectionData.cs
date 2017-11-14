using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EmojiSelectionData : MonoBehaviour {

	public EmojiSO currentEmojiData;
	public GameObject priceBox;
	public Text textPrice;

	public delegate void EmojiClicked(bool needToBuy,Sprite sprite,EmojiSO emojiData);
	public static event EmojiClicked OnEmojiClicked;

	public bool needToBuy=false;

	Sprite tempEmojiIcon;

	public void InitEmoji(EmojiSO emojiData,Sprite emojiIcon){
		currentEmojiData = emojiData;
		tempEmojiIcon=emojiIcon;
		if(emojiData.price == 0){
			priceBox.SetActive(false);	
			needToBuy=false;
		} else{
			priceBox.SetActive(true);
			textPrice.text = emojiData.price.ToString();
			needToBuy=true;
		}
		//gameObject.name = emojiData.emojiType.ToString();
		gameObject.GetComponent<Image>().sprite = emojiIcon;
	}

	public void OnClick(){
		OnEmojiClicked(needToBuy,tempEmojiIcon,currentEmojiData);
	}

}
