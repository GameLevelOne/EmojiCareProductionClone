using System.Collections;
using UnityEngine;

public class MagnifyingGlass : TriggerableFurniture {
	#region attributes
	[Header("MagnifyingGlass Attributes")]
	public MovableFurniture thisMovable;
	public FloatingStatsManager popupStats;
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Start()
	{
		thisMovable.OnItemReleased += HideStatsPopup;
	}

	void OnDestroy()
	{
		thisMovable.OnItemReleased -= HideStatsPopup;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void ShowStatsPopup()
	{
//		Debug.Log ("scan");
		popupStats.ShowStatsFromMagnifyingGlass();
	}
	public void HideStatsPopup()
	{
//		Debug.Log ("scan end");
		popupStats.HideStatsFromMagnifyingGlass ();
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
