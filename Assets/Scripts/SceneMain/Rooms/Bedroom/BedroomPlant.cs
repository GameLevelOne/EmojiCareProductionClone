using System.Collections;
using UnityEngine;

public class BedroomPlant : ActionableFurniture {
	#region attributes
	[Header("BedroomPlant Attributes")]
	public Animator thisAnim;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public override void PointerClick ()
	{
		thisAnim.SetTrigger(AnimatorParameters.Triggers.ANIMATE);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
