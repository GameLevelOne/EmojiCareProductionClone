using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTile : MonoBehaviour {
	public delegate void SelectExpression(Sprite item,string name,string condition,bool isLocked);
	public static event SelectExpression OnSelectExpression;

	public FaceExpression exprType;
	public Image expressionIcon;
	string unlockCondition;
	string expressionName;

	bool lockedExpression = true;

	public void InitTile(Sprite sprite,string name,string unlockCondition,bool locked){
		expressionIcon.sprite = sprite;
		expressionName=name;
		this.unlockCondition = unlockCondition;
		lockedExpression = locked;
	}

	public void OnClickTile(){
		OnSelectExpression(expressionIcon.sprite,expressionName,unlockCondition,lockedExpression);
	}

}
