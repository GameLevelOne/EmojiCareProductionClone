using System.Collections;
using UnityEngine;

public class MagnifyingGlass : TriggerableFurniture {
	#region attributes
	[Header("MagnifyingGlass Attributes")]
	public FloatingStatsManager popupStats;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void ShowStatsPopup()
	{
		Debug.Log ("scan");
		popupStats.ShowStatsFromMagnifyingGlass();
	}
	public void HideStatsPopup()
	{
		Debug.Log ("scan end");
		popupStats.HideStatsFromMagnifyingGlass ();
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
