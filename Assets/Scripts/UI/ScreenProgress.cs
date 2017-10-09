using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenProgress : BaseUI {
	public ScreenPopup screenPopup;

	public GameObject buttonSend;
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
	bool canSendOff = false;
	string popupOpenTrigger = "PopupOpen";
	string popupCloseTrigger = "PopupClose";

	public override void InitUI ()
	{
		Debug.Log("progress");

		int exprTileIdx = 0;
		int unlockedExprIdx = 0;
		List<FaceExpression> exprList = new List<FaceExpression>(); //temp

		//for testing
		exprList.Add(FaceExpression.Default);
		exprList.Add(FaceExpression.Cry);
		exprList.Add(FaceExpression.Fidget);
		exprList.Add(FaceExpression.Amused);

		contentBox.sizeDelta = new Vector2(0,((float)tileHeight*expressionBoxWidth)+contentBoxMarginX);

		for(int i=0;i<tileHeight;i++){
			for(int j=0;j<tileWidth;j++){
				GameObject obj = Instantiate(expressionBoxPrefab,contentBox,false) as GameObject;
				obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-160+j*105,500-i*105);
				obj.GetComponent<AlbumTile>().exprType = (FaceExpression)exprTileIdx;
				obj.name = "Expr"+exprTileIdx.ToString();
				if(unlockedExprIdx < exprList.Count){
					if((int)exprList[unlockedExprIdx]-1 == exprTileIdx){
						Debug.Log((int)exprList[unlockedExprIdx]);
						obj.transform.GetChild(0).GetComponent<Image>().sprite = expressionIcons[exprTileIdx];
						unlockedExprIdx++;
					}else{
						obj.transform.GetChild(0).GetComponent<Image>().sprite = lockedExpression;
						obj.GetComponent<Button>().interactable=false;
					}

				}else{
					obj.transform.GetChild(0).GetComponent<Image>().sprite = lockedExpression;
					obj.GetComponent<Button>().interactable=false;
				}

				exprTileIdx++;
			}
		}
		float progressValue = (float)exprList.Count/(float)expressionTotalCount;
		progressBarFill.fillAmount = progressValue;
		if(progressValue>=1){
			canSendOff=true;
		}

		if(canSendOff){
			buttonSend.SetActive(true);
		} else{
			buttonSend.SetActive(false);
		}
	}

	void OnEnable(){
		AlbumTile.OnSelectExpression += OnSelectExpression;
	}

	void OnDisable(){
		AlbumTile.OnSelectExpression -= OnSelectExpression;
	}

	void OnSelectExpression (FaceExpression item)
	{
		expressionIcon.sprite = expressionIcons[(int)item];
	}

	public void ConfirmSendOff(){
		if(canSendOff){
			screenPopup.ShowPopup(PopupType.Confirmation,PopupEventType.SendOff,false);
		}
	}
}
