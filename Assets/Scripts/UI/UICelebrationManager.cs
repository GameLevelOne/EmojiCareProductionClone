using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICelebrationManager : MonoBehaviour {
	public ScreenTutorial screenTutorial;
	public ScreenNewEmoji screenNewEmoji;
	public NotificationNewExpression notificationNewExpression;
	public NotificationNewExpression notificationExpressionProgress;
	public ScreenSendOff screenSendOff;
	public ScreenEmojiDead screenEmojiDead;
	public ScreenEmojiTransfer screenEmojiTransfer;

	public Transform canvasParent;
	public ExpressionIcons expressionIcons;
	public EmojiIcons emojiIcons;
	public ParticlePlayer particlePlayer;

	void OnEnable(){
		Debug.Log("celebration events");
		ScreenPopup.OnCelebrationNewEmoji += OnCelebrationNewEmoji;
		ScreenPopup.OnSendOffEmoji += OnSendOffEmoji;
		ScreenPopup.OnTransferEmoji += OnTransferEmoji;
		EmojiExpression.OnNewExpression += OnNewExpression;
		Emoji.OnEmojiDead += OnEmojiDead;
	}

	void OnDisable(){
		ScreenPopup.OnCelebrationNewEmoji -= OnCelebrationNewEmoji;
		ScreenPopup.OnSendOffEmoji -= OnSendOffEmoji;
		ScreenPopup.OnTransferEmoji -= OnTransferEmoji;
		EmojiExpression.OnNewExpression -= OnNewExpression;
		Emoji.OnEmojiDead -= OnEmojiDead;
	}

	void OnCelebrationNewEmoji (Sprite sprite,string emojiName)
	{
		Debug.Log("new emoji");
		screenNewEmoji.ShowUI(sprite,emojiName,screenNewEmoji.gameObject);
	}

	void OnNewExpression (int expressionStateIndex,bool isNewExpression)
	{
		Debug.Log("new expression");
		EmojiExpression expr = PlayerData.Instance.PlayerEmoji.emojiExpressions;
		if(expr.unlockedExpressions.Count >= expr.totalExpression){
			if(PlayerData.Instance.TutorialFirstExpressionFull == 0)
				screenTutorial.ShowFirstDialog (TutorialType.TriggerFirstExpressionFull);
		}

		StartCoroutine(WaitForNewExpression(expressionStateIndex,isNewExpression));
	}

	void OnSendOffEmoji (Sprite sprite, string emojiName)
	{
		Debug.Log("send off");
		Emoji currentEmoji = PlayerData.Instance.PlayerEmoji;
		screenSendOff.ShowUI(sprite,emojiName,screenSendOff.gameObject);
	}

	void OnEmojiDead ()
	{
		ResetData ();
		Debug.Log("emoji dead");

		if(PlayerData.Instance.TutorialFirstEmojiDead == 0){
			screenTutorial.ShowFirstDialog (TutorialType.TriggerFirstDead);
		}

		Sprite sprite = emojiIcons.GetEmojiIcon(PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType);
		screenEmojiDead.ShowUI(sprite,screenEmojiDead.gameObject);
	}

	void OnTransferEmoji ()
	{
		Debug.Log("emoji transferred");
		Sprite sprite = emojiIcons.GetEmojiIcon(PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType);
		screenEmojiTransfer.ShowUI(sprite,screenEmojiTransfer.gameObject);
	}

	IEnumerator WaitForNewExpression(int expressionStateIndex,bool isNewExpression){
		Debug.Log("wait");
		yield return new WaitForSeconds(2);
		NotificationNewExpression obj = null;
//		if(isNewExpression){
//			obj = Instantiate(notificationNewExpression,canvasParent,false) as NotificationNewExpression;
//		} else{
//			obj = Instantiate(notificationExpressionProgress,canvasParent,false) as NotificationNewExpression;
//		}
		obj.AddNotifToList (obj.gameObject);
		obj.ShowUI (expressionStateIndex, expressionIcons, particlePlayer, isNewExpression);
	}

	void ResetData(){
		PlayerPrefs.DeleteKey (PlayerPrefKeys.Emoji.HUNGER);
		PlayerPrefs.DeleteKey (PlayerPrefKeys.Emoji.HYGENE);
		PlayerPrefs.DeleteKey (PlayerPrefKeys.Emoji.HAPPINESS);
		PlayerPrefs.DeleteKey (PlayerPrefKeys.Emoji.STAMINA);
		PlayerPrefs.DeleteKey (PlayerPrefKeys.Emoji.HEALTH);
		PlayerPrefs.DeleteKey (PlayerPrefKeys.Player.LAST_TIME_PLAYED);
	}
}
