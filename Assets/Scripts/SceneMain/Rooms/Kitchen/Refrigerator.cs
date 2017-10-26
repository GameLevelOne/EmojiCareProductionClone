using System.Collections;
using UnityEngine;

public class Refrigerator : ActionableFurniture {
	#region attributes
	[Header("Refrigerator Attributes")]
	public GameObject doorClosed;
	public GameObject doorOpened;

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public override void PointerClick ()
	{
		doorOpened.SetActive(true);
		doorClosed.SetActive(false);

		//open refrigerator UI
	}

	public void CloseRefrigerator()
	{
		doorClosed.SetActive(true);
		doorOpened.SetActive(false);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
