using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSendOff : BaseUI {
	public Image emojiIcon;
	public Text emojiNameText;

	public void ShowUI(Sprite sprite,string emojiName,GameObject obj){
		emojiIcon.sprite = sprite;
		emojiNameText.text = emojiName;
		base.ShowUI(obj);
	}
}
