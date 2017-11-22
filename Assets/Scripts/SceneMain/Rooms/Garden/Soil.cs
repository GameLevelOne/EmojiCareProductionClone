using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Soil : MonoBehaviour {
	#region attributes
	[Header("NEW!")]
	public List<GameObject> plants;
	public GardenField[] gardenFields;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void Init()
	{
		foreach(GardenField gf in gardenFields) gf.Init();
	}

	public GameObject GetPlant(IngredientType type)
	{
		if(type == IngredientType.Cabbage) return plants[0];
		else if(type == IngredientType.Carrot) return plants[1];
		else if(type == IngredientType.Mushroom) return plants[2];
		else if (type == IngredientType.Potato) return plants[3];
		else return plants[4];
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}