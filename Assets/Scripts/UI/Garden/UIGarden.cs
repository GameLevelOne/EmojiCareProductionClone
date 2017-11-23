using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGarden : MonoBehaviour {
	public GardenStall gardenStall;
	public UICoin coinBox;
	public GameObject boxTimerSeed;
	public GameObject boxTimerIngredientStall;
	public Text timerSeed;
	public Text timerIngredientStall;

	void OnEnable(){
		gardenStall.OnStallItemTick += ShowStallItemTimer;
		gardenStall.OnStallSeedTick += ShowSeedTimer;
	}

	void OnDisable(){
		gardenStall.OnStallItemTick -= ShowStallItemTimer;
		gardenStall.OnStallSeedTick -= ShowSeedTimer;
	}

	public void InitGardenUI(){
		RegisterEvents ();
		timerSeed.gameObject.SetActive (true);
		timerIngredientStall.gameObject.SetActive (true);
	}

	void RegisterEvents(){
		Seed.OnDragSeed += HandleDragStallItem;
		StallItem.OnDragStallItem += HandleDragStallItem;
	}

	public void UnregisterGardenEvents(){
		Seed.OnDragSeed -= HandleDragStallItem;
		StallItem.OnDragStallItem -= HandleDragStallItem;
		boxTimerSeed.SetActive (false);
		boxTimerIngredientStall.SetActive (false);
	}

	void HandleDragStallItem (int price)
	{
		coinBox.gameObject.SetActive (true);
		coinBox.ShowUI (price);
	}

	void ShowStallItemTimer(TimeSpan ingredientTimer){
		boxTimerIngredientStall.SetActive (true);
		timerIngredientStall.text = ingredientTimer.Minutes.ToString () + ":" + ingredientTimer.Seconds.ToString ();
	}

	void ShowSeedTimer(TimeSpan seedTimer){
		boxTimerSeed.SetActive (true);
		timerSeed.text = seedTimer.Minutes.ToString () + ":" + seedTimer.Seconds.ToString ();
	}
	
}
