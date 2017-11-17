using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : BaseFurniture {
	#region attributes
	public Transform furnitureTransform;
	public List<GameObject> FoodsOnPlate = new List<GameObject>();
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void AddFood(GameObject foodObject)
	{
		foodObject.transform.SetParent(transform);
		Food food = foodObject.GetComponent<Food>();

		food.onPlate = true;
		
		FoodsOnPlate.Add(foodObject);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
