using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGMList{
	BGMMain,
	BGMRadio1,
	BGMRadio2
}

public enum SFXList{
	Blop,
	Bounce,
	Cook,
	OpenThings,
	Shower,
	Sponge,
	Ding,
	AlarmClock,
	Globe,
	Bong
}

public enum VoiceList{
	Gulp,
	Huh,
	Laugh,
	Mmm,
	Sigh,
	UghYuck,
	Urrh,
	Yo,
	Yuck
}

public class SoundManager : MonoBehaviour {
	private static SoundManager instance=null;
	public static SoundManager Instance{get {return instance;}}

	public AudioClip[] BGMClips;
	public AudioClip[] SFXClips;
	public AudioClip[] VoiceClips;

	public AudioSource BGMSource; //bgm
	public AudioSource SFXSource; //sfx,voice

	void Awake()
	{
		if(instance != null && instance != this){
			Destroy(this.gameObject);
		} else{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	public void PlayBGM(BGMList bgm)
	{
		BGMSource.Stop();
		BGMSource.clip = BGMClips[(int)bgm];
		BGMSource.Play();
	}

	public void StopBGM()
	{
		BGMSource.Stop();
	}

	public void PlaySFXOneShot(SFXList sfx)
	{
		SFXSource.PlayOneShot(SFXClips[(int)sfx]);
	}

	public void PlaySFX(SFXList sfx)
	{
		SFXSource.Stop();
		SFXSource.loop = true;
		SFXSource.clip = SFXClips[(int)sfx];
		SFXSource.Play();
	}

	public void StopSFX()
	{
		SFXSource.Stop();
		if(SFXSource.loop) SFXSource.loop = false;
	}

	public void PlayVoice(VoiceList voice)
	{
		SFXSource.PlayOneShot(VoiceClips[(int)voice]);
	}

	public void SetAudioVolume(int type,float value)
	{
		SFXSource.volume = value;
	}
}
