using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EmojiExpressionData {
	[Header("EmojiExpressionData Attributes. VIEW ONLY")]
	EmojiExpressionState expressionState;
	public int expressionCurrentProgress=0;
	public int expressionTotalProgress=0; //temp

	public EmojiExpressionData(int expressionState,int totalProgress){
		this.expressionState = (EmojiExpressionState)expressionState;
		this.expressionTotalProgress = totalProgress;

		expressionCurrentProgress = PlayerPrefs.GetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_PROGRESS + expressionState.ToString (), 0);
	}

	public void AddToCurrentProgress(int mod){
		if (expressionCurrentProgress < expressionTotalProgress)
			expressionCurrentProgress += mod;
		else
			expressionCurrentProgress = expressionTotalProgress;

		PlayerPrefs.SetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_PROGRESS + expressionState.ToString (), expressionCurrentProgress);
		PlayerPrefs.Save ();

	}

	public int GetCurrentProgress(){
		return expressionCurrentProgress;
	}

	public int GetTotalProgress(){
		return expressionTotalProgress;
	}

	public float GetProgressRatio(){
		return ((float)expressionCurrentProgress / (float)expressionTotalProgress);
	}
}
