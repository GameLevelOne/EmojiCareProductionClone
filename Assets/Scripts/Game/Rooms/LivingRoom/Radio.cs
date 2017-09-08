using System.Collections;
using UnityEngine;

public class Radio : MovableFurniture {
	public Animator toneAnimation;

	bool radioOn = false;

	public void PointerDown()
	{
		if(!radioOn) radioOn = true;
		else radioOn = false;
		toneAnimation.SetBool(AnimatorParameters.Bools.RADIO_ON,radioOn);
	}

}
