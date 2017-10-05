using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCelebration : MonoBehaviour {

	private EmojiSO currEmojiData;

	public SceneLoader panelLoadingBar;
	public GameObject panelNewEmoji;
	public GameObject panelNewExpression;
	public GameObject panelSendOff;
	public Image emojiIcon;
	public Text emojiName; //temp
	public Image expressionIcon;
	public Text expressionName;
	public Text expressionUnlockCondition;

	public void ShowNewEmoji(EmojiSO data){
		currEmojiData = data;
//		emojiName.text = data.emojiName;
		//emojiIcon.sprite = currEmojiData.emojiSelectionIcon;
		panelNewEmoji.SetActive(true);
		panelNewExpression.SetActive(false);
	}

	public void ShowNewExpression(){
		panelNewEmoji.SetActive(false);
		panelNewExpression.SetActive(true);
	}

	public void ConfirmNewEmoji(){
		panelLoadingBar.gameObject.SetActive(true);
		panelLoadingBar.NextScene = "SceneMain"; //replace with actual name
	}
}
