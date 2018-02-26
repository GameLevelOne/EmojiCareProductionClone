using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupEmojiGrowth : BaseUI {
	public EmojiIcons emojiIcons;
	public PopupUnlockables popupUnlockables;
	public Text growthText;
	public Image emojiIcon;

	bool toTeen = false;

	public void SetDisplay(EmojiAgeType type){
		if(type == EmojiAgeType.Adult){
			growthText.text = PlayerData.Instance.EmojiName +" has grown into Adult!";
			emojiIcon.sprite = emojiIcons.GetEmojiIcon (PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType);
		} else if(type == EmojiAgeType.Juvenille){
			growthText.text = PlayerData.Instance.EmojiName +" has grown into Teen!";
			emojiIcon.sprite = emojiIcons.GetBabyEmojiIcon(PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType);
			toTeen = true;
		}
		ShowUI (gameObject);
	}

	public void OnClickOK(){
		//emoji growth
		PlayerData.Instance.PlayerEmoji.emojiGrowth.OnClosePopup ();
		if(PlayerData.Instance.GardenField1 == 0)
			popupUnlockables.WaitForGrowthPopup ();
		CloseUI (gameObject);
	}
}
