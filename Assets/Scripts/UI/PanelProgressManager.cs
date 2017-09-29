using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelProgressManager : MonoBehaviour {
	public GameObject confirmSendOffPopup;
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

	void Start(){
		InitContentBox();
	}

	void OnEnable(){
		AlbumTile.OnSelectExpression += OnSelectExpression;
	}

	void OnDisable(){
		AlbumTile.OnSelectExpression -= OnSelectExpression;
	}

	void OnSelectExpression (FaceAnimation item)
	{
		expressionIcon.sprite = expressionIcons[(int)item];
	}

	void InitContentBox(){
		int exprTileIdx = 0;
		int unlockedExprIdx = 0;
		List<FaceAnimation> exprList = new List<FaceAnimation>(); //temp

		//for testing
		exprList.Add(FaceAnimation.Default);
		exprList.Add(FaceAnimation.Cry);
		exprList.Add(FaceAnimation.Fidget);
		exprList.Add(FaceAnimation.Amused);

		contentBox.sizeDelta = new Vector2(0,((float)tileHeight*expressionBoxWidth)+contentBoxMarginX);

		for(int i=0;i<tileHeight;i++){
			for(int j=0;j<tileWidth;j++){
				GameObject obj = Instantiate(expressionBoxPrefab,contentBox,false) as GameObject;
				obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-160+j*105,500-i*105);
				obj.GetComponent<AlbumTile>().exprType = (FaceAnimation)exprTileIdx;
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

	public void ConfirmSendOff(){
		if(canSendOff){
			confirmSendOffPopup.SetActive(true);
			confirmSendOffPopup.GetComponent<Animator>().SetTrigger(popupOpenTrigger);	
		}
	}

	public void ClosePopup(){
		confirmSendOffPopup.GetComponent<Animator>().SetTrigger(popupCloseTrigger);
		StartCoroutine(WaitForAnim(confirmSendOffPopup));
	}

	IEnumerator WaitForAnim(GameObject obj){
		yield return new WaitForSeconds(0.51f);
		obj.SetActive(false);
	}
}
