using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTile : MonoBehaviour {
	public delegate void SelectExpression(Sprite item,string name,string condition,bool isLocked);
	public static event SelectExpression OnSelectExpression;

	public EmojiExpressionState exprType;
	public Image expressionIcon;
	public Image progressBarFill;
	string unlockCondition;
	string expressionName;

	bool lockedExpression = true;

	public void InitTile(Sprite sprite,string name,string unlockCondition,bool locked,float progress=0f){
		expressionIcon.sprite = sprite;
		expressionName=name;
		this.unlockCondition = unlockCondition;
		lockedExpression = locked;
		progressBarFill.fillAmount = PlayerData.Instance.PlayerEmoji.emojiExpressions.expressionDataInstances [(int)exprType].GetProgressRatio ();
	}

	public void OnClickTile(){
		OnSelectExpression(expressionIcon.sprite,expressionName,unlockCondition,lockedExpression);
	}

}
