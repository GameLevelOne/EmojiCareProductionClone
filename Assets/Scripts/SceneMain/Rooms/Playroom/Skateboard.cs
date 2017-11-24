using System.Collections;
using UnityEngine;

public class Skateboard : ActionableFurniture {
	#region attributes
	[Header("Skateboard Attribute")]
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
