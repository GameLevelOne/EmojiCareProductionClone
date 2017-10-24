using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGMList{
	BGM1
}

public enum SFXList{
	SFX1
}

public enum VoiceList{
	Voice1
}

public class SoundManager : MonoBehaviour {

	private static SoundManager instance=null;
	public static SoundManager Instance{get {return instance;}}

	public AudioClip[] BGM;
	public AudioClip[] SFX;
	public AudioClip[] Voices;

	public AudioSource[] audioSource; //bgm,sfx,voice

	void Awake(){
		if(instance != null && instance != this){
			Destroy(this.gameObject);
		} else{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	public void PlayBGM(BGMList bgm){
		if(audioSource[0].clip == BGM[(int)bgm]){
			return;
		} else{
			audioSource[0].clip = BGM[(int)bgm];
			audioSource[0].Play();
		}
	}

	public void PlaySFX(SFXList sfx){
		audioSource[1].PlayOneShot(SFX[(int)sfx]);
	}

	public void PlayVoice(VoiceList voice){
		audioSource[2].PlayOneShot(Voices[(int)voice]);
	}

	public void SetAudioVolume(int type,float value){
		audioSource[type].volume = value;
	}
}
