using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EmojiExpressionData {
	[Header("EmojiExpressionData Attributes. VIEW ONLY")]
	EmojiExpressionState expressionState;
	EmojiType emojiType;
	public int expressionCurrentProgress=0;
	public int expressionTotalProgress=0; //temp

	public EmojiExpressionData (int expressionState, EmojiType emojiType, int totalProgress)
	{
		this.expressionState = (EmojiExpressionState)expressionState;
		this.emojiType = emojiType;
		this.expressionTotalProgress = totalProgress;

		if (expressionState == 0) {
			expressionCurrentProgress = PlayerPrefs.GetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_PROGRESS + emojiType.ToString()+this.expressionState.ToString (), 1);
			PlayerPrefs.SetFloat (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_PROGRESSRATIO +emojiType.ToString () +this.expressionState.ToString (), 1);
			PlayerPrefs.SetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_STATUS +emojiType.ToString () +this.expressionState.ToString (), (int)ExpressionStatus.Unlocked);
		} else {
			expressionCurrentProgress = PlayerPrefs.GetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_PROGRESS + emojiType.ToString()+this.expressionState.ToString (), 0);
		}
	}

	public void SetCurrentProgress(int value){
		expressionCurrentProgress = value;

		PlayerPrefs.SetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_PROGRESS + emojiType.ToString()+expressionState.ToString (), expressionCurrentProgress);
		Debug.Log (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_PROGRESS + emojiType.ToString () + expressionState.ToString ());
	}

	public void AddToCurrentProgress(int mod){
		if (expressionCurrentProgress < expressionTotalProgress)
			expressionCurrentProgress += mod;
		else
			expressionCurrentProgress = expressionTotalProgress;

		PlayerPrefs.SetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_PROGRESS + emojiType.ToString()+expressionState.ToString (), expressionCurrentProgress);

	}

	public float GetProgressRatio(EmojiType emojiType){
		float ratio = (float)expressionCurrentProgress / (float)expressionTotalProgress;

		PlayerPrefs.SetFloat (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_PROGRESSRATIO + emojiType.ToString () + expressionState.ToString (),
			ratio);

		return ratio;
	}
}
