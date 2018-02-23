using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenProgress : BaseUI {
	public GuidedTutorialStork guidedTutorial;
	public ScreenPopup screenPopup;
	public ScreenTutorial screenTutorial;
	public ExpressionIcons expressionIcons;
	public EmojiIcons emojiIcons;

	public GameObject hotkeyButton;
	public GameObject UIExpressionProgress;
	public Transform emojiScrollView;
	public GameObject emojiTypeObj;
	public GameObject buttonSend;
	public Image totalProgressBarFill;
	public Image indivProgressBarFill;
	public Text progressText;
	public Image expressionIcon;
	public Text expressionNameText;
	public Text expressionUnlockConditionText;
	public Text totalExpressionProgressText;

	public GameObject expressionBoxPrefab;
	public RectTransform contentBox;

	public Sprite lockedExpression;

	public bool openedFromEnvelope = false;

	int currentExpressionTile = 0;
	int tileWidth = 4;
	int tileHeight = 15;
	float expressionBoxWidth = 120f;
	float contentBoxMarginX = 100f;
	float currentTotalProgress = 0f;
	bool canSendOff = false;
	Emoji currentEmojiData;

	GameObject[] expressionObj = new GameObject[60];
	List<GameObject> emojiObj = new List<GameObject>();

	int unlockExpressionPriceGem = 10;

	void Start(){
		for(int i=0;i<expressionObj.Length;i++){
			GameObject obj = Instantiate(expressionBoxPrefab,contentBox,false) as GameObject;
			expressionObj [i] = obj;
		}
		screenPopup.OnGemUnlockExpression += OnGemUnlockExpression;
	}

	void OnDisable(){
		screenPopup.OnGemUnlockExpression -= OnGemUnlockExpression;
	}

	public override void InitUI ()
	{
		hotkeyButton.SetActive (false);
		List<EmojiType> availableEmoji = new List<EmojiType> ();
		int recordCount = PlayerData.Instance.EmojiRecordCount;
		bool newType = true;
		bool isInited = false;

		if (emojiObj.Count > 0) {
			isInited = true;
		}

		if (!isInited) {
			for (int i = 0; i <= recordCount; i++) {
				EmojiType type = (EmojiType)PlayerPrefs.GetInt (PlayerPrefKeys.Album.EMOJI_TYPE, 0);

				if (i == 0) {
					availableEmoji.Add (type);
				} else {
					foreach (EmojiType a in availableEmoji) {
						if (type == a) {
							newType = false;
							break;
						}
					}
				}

				if (newType) {
					if (i > 0) {
						availableEmoji.Add (type);
					}
					GameObject obj = Instantiate (emojiTypeObj, emojiScrollView, false) as GameObject;
					obj.transform.localPosition = new Vector3 (0 + 600 * (availableEmoji.IndexOf (type)), 0, 0);
					obj.GetComponent<Button> ().onClick.AddListener (delegate {
						OnClickEmoji (type);
					});
					obj.transform.GetChild (0).GetComponent<Image> ().sprite = emojiIcons.GetEmojiIcon (type);
					obj.transform.GetChild (1).GetComponent<Text> ().text = type.ToString ();
					emojiObj.Add (obj);
				}
			}
		}
	}

	public void InitExpressionUI (EmojiType currentEmojiType)
	{
//		if(PlayerData.Instance.TutorialFirstProgressUI == 0){
//			screenTutorial.ShowFirstDialog (TutorialType.FirstProgressUI);
//		}
		int exprTileIdx = 0;
		int unlockedExprIdx = 0;
		string condition = "";
		string name = "";
		currentEmojiData = PlayerData.Instance.PlayerEmoji;
		List<EmojiExpressionState> exprList = currentEmojiData.emojiExpressions.unlockedExpressions;

		SortList(exprList);
	
		contentBox.sizeDelta = new Vector2(0,((float)tileHeight*expressionBoxWidth)+contentBoxMarginX);

		for(int i=0;i<tileHeight;i++){
			for(int j=0;j<tileWidth;j++){
				expressionObj[exprTileIdx].GetComponent<RectTransform>().anchoredPosition = new Vector2(-200+j*125,880-i*125);
				expressionObj[exprTileIdx].GetComponent<ProgressTile>().exprType = (EmojiExpressionState)exprTileIdx;
				expressionObj[exprTileIdx].name = "Expr"+exprTileIdx.ToString();
				name = expressionIcons.GetExpressionName(currentEmojiType,exprTileIdx);
				Sprite sprite = expressionIcons.GetExpressionIcon(currentEmojiType,exprTileIdx);

				float fillAmount = currentEmojiData.emojiExpressions.expressionDataInstances [exprTileIdx].GetProgressRatio (currentEmojiType);
				ExpressionStatus status = 
					(ExpressionStatus)PlayerPrefs.GetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_STATUS +
					currentEmojiType.ToString () + ((EmojiExpressionState)exprTileIdx).ToString(), 0);

				expressionObj[exprTileIdx].GetComponent<ProgressTile>().InitTile(sprite,name,condition,fillAmount,status);
				expressionObj[exprTileIdx].GetComponent<ProgressTile> ().OnSelectExpression += OnSelectExpression;

				if(unlockedExprIdx < exprList.Count){
					if((int)exprList[unlockedExprIdx] == exprTileIdx){
						unlockedExprIdx++;
					}
				}
				exprTileIdx++;
			}
		}

		//float sendOffPercentage = currentEmojiData.emojiExpressions.sendOffProgressThreshold;
		currentTotalProgress = currentEmojiData.emojiExpressions.GetTotalExpressionProgress ();
		totalExpressionProgressText.text = (currentTotalProgress*100f).ToString() + "%";
		totalProgressBarFill.fillAmount = currentTotalProgress*0.8f;

		Debug.Log ("currentTotalProgress:"+currentTotalProgress);
		//Debug.Log ("sendOffPercentage:" + sendOffPercentage);

		if(currentTotalProgress>=1){
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
			screenPopup.ShowPopup(PopupType.Confirmation,PopupEventType.AbleToSendOff,false,sprite,emojiName);
		}else{
			screenPopup.ShowPopup(PopupType.Warning,PopupEventType.NotAbleToSendOff);
		}
	} 

	public void OnClickBack(){
		hotkeyButton.SetActive (true);
		for(int i=0;i<expressionObj.Length;i++){
			expressionObj [i].GetComponent<ProgressTile> ().OnSelectExpression -= OnSelectExpression;
		}

		foreach(GameObject obj in emojiObj){
			Destroy (obj);
		}
		emojiObj.Clear ();

		if(openedFromEnvelope){
			base.CloseUI (this.gameObject);
		} else{
			base.ClosePanelInHotkey (this.gameObject);
		}
		openedFromEnvelope = false;
	}

	void OnSelectExpression (EmojiExpressionState type,Sprite item, string expressionName, string condition, bool isLocked, float progress)
	{
		currentExpressionTile = (int)type;
		if (!isLocked) {
			expressionIcon.sprite = item;
			expressionNameText.text = expressionName;
			indivProgressBarFill.fillAmount = progress;
		} else{
			screenPopup.ShowPopup (PopupType.Confirmation, PopupEventType.ProgressUnlockExpressionGem, false, null, null, null, unlockExpressionPriceGem);
		}
	}

	void OnGemUnlockExpression ()
	{
		expressionObj [currentExpressionTile].GetComponent<ProgressTile> ().UnlockExpression ();
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

	public void OnClickEmoji(EmojiType typeIdx){
		if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == ShortCode.SCENE_GUIDED_TUTORIAL){
			StartCoroutine (WaitForPanel ());
		} else{
			base.ShowPanelInHotkey (UIExpressionProgress);
		}

		InitExpressionUI (typeIdx);
	}

	IEnumerator WaitForPanel(){
		guidedTutorial.ShowFirstDialog (64);
		yield return new WaitForSeconds (0.16f);
		base.ShowPanelInHotkey (UIExpressionProgress);
	}
}
