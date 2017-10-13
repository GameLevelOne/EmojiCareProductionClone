using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenNewExpression : BaseUI {
	public Transform parentFrame;
	Image expressionImage;
	Text expressionNameText;
	Text unlockDetailsText;
	Button continueButton;

	public void ShowUI(int expression,ExpressionIcons expressionIcons){
		EmojiType currentEmoji = PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType;

		base.ShowUI(this.gameObject);

		expressionImage = parentFrame.GetChild(2).GetComponent<Image>();
		expressionNameText = parentFrame.GetChild(3).GetComponent<Text>();
		unlockDetailsText = parentFrame.GetChild(4).GetComponent<Text>();
		continueButton = parentFrame.GetChild(5).GetComponent<Button>();
		continueButton.onClick.AddListener(OnClickContinue);

		expressionImage.sprite = expressionIcons.GetExpressionIcon(currentEmoji,expression);
		expressionNameText.text = expressionIcons.GetExpressionName(currentEmoji,expression-1);
		unlockDetailsText.text = expressionIcons.GetExpressionUnlockCondition(currentEmoji,expression-1);

		EmojiExpression emojiExpression = PlayerData.Instance.PlayerEmoji.emojiExpressions;

		emojiExpression.expressionProgress = ((float)emojiExpression.unlockedExpressions.Count / (float) emojiExpression.totalExpression)*100;
	}

	public void OnClickContinue(){
		CloseUI(this.gameObject);
		Destroy(this.gameObject);
	}
}
