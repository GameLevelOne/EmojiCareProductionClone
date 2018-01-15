using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ExpressionStatus{
	Hidden,
	Unlocked,
	Locked
}

public class ProgressTile : MonoBehaviour {
	public delegate void SelectExpression(Sprite item,string name,string condition,bool isLocked,float progress);
	public event SelectExpression OnSelectExpression;

	public EmojiExpressionState exprType;
	public Image expressionIcon;
	public Image progressBarFill;
	public GameObject lockIcon;
	public Image blackOverlay;
	string unlockCondition;
	string expressionName;
	float currentProgress;

	bool lockedExpression = true;

	public void InitTile(Sprite sprite,string name,string unlockCondition,float progress,ExpressionStatus status){
		expressionIcon.sprite = sprite;
		expressionName=name;
		currentProgress = progress;
		this.unlockCondition = unlockCondition;

//		if(progress >= 1){
//			lockedExpression = false;
//			blackOverlay.SetActive (false);
//		} else{
//			lockedExpression = true;
//			blackOverlay.SetActive (true);
//		}

		if(status == ExpressionStatus.Hidden){
			lockedExpression = true;
			lockIcon.SetActive (true);
			blackOverlay.enabled = true;
			blackOverlay.color = new Color (0.28f, 0.15f, 0.15f, 1f);
		} else if(status == ExpressionStatus.Locked){
			lockedExpression = false;
			lockIcon.SetActive (false);
			blackOverlay.enabled = true;
			blackOverlay.color = new Color (0.28f, 0.15f, 0.15f, 0.5f);
		} else if(status == ExpressionStatus.Unlocked){
			lockedExpression = false;
			lockIcon.SetActive (false);
			blackOverlay.enabled = false;
		}
		progressBarFill.fillAmount = progress;

		Debug.Log ("Expression name: " + name + " Status: " + status + " Progress: " + progress);
	}

	public void OnClickTile(){
		OnSelectExpression(expressionIcon.sprite,expressionName,unlockCondition,lockedExpression,currentProgress);
	}

}
