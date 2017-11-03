﻿using System.Collections;
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

	public void BeginDrag()
	{
		if(ingredientObjects.Count > 0){
			PlayerData.Instance.PlayerEmoji.emojiExpressions.SetExpression(EmojiExpressionState.CURIOUS,2f);
		}

		bowl.Init(ingredientObjects.ToArray());
		foreach(GameObject g in ingredientObjects) Destroy(g);
		refrigerator.CloseRefrigerator();
		ingredientObjects = new List<GameObject>();
	}
}