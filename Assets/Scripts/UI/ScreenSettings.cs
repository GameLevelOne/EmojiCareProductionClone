using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSettings : BaseUI {
	public Slider[] sliders;

	public override void InitUI ()
	{
		sliders[0].value = PlayerData.Instance.BGMVolume;
		sliders[1].value = PlayerData.Instance.SFXVolume;
		sliders[2].value = PlayerData.Instance.VoicesVolume;
	}

	public void OnToggleDebug(bool debug)
	{
		print("debug = "+debug);
		PlayerData.Instance.PlayerEmoji.SwitchDebugMode(debug);
	}

	public void OnClickResetCoin(){
		PlayerData.Instance.PlayerCoin = 1000000;
	}

	public void OnClickResetGem(){
		PlayerData.Instance.PlayerGem = 10000;
	}

	public void AdjustMusicVolume(int type){
		SoundManager.Instance.SetAudioVolume(type,sliders[type].value);
	}

	public void OnClickSkipProgress(){
		EmojiExpression emojiExpression = PlayerData.Instance.PlayerEmoji.emojiExpressions;
		for (int i = 1; i < emojiExpression.expressionDataInstances.Length; i++) {
			if ((i < emojiExpression.totalExpressionForSendOff-1)) {
				emojiExpression.expressionDataInstances [i].SetCurrentProgress (emojiExpression.expressionDataInstances [i].expressionTotalProgress);
				PlayerPrefs.SetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_STATUS +
				PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType.ToString () + ((EmojiExpressionState)i).ToString (), 1);
			}
		}
	}
}
