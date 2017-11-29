using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTile : MonoBehaviour {
	public delegate void SelectExpression(Sprite item,string name,string condition,bool isLocked,float progress);
	public static event SelectExpression OnSelectExpression;

	public EmojiExpressionState exprType;
	public Image expressionIcon;
	public Image progressBarFill;
	public GameObject blackOverlay;
	string unlockCondition;
	string expressionName;
	float currentProgress;

	bool lockedExpression = true;

	public void InitTile(Sprite sprite,string name,string unlockCondition,float progress=0f){
		expressionIcon.sprite = sprite;
		expressionName=name;
		currentProgress = progress;
		this.unlockCondition = unlockCondition;

		if(progress >= 1){
			lockedExpression = false;
			blackOverlay.SetActive (false);
		} else{
			lockedExpression = true;
			blackOverlay.SetActive (true);
		}

		progressBarFill.fillAmount = progress;
	}

	public void OnClickTile(){
		OnSelectExpression(expressionIcon.sprite,expressionName,unlockCondition,lockedExpression,currentProgress);
	}

}
