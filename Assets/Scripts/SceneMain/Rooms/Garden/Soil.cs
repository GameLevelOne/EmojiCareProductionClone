using System.Collections;
using UnityEngine;

public class Soil : MonoBehaviour {
	public Transform[] SoilObjects;
	public bool[] flagHasPlant = new bool[]{false,false,false};
	#region attributes
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void RegisterPlantEvent(Plant plant)
	{
		plant.OnPlantDestroyed += OnPlantDestroyed;
	}

	void OnPlantDestroyed (int soilIndex)
	{
		flagHasPlant[soilIndex] = false;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
