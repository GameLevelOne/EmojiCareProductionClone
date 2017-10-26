using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSettings : BaseUI {
	public Slider[] sliders;

	void Start(){
		sliders[0].value = PlayerData.Instance.BGMVolume;
		sliders[1].value = PlayerData.Instance.SFXVolume;
		sliders[2].value = PlayerData.Instance.VoicesVolume;
	}

	public void OnClickTAndC(){

	}

	public void OnClickCredits(){

	}

	public void AdjustMusicVolume(int type){
		SoundManager.Instance.SetAudioVolume(type,sliders[type].value);
	}
}
