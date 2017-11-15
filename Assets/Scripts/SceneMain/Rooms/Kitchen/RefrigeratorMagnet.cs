using System.Collections;
using UnityEngine;

public class RefrigeratorMagnet : ActionableFurniture {
	public Animator thisAnim;

	public override void PointerClick ()
	{
		thisAnim.SetBool(AnimatorParameters.Triggers.ANIMATE,true);
	}
}
