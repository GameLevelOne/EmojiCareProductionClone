using System.Collections;
using UnityEngine;

public class Radio : ActionableFurniture {
	[Header("Radio Attributes")]
	public Animator toneAnimation;
	public SpriteRenderer[] childSprites;

	bool radioOn = false;

	public override void PointerClick()
	{
		if(!radioOn) radioOn = true;
		else radioOn = false;
		toneAnimation.SetBool(AnimatorParameters.Bools.RADIO_ON,radioOn);
	}
}