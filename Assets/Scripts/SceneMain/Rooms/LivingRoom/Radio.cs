using System.Collections;
using UnityEngine;

public class Radio : ActionableFurniture {
	[Header("Radio Attributes")]
	public Animator toneAnimation;
	public SpriteRenderer[] childSprites;

	bool radioOn = false;

	public override void PointerClick()
	{
		if(!radioOn){ 
			radioOn = true;
			SoundManager.Instance.PlayBGM(BGMList.BGMRadio1);
			toneAnimation.SetBool(AnimatorParameters.Bools.RADIO_ON,radioOn);
		}else if(radioOn && SoundManager.Instance.BGMSource.clip == SoundManager.Instance.BGMClips[(int)BGMList.BGMRadio1]){
			SoundManager.Instance.PlayBGM(BGMList.BGMRadio2);
		}else if(radioOn && SoundManager.Instance.BGMSource.clip == SoundManager.Instance.BGMClips[(int)BGMList.BGMRadio2]){
			radioOn = false;
			SoundManager.Instance.PlayBGM(BGMList.BGMMain);
			toneAnimation.SetBool(AnimatorParameters.Bools.RADIO_ON,radioOn);
		}
	}
}