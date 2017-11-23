using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenTimer : MonoBehaviour {
	public Text timerSeed;
	public Text timerIngredientStall;
	public Text[] timerPlants;

	//timer for seed and ingredients
	void ShowStallTimers(TimeSpan seedTimer,TimeSpan ingredientTimer){
		timerSeed.text = seedTimer.Minutes.ToString () + ":" + seedTimer.Seconds.ToString ();
		timerIngredientStall.text = ingredientTimer.Minutes.ToString () + ":" + ingredientTimer.Seconds.ToString ();
	}

	//timer for plants
	void ShowPlantTimers(TimeSpan plant1,TimeSpan plant2,TimeSpan plant3){
		timerPlants [0].text = plant1.Minutes.ToString () + ":" + plant1.Seconds.ToString ();
		timerPlants [1].text = plant2.Minutes.ToString () + ":" + plant2.Seconds.ToString ();
		timerPlants [2].text = plant3.Minutes.ToString () + ":" + plant3.Seconds.ToString ();
	}
	
}
