using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupEmojiGrowth : BaseUI {
	public EmojiIcons emojiIcons;
	public Text growthText;
	public Image emojiIcon;

	public void SetDisplay(EmojiAgeType type){
		if(type == EmojiAgeType.Adult){
			growthText.text = PlayerData.Instance.EmojiName +" has grown into Adult!";
			emojiIcon.sprite = emojiIcons.GetEmojiIcon (PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType);
		} else if(type == EmojiAgeType.Juvenille){
			growthText.text = PlayerData.Instance.EmojiName +" has grown into Teen!";
			emojiIcon.sprite = emojiIcons.GetBabyEmojiIcon(PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType);
		}


		ShowUI (gameObject);
	}

	public void OnClickOK(){
		//emoji growth
		PlayerData.Instance.PlayerEmoji.emojiGrowth.OnClosePopup ();
		CloseUI (gameObject);
	}
}
