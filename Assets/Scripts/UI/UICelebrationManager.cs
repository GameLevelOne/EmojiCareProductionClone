using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICelebrationManager : MonoBehaviour {

	public ScreenTutorial screenTutorial;
	public ScreenNewEmoji screenNewEmoji;
	public GachaReward gachaReward;
	public NotificationNewExpression notificationExpressionProgress;
	public ScreenSendOff screenSendOff;
	public ScreenEmojiDead screenEmojiDead;
	public ScreenEmojiTransfer screenEmojiTransfer;
	public PopupEmojiGrowth popupEmojiGrowth;
	public PopupUnlockables popupUnlockables;

	public Transform canvasParent;
	public ExpressionIcons expressionIcons;
	public EmojiIcons emojiIcons;
	public ParticlePlayer particlePlayer;
	public GameObject buttonGacha;
	public Pan pan;

	bool hasInit = false;

	List<NotificationNewExpression> notificationObj = new List<NotificationNewExpression>();
	bool isShowingNotif = false;

	public void Init()
	{
		if(!hasInit){
			hasInit = true;
			ScreenPopup.OnCelebrationNewEmoji += OnCelebrationNewEmoji;
			ScreenPopup.OnSendOffEmoji += OnSendOffEmoji;
		}
	}

	public void RegisterEmojiEvents()
	{
		EmojiExpression.OnNewExpression += OnNewExpression;
		if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == ShortCode.SCENE_MAIN){
			PlayerData.Instance.PlayerEmoji.emojiGrowth.OnNewGrowth += OnEmojiGrow;
			Emoji.OnEmojiDead += OnEmojiDead;
			pan.OnCookingDone += OnCookingDone;
		}
	}

	public void UnregisterEmojiEvents ()
	{
		EmojiExpression.OnNewExpression -= OnNewExpression;
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name == ShortCode.SCENE_MAIN) {
			Emoji.OnEmojiDead -= OnEmojiDead;
			PlayerData.Instance.PlayerEmoji.emojiGrowth.OnNewGrowth -= OnEmojiGrow;
			pan.OnCookingDone -= OnCookingDone;
		}
	}

	void OnDestroy(){
		ScreenPopup.OnCelebrationNewEmoji -= OnCelebrationNewEmoji;
		ScreenPopup.OnSendOffEmoji -= OnSendOffEmoji;
		UnregisterEmojiEvents();
	}

	void OnCelebrationNewEmoji (Sprite sprite,string emojiName)
	{
//		Debug.Log("new emoji");
		screenNewEmoji.ShowUI(sprite,emojiName,screenNewEmoji.gameObject);
	}

	void OnNewExpression (int expressionStateIndex, bool isNewExpression)
	{
//		Debug.Log("new expression");
		EmojiExpression expr = PlayerData.Instance.PlayerEmoji.emojiExpressions;
//		if(expr.GetTotalExpressionProgress() >= expr.sendOffProgressThreshold){
//			if(PlayerData.Instance.TutorialFirstExpressionFull == 0)
//				screenTutorial.ShowFirstDialog (TutorialType.TriggerFirstExpressionFull);
//		} 

//		if(isNewExpression && PlayerData.Instance.TutorialFirstNewExpression == 0){
//			screenTutorial.ShowFirstDialog (TutorialType.firstNewExpression);
//			PlayerData.Instance.TutorialFirstNewExpression = 1;
//		}

		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name == ShortCode.SCENE_MAIN) {
			if (expr.GetTotalExpressionProgress () == 0.5f && PlayerData.Instance.MiniGameBlocks == 0) {
				popupUnlockables.SetDisplay (UnlockableList.MinigamePlayBlock);
			} else if (expr.GetTotalExpressionProgress () == 1 && PlayerData.Instance.MiniGamePainting == 0) {
				popupUnlockables.SetDisplay (UnlockableList.MinigameDrawingBoard);
			}
		}

		StartCoroutine(WaitForNewExpression(expressionStateIndex,isNewExpression));
	}

	void OnEmojiGrow (EmojiAgeType type)
	{
		popupEmojiGrowth.SetDisplay (type);
	}

	void OnSendOffEmoji (Sprite sprite, string emojiName)
	{
//		Debug.Log("send off");
		Emoji currentEmoji = PlayerData.Instance.PlayerEmoji;
		screenSendOff.ShowUI(sprite,emojiName,screenSendOff.gameObject);
		//popup unlock? adjust popup positions
	}

	void OnEmojiDead ()
	{
		ResetData ();
//		Debug.Log("emoji dead");
//		if(PlayerData.Instance.TutorialFirstEmojiDead == 0){
//			screenTutorial.ShowFirstDialog (TutorialType.TriggerFirstDead);
//		}

		Sprite sprite = emojiIcons.GetEmojiIcon(PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType);
		screenEmojiDead.ShowUI(sprite,screenEmojiDead.gameObject);
	}

	void OnTransferEmoji ()
	{
//		Debug.Log("emoji transferred");
		Sprite sprite = emojiIcons.GetEmojiIcon(PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType);
		screenEmojiTransfer.ShowUI(sprite,screenEmojiTransfer.gameObject);
	}

	void OnCookingDone ()
	{
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name == ShortCode.SCENE_MAIN) {
			PlayerData.Instance.CookCount++;
			if (PlayerData.Instance.CookCount == 10) {
				popupUnlockables.SetDisplay (UnlockableList.RecipeRamen);
			} else if (PlayerData.Instance.CookCount == 50) {
				popupUnlockables.SetDisplay (UnlockableList.RecipeBurger);
			} else if (PlayerData.Instance.CookCount == 100) {
				popupUnlockables.SetDisplay (UnlockableList.RecipeGrilledFish);
			}
		}
	}

	void ProcessNotification(){
		NotificationNewExpression obj = Instantiate(notificationExpressionProgress,canvasParent,false) as NotificationNewExpression;
		notificationObj.Add (obj);
	}

	void ResetData(){
		PlayerPrefs.DeleteKey (PlayerPrefKeys.Emoji.HUNGER);
		PlayerPrefs.DeleteKey (PlayerPrefKeys.Emoji.HYGENE);
		PlayerPrefs.DeleteKey (PlayerPrefKeys.Emoji.HAPPINESS);
		PlayerPrefs.DeleteKey (PlayerPrefKeys.Emoji.STAMINA);
		PlayerPrefs.DeleteKey (PlayerPrefKeys.Emoji.HEALTH);
		PlayerPrefs.DeleteKey (PlayerPrefKeys.Player.LAST_TIME_PLAYED);
	}

	IEnumerator WaitForNewExpression(int expressionStateIndex,bool isNewExpression){
//		Debug.Log("wait");
		NotificationNewExpression obj = Instantiate(notificationExpressionProgress,canvasParent,false) as NotificationNewExpression;
		if(isNewExpression){
			buttonGacha.SetActive (true);
			gachaReward.GetGachaReward ();
		} 
		notificationObj.Add (obj);
		StartCoroutine(ShowNotifExpression (expressionStateIndex, isNewExpression));
//		obj.AddNotifToList (obj.gameObject);
//		obj.ShowUI (expressionStateIndex, expressionIcons, particlePlayer, isNewExpression);
		yield return null;
	}

	IEnumerator ShowNotifExpression(int expressionStateIndex,bool isNewExpression){
		while(true){
			if(notificationObj.Count>0 && !isShowingNotif){
				isShowingNotif = true;
				//yield return new WaitForSeconds (2);
				notificationObj [0].gameObject.SetActive (true);
				notificationObj [0].ShowPopup (expressionStateIndex, expressionIcons, particlePlayer,isNewExpression);
				yield return new WaitForSeconds (2);

				notificationObj.RemoveAt (0);
				isShowingNotif = false;
			}
			yield return null;
		}
	}
}
