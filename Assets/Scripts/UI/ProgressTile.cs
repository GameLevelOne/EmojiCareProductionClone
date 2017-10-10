using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTile : MonoBehaviour {
	public delegate void SelectExpression(FaceExpression item,bool isLocked);
	public static event SelectExpression OnSelectExpression;

	public FaceExpression exprType;
	public Image expressionIcon;

	bool lockedExpression = true;

	public void InitTile(Sprite sprite,bool locked){
		expressionIcon.sprite = sprite;
		lockedExpression = locked;
	}

	public void OnClickTile(){
		OnSelectExpression(exprType,lockedExpression);
	}

}
