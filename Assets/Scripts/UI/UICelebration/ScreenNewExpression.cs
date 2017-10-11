using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenNewExpression : BaseUI {
	public Image expressionImage;
	public Text expressionNameText;
	public Text unlockDetailsText;

	public void ShowUI(int expression,ExpressionIcons expressionIcons){
		EmojiType currentEmoji = PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType;

		expressionImage.sprite = expressionIcons.GetExpressionIcon(currentEmoji,expression);
		expressionNameText.text = currentEmoji.ToString();
	}

	public void OnClickContinue(){
		CloseUI(this.gameObject);
		Destroy(this.gameObject);
	}
}
