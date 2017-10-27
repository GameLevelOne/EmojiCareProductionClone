using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBowl : MonoBehaviour {
	public const int MAX_INGREDIENT = 10;
	public Refrigerator refrigerator;
	public Bowl bowl;
	[Header("Content")]
	public List<GameObject> ingredientObjects = new List<GameObject>();

	public void AddObject(GameObject ingredientObject)
	{
		ingredientObjects.Add(ingredientObject);
	}

	public void RemoveObject(GameObject ingrediendObject)
	{
		ingredientObjects.Remove(ingrediendObject);
	}

	public void ClearObject()
	{
		ingredientObjects = new List<GameObject>();
	}

	public void BeginDrag()
	{
		bowl.Init(ingredientObjects.ToArray());
		foreach(GameObject g in ingredientObjects) Destroy(g);
		refrigerator.CloseRefrigerator();
		ClearObject();
		//Destroy(this.gameObject);
	}
}