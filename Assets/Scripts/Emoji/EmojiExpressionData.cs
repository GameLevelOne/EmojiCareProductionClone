using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiExpressionData {

	int expressionState;
	int expressionCurrentProgress=0;
	int expressionTotalProgress=0; //temp

	public EmojiExpressionData(int expressionState,int totalProgress){
		this.expressionState = expressionState;
		this.expressionTotalProgress = totalProgress;
	}

	public void AddToCurrentProgress(int mod){
		if (expressionCurrentProgress < expressionTotalProgress)
			expressionCurrentProgress += mod;
		else
			expressionCurrentProgress = expressionTotalProgress;
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
