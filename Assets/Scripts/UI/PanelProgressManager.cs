using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelProgressManager : MonoBehaviour {

	public Image progressBarFill;
	public Text progressText;
	public Image expressionIcon;
	public Text expressionNameText;
	public Text expressionUnlockConditionText;

	public GameObject expressionBoxPrefab;
	public RectTransform contentBox;

	public Sprite[] expressionIcons = new Sprite[40];
	public Sprite lockedExpression;

	int expressionTotalCount = 40;
	int tileWidth = 4;
	int tileHeight = 10;
	float expressionBoxWidth = 110f;
	float contentBoxMarginX = 50f;

	void Start(){
		InitContentBox();
	}

	void InitContentBox(){
		int exprTileIdx = 0;
		int unlockedExprIdx = 0;
		List<FaceAnimation> exprList = new List<FaceAnimation>(); //temp

		//for testing
		exprList.Add(FaceAnimation.Default);
		exprList.Add(FaceAnimation.Amused);
		exprList.Add(FaceAnimation.Cry);
		exprList.Add(FaceAnimation.Fidget);

		contentBox.sizeDelta = new Vector2(0,((float)tileHeight*expressionBoxWidth)+contentBoxMarginX);

		for(int i=0;i<tileHeight;i++){
			for(int j=0;j<tileWidth;j++){
				GameObject obj = Instantiate(expressionBoxPrefab,contentBox,false) as GameObject;
				obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-160+j*105,500-i*105);

				if(unlockedExprIdx < exprList.Count){
					if((int)exprList[unlockedExprIdx] == (exprTileIdx-1)){
						obj.transform.GetChild(0).GetComponent<Image>().sprite = expressionIcons[exprTileIdx-1];
						unlockedExprIdx++;
					}else{
						obj.transform.GetChild(0).GetComponent<Image>().sprite = lockedExpression;
					}

				}else{
					obj.transform.GetChild(0).GetComponent<Image>().sprite = lockedExpression;
				}

				exprTileIdx++;
			}
		}
	}
}
