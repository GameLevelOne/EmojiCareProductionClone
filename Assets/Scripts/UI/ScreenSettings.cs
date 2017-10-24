using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSettings : BaseUI {
	public Slider[] sliders;

	public void OnClickTAndC(){

	}

	public void OnClickCredits(){

	}

	public void AdjustMusicVolume(int type){
		SoundManager.Instance.SetAudioVolume(type,sliders[type].value);
	}
}
