using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGarden : MonoBehaviour {
	public GardenStall gardenStall;
	public UICoin coinBox;
 
	public void Init(){	
		Seed.OnDragSeed += HandleDragStallItem;
		StallItem.OnDragStallItem += HandleDragStallItem;
		Seed.OnEndDragSeed += OnEndDragSeed;
		StallItem.OnEndDragStallItem += OnEndDragStallItem;
	}

	public void UnregisterGardenEvents(){
		Debug.Log ("disable ui");
		Seed.OnDragSeed -= HandleDragStallItem;
		StallItem.OnDragStallItem -= HandleDragStallItem;
		Seed.OnEndDragSeed -= OnEndDragSeed;
		StallItem.OnEndDragStallItem -= OnEndDragStallItem;
	}

	void OnEndDragStallItem (bool isBought)
	{
		coinBox.CloseUI (isBought);
	}

	void OnEndDragSeed (bool isBought)
	{
		coinBox.CloseUI (isBought);
	}

	void HandleDragStallItem (int price)
	{
		coinBox.gameObject.SetActive (true);
		coinBox.ShowUI (price);
	}
}