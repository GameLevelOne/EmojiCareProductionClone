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
}
