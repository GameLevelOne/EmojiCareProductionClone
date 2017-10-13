using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICelebrationManager : MonoBehaviour {
	public ScreenNewEmoji screenNewEmoji;
	public ScreenNewExpression screenNewExpression;
	public ScreenSendOff screenSendOff;
	public ScreenEmojiDead screenEmojiDead;
	public ScreenEmojiTransfer screenEmojiTransfer;

	public Transform canvasParent;
	public ScreenNewExpression screenNewExpressionPrefab;
	public ExpressionIcons expressionIcons;
	public EmojiIcons emojiIcons;

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

	void OnNewExpression (int newExpression)
	{
		Debug.Log("new expression");
		StartCoroutine(WaitForNewExpression(newExpression));
	}

	void OnSendOffEmoji (Sprite sprite, string emojiName)
	{
		Debug.Log("send off");
		Emoji currentEmoji = PlayerData.Instance.PlayerEmoji;
		screenSendOff.ShowUI(sprite,emojiName,screenSendOff.gameObject);
	}

	void OnEmojiDead ()
	{
		Debug.Log("emoji dead");
		Sprite sprite = emojiIcons.GetEmojiIcon(PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType);
		screenEmojiDead.ShowUI(sprite,screenEmojiDead.gameObject);
	}

	void OnTransferEmoji ()
	{
		Debug.Log("emoji transferred");
		Sprite sprite = emojiIcons.GetEmojiIcon(PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType);
		screenEmojiTransfer.ShowUI(sprite,screenEmojiTransfer.gameObject);
	}

	IEnumerator WaitForNewExpression(int newExpression){
		yield return new WaitForSeconds(2);
		ScreenNewExpression obj = Instantiate(screenNewExpressionPrefab,canvasParent,false) as ScreenNewExpression;
		obj.ShowUI(newExpression,expressionIcons);
	}
}
