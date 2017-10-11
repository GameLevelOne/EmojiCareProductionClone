using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICelebrationManager : MonoBehaviour {
	public ScreenNewEmoji screenNewEmoji;
	public ScreenNewExpression screenNewExpression;
	public ScreenSendOff screenSendOff;
	public ScreenEmojiDeath screenEmojiDeath;
	public ScreenEmojiTransfer screenEmojiTransfer;

	public Transform canvasParent;
	public ScreenNewExpression screenNewExpressionPrefab;
	public ExpressionIcons expressionIcons;

	void OnEnable(){
		ScreenPopup.OnCelebrationNewEmoji += OnCelebrationNewEmoji;
		ScreenPopup.OnSendOffEmoji += OnSendOffEmoji;
		PlayerData.Instance.PlayerEmoji.emojiExpressions.OnNewExpression += OnNewExpression;
		PlayerData.Instance.PlayerEmoji.OnEmojiDead += OnEmojiDead;
	}

	void OnDisable(){
		ScreenPopup.OnCelebrationNewEmoji -= OnCelebrationNewEmoji;
		ScreenPopup.OnSendOffEmoji -= OnSendOffEmoji;
		PlayerData.Instance.PlayerEmoji.emojiExpressions.OnNewExpression -= OnNewExpression;
		PlayerData.Instance.PlayerEmoji.OnEmojiDead -= OnEmojiDead;
	}

	void OnCelebrationNewEmoji (Sprite sprite,string emojiName)
	{
		Debug.Log("new emoji");
		screenNewEmoji.ShowUI(sprite,emojiName,screenNewEmoji.gameObject);
	}

	void OnNewExpression (int newExpression)
	{
		ScreenNewExpression obj = Instantiate(screenNewExpressionPrefab,canvasParent,false) as ScreenNewExpression;
		obj.ShowUI(newExpression,expressionIcons);
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
		screenEmojiDeath.ShowUI(screenEmojiDeath.gameObject);
	}


	
}
