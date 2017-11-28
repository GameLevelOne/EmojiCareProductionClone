using System.Collections;
using UnityEngine;

public class Soap : ActionableFurniture {
	[Header("Soap Attributes")]
	public Sponge sponge;
	public Animator thisAnim;

	public override void PointerClick()
	{
		sponge.ApplySoapLiquid();
		thisAnim.SetTrigger(AnimatorParameters.Triggers.ANIMATE);
	}
	
}
