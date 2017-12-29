using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupEmojiGrowth : BaseUI {
	public EmojiIcons emojiIcons;
	public Text growthText;
	public Image emojiIcon;

	public void SetDisplay(){
		growthText.text = PlayerData.Instance.EmojiName +" has grown into Adult!";
		emojiIcon.sprite = emojiIcons.GetEmojiIcon (PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType);
		ShowUI (gameObject);
	}
}
