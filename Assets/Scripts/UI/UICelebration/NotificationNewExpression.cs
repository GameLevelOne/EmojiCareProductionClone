using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationNewExpression : MonoBehaviour {
	public Transform parentFrame;
	Image expressionImage;
	Text expressionNameText;
	Text unlockDetailsText;
	Button continueButton;

	string triggerOpenNotif = "OpenNotif";
	string triggerCloseNotif = "CloseNotif";

	public void ShowUI(int expression,ExpressionIcons expressionIcons){
		EmojiType currentEmoji = PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType;

		ShowNotification();

		expressionImage = parentFrame.GetChild(0).GetComponent<Image>();
		expressionNameText = parentFrame.GetChild(1).GetComponent<Text>();
		unlockDetailsText = parentFrame.GetChild(2).GetComponent<Text>();
		continueButton = parentFrame.GetChild(3).GetComponent<Button>();
		continueButton.onClick.AddListener(OnClickContinue);

		expressionImage.sprite = expressionIcons.GetExpressionIcon(currentEmoji,expression);
		expressionNameText.text = expressionIcons.GetExpressionName(currentEmoji,expression);
		unlockDetailsText.text = expressionIcons.GetExpressionUnlockCondition(currentEmoji,expression);

		EmojiExpression emojiExpression = PlayerData.Instance.PlayerEmoji.emojiExpressions;

		emojiExpression.expressionProgress = ((float)emojiExpression.unlockedExpressions.Count / (float) emojiExpression.totalExpression)*100;
	}

	public void OnClickContinue(){
		StartCoroutine(WaitForAnim());
	}

	void ShowNotification(){
		gameObject.SetActive(true);
		GetComponent<Animator>().SetTrigger(triggerOpenNotif);
	}

	IEnumerator WaitForAnim(){
		GetComponent<Animator>().SetTrigger(triggerCloseNotif);
		yield return new WaitForSeconds(0.16f);
		//Destroy(gameObject);
	}
}
