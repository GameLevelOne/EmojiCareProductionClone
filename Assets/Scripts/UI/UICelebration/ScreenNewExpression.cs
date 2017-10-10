using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenNewExpression : BaseUI {
	public ExpressionIcons expressionIcons;
	public Image expressionImage;
	public Text expressionNameText;
	public Text unlockDetailsText;

	public void ShowUI(int expression){
		EmojiType currentEmoji = PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType;

		expressionImage.sprite = expressionIcons.GetExpressionIcon(currentEmoji,expression);

	}
}
