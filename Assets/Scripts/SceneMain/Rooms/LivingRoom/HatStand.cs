using System.Collections;
using UnityEngine;

public class HatStand : ActionableFurniture {
	#region attributes
	[Header("HatStand Attributes")]
	public GameObject[] hatObjects;
	public UIChangeHat uiChangeHat;

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public override void PointerClick ()
	{
		//open hat UI menu
		uiChangeHat.ShowHatUI();
	}


	void GetHatData(){}
	void SetHatData(){}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void LoadHat()
	{
		
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
