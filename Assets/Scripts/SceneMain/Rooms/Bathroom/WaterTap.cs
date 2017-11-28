using System.Collections;
using UnityEngine;

public class WaterTap : ActionableFurniture {
	#region attributes
	[Header("WaterTap Attribute")]
	public GameObject waterFlowObject;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public override void PointerClick ()
	{
		if(waterFlowObject.activeSelf) waterFlowObject.SetActive(false);
		else waterFlowObject.SetActive(true);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
