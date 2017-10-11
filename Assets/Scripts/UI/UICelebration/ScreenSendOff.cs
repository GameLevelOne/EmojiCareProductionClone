using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSendOff : BaseUI {
	public Image emojiIcon;
	public Text expressionProgress;

	public void ShowUI(Sprite sprite,string emojiName,GameObject obj){
		emojiIcon.sprite = sprite;
		base.ShowUI(obj);
	}
}
