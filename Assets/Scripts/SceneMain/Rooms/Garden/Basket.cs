using System.Collections;
using UnityEngine;

public class Basket : BaseFurniture {
	#region attributes
	[Header("Basket Attribute")]
	public Animator thisAnim;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void Animate()
	{
		thisAnim.SetTrigger(AnimatorParameters.Triggers.ANIMATE);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
