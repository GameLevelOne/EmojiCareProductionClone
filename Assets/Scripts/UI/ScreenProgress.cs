using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenProgress : BaseUI {
	public ScreenPopup screenPopup;
	public ScreenTutorial screenTutorial;
	public ExpressionIcons expressionIcons;
	public EmojiIcons emojiIcons;

	public GameObject buttonSend;
	public Image progressBarFill;
	public Text progressText;
	public Image expressionIcon;
	public Text expressionNameText;
	public Text expressionUnlockConditionText;

	public GameObject expressionBoxPrefab;
	public RectTransform contentBox;

	public Sprite lockedExpression;

	int expressionTotalCount;
	int tileWidth = 4;
	int tileHeight = 15;
	float expressionBoxWidth = 110f;
	float contentBoxMarginX = 50f;
	bool canSendOff = false;
	Emoji currentEmojiData;

	public override void InitUI ()
	{
		if(PlayerData.Instance.TutorialFirstProgressUI == 0){
			screenTutorial.ShowFirstDialog (TutorialType.FirstProgressUI);
		}

		Debug.Log("progress");

		int exprTileIdx = 0;
		int unlockedExprIdx = 0;
		string condition = "";
		string name = "";
		currentEmojiData = PlayerData.Instance.PlayerEmoji;
		List<EmojiExpressionState> exprList = currentEmojiData.emojiExpressions.unlockedExpressions;

		expressionTotalCount = currentEmojiData.emojiExpressions.totalExpression;

		currentEmojiData.emojiExpressions.expressionProgress = (float)exprList.Count / (float)expressionTotalCount; 

		SortList(exprList);

		//for testing
//		exprList.Add(FaceExpression.Default);
//		exprList.Add(FaceExpression.Cry);
//		exprList.Add(FaceExpression.Fidget);
//		exprList.Add(FaceExpression.Amused);

		contentBox.sizeDelta = new Vector2(0,((float)tileHeight*expressionBoxWidth)+contentBoxMarginX);

		for(int i=0;i<tileHeight;i++){
			for(int j=0;j<tileWidth;j++){
				GameObject obj = Instantiate(expressionBoxPrefab,contentBox,false) as GameObject;
				obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200+j*125,770-i*125);
				obj.GetComponent<ProgressTile>().exprType = (EmojiExpressionState)exprTileIdx;
				obj.name = "Expr"+exprTileIdx.ToString();
				condition = expressionIcons.GetExpressionUnlockCondition(currentEmojiData.emojiBaseData.emojiType,exprTileIdx);
				name = expressionIcons.GetExpressionName(currentEmojiData.emojiBaseData.emojiType,exprTileIdx);

				if(unlockedExprIdx < exprList.Count){
					if((int)exprList[unlockedExprIdx] == exprTileIdx){
						Sprite sprite = expressionIcons.GetExpressionIcon(currentEmojiData.emojiBaseData.emojiType,(int)exprList[unlockedExprIdx]);

						obj.GetComponent<ProgressTile>().InitTile(sprite,name,condition,false);
						unlockedExprIdx++;
					}else{
						obj.GetComponent<ProgressTile>().InitTile(lockedExpression,name,condition,true);
						//obj.GetComponent<Button>().interactable=false;
					}

				}else{
					obj.GetComponent<ProgressTile>().InitTile(lockedExpression,name,condition,true);
					//obj.GetComponent<Button>().interactable=false;
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
			buttonSend.GetComponent<Image>().color = Color.white;
		} else{
			buttonSend.GetComponent<Image>().color = Color.gray;
		}
	}

	public void ConfirmSendOff(){
		EmojiType type = currentEmojiData.emojiBaseData.emojiType;
//		EmojiType type = EmojiType.Emoji;
		Sprite sprite = emojiIcons.GetEmojiIcon(type);
		string emojiName = type.ToString();
		if(canSendOff){
			screenPopup.ShowPopup(PopupType.Confirmation,PopupEventType.AbleToSendOff,false,false,sprite,emojiName);
		}else{
			screenPopup.ShowPopup(PopupType.Confirmation,PopupEventType.NotAbleToSendOff,false,true);
		}
	} 

	void OnEnable(){
		ProgressTile.OnSelectExpression += OnSelectExpression;
	}

	void OnDisable(){
		ProgressTile.OnSelectExpression -= OnSelectExpression;
	}

	void OnSelectExpression (Sprite item, string expressionName,string condition,bool isLocked)
	{
		if (!isLocked) {
			expressionIcon.sprite = item;
			expressionNameText.text = expressionName;
			expressionUnlockConditionText.text = condition;
		}
	}

	void SortList(List<EmojiExpressionState> list){
		EmojiExpressionState temp = EmojiExpressionState.DEFAULT;
		for(int i=1;i<list.Count;i++){
			for(int j=0;j<i;j++){
				if(list[i]<list[j]){
					temp = list[i];
					list[i]=list[j];
					list[j]=temp;
				}
			}
		}
	}
}
