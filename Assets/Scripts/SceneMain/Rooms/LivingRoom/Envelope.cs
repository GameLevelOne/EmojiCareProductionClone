using System.Collections;
using UnityEngine;

public class Envelope : ActionableFurniture {
	#region attributes
	[Header("Envelope Attributes")]
	public ScreenProgress UIProgress;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public override void PointerClick ()
	{
		if(!flagEditMode) UIProgress.ShowUI(UIProgress.gameObject);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
