using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EmojiExpressionData {
	[Header("EmojiExpressionData Attributes. VIEW ONLY")]
	EmojiExpressionState expressionState;
	public int expressionCurrentProgress=0;
	public int expressionTotalProgress=0; //temp

	public EmojiExpressionData (int expressionState, int totalProgress)
	{
		this.expressionState = (EmojiExpressionState)expressionState;
		this.expressionTotalProgress = totalProgress;

		if (expressionState == 0) {
			expressionCurrentProgress = PlayerPrefs.GetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_PROGRESS + this.expressionState.ToString (), 1);
		} else {
			expressionCurrentProgress = PlayerPrefs.GetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_PROGRESS + this.expressionState.ToString (), 0);
		}
	}

	public void SetCurrentProgress(int value){
		expressionCurrentProgress = value;

		PlayerPrefs.SetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_PROGRESS + expressionState.ToString (), expressionCurrentProgress);
		PlayerPrefs.Save ();
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
//		Debug.Log ("currentprogress:" + expressionCurrentProgress);
		return expressionCurrentProgress;
	}

	public int GetTotalProgress(){
//		Debug.Log ("totalprogress:" + expressionTotalProgress);
		return expressionTotalProgress;
	}

	public float GetProgressRatio(){
		return ((float)expressionCurrentProgress / (float)expressionTotalProgress);
	}
}
