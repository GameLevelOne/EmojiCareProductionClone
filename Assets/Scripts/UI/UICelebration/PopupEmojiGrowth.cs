using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupEmojiGrowth : BaseUI {
	public EmojiIcons emojiIcons;
	public Text growthText;
	public Image emojiIcon;

	public void SetDisplay(bool isAdult){
		if(isAdult){
			growthText.text = PlayerData.Instance.EmojiName +" has grown into Adult!";
		} else{
			growthText.text = PlayerData.Instance.EmojiName +" has grown into Teen!";
		}

		emojiIcon.sprite = emojiIcons.GetEmojiIcon (PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType);
		ShowUI (gameObject);
	}
}
