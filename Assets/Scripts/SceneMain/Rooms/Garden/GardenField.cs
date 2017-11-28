using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GardenField : MonoBehaviour {
	public delegate void TimerTick(TimeSpan duration);
	public event TimerTick OnTimerTick;
	#region attributes
	[Header("GardenFiemd Attributes")]
	public Soil soil;
	public List<Transform> fieldLocations = new List<Transform>();
	public SpriteRenderer[] wetSoil;
	public FieldPost post;

	[Header("Custom Attributes")]
	public int fieldIndex;
	public bool hasPlant;
	public bool hasWatered = false;

	[Header("value in minutes")]
	public int harvestTimeCut = 5;
	public int waterCooldownTime = 5;

	[Header("Do Not modify")]
	public GameObject[] currentPlants = new GameObject[3]{null,null,null};

	DateTime plantHarvestTime;
	TimeSpan plantGrowDuration;

	string prefKeySeedType;
	string prefKeyHarvestTime;
	string prefKeyWaterCooldownTime;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init()
	{
		prefKeySeedType = PlayerPrefKeys.Game.Garden.SEED_TYPE+fieldIndex.ToString();
		prefKeyHarvestTime = PlayerPrefKeys.Game.Garden.SEED_HARVEST_TIME+fieldIndex.ToString();
		prefKeyWaterCooldownTime = PlayerPrefKeys.Game.Garden.SEED_WATER_COOLDOWN+fieldIndex.ToString();

		foreach(SpriteRenderer s in wetSoil) s.enabled = false;
		post.Hide();

		if(PlayerPrefs.HasKey(prefKeySeedType))
		{
			//has plant
			IngredientType plantType = (IngredientType) PlayerPrefs.GetInt(prefKeySeedType);
			LoadPlant(plantType);

			//check time, compare with plant stage
			plantHarvestTime = DateTime.Parse(PlayerPrefs.GetString(prefKeyHarvestTime.ToString()));
			if(DateTime.Now.CompareTo(plantHarvestTime) >= 0){
				foreach(GameObject g in currentPlants) g.GetComponent<Plant>().UpdatePlantStage(0);
			}else{
				StartCoroutine(StartPlantGrowing());

				DateTime waterAvailableTime = DateTime.Parse(PlayerPrefs.GetString(prefKeyWaterCooldownTime));
				if(DateTime.Now.CompareTo(waterAvailableTime) < 0){
					foreach(SpriteRenderer s in wetSoil) s.enabled = true;
					StartCoroutine(StartWaterCooldown());
					hasWatered = true;
				}else{
					foreach(SpriteRenderer s in wetSoil) s.enabled = false;
					hasWatered = false;
				}
			}
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void LoadPlant(IngredientType type)
	{
		if(!hasPlant){
			GameObject plantToInstantiate = soil.GetPlant(type);
			for(int i = 0;i<fieldLocations.Count;i++){
				GameObject tempPlant = Instantiate(plantToInstantiate,fieldLocations[i]) as GameObject;
				tempPlant.transform.localPosition = Vector3.zero;
				currentPlants[i] = tempPlant;

				Plant plant = tempPlant.GetComponent<Plant>();
				plant.Init(i);
				plant.OnPlantDestroyed += OnPlantDestroyed;

			}
			hasPlant = true;
			post.Show();
			StartCoroutine(StartPlantGrowing());
		}
	}

	public void PlantSeed(IngredientType type)
	{
		if(!hasPlant){
			GameObject plantToInstantiate = soil.GetPlant(type);
			for(int i = 0;i<fieldLocations.Count;i++){
				GameObject tempPlant = Instantiate(plantToInstantiate,fieldLocations[i]) as GameObject;
				tempPlant.transform.localPosition = Vector3.zero;
				currentPlants[i] = tempPlant;

				Plant plant = tempPlant.GetComponent<Plant>();
				plant.Init(i);
				plant.OnPlantDestroyed += OnPlantDestroyed;
			}

			plantHarvestTime = DateTime.Now.Add(TimeSpan.FromMinutes(currentPlants[0].GetComponent<Plant>().plantSO.GrowTime));
			PlayerPrefs.SetInt(prefKeySeedType,(int)type);
			PlayerPrefs.SetString(prefKeyHarvestTime,plantHarvestTime.ToString());
			hasPlant = true;
			post.Show();
			StartCoroutine(StartPlantGrowing());
		}
	}

	public void Watered()
	{
		if(hasPlant && !hasWatered){
			hasWatered = true;
			DateTime nextWaterTime = DateTime.Now.Add(TimeSpan.FromMinutes(waterCooldownTime));
			PlayerPrefs.SetString(prefKeyWaterCooldownTime,nextWaterTime.ToString());

			DateTime newPlantHarvestTime = plantHarvestTime.Subtract(TimeSpan.FromMinutes(harvestTimeCut));
			PlayerPrefs.SetString(prefKeyHarvestTime,newPlantHarvestTime.ToString());
			print("New harvest time: "+newPlantHarvestTime);

			foreach(SpriteRenderer s in wetSoil) s.enabled = true;
			StartCoroutine(StartWaterCooldown());
		}
	}

	void OnPlantDestroyed(int locationIndex)
	{
		currentPlants[locationIndex].GetComponent<Plant>().OnPlantDestroyed -= OnPlantDestroyed;
		wetSoil[locationIndex].enabled = false;

		int plantCount = 0;
		foreach(GameObject g in currentPlants){
			if(g != null) plantCount++;
		}

		//not 0, because count first then destroy. 3 2 1
		if(plantCount == 1){
			StopAllCoroutines();
			hasPlant = false;
			hasWatered = false;
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	#region coroutine
	IEnumerator StartPlantGrowing()
	{
		bool flag = true;
		while(flag){
			plantHarvestTime = DateTime.Parse(PlayerPrefs.GetString(prefKeyHarvestTime));
			if(DateTime.Now.CompareTo(plantHarvestTime) >= 0){
				//ready harvest
				foreach(GameObject g in currentPlants) g.GetComponent<Plant>().UpdatePlantStage(0);
				flag = false;
				post.Hide();
				break;
			}else{
				plantGrowDuration = plantHarvestTime.Subtract(DateTime.Now);
//				print("Field "+fieldIndex+" harvest in "+plantGrowDuration.Minutes+":"+plantGrowDuration.Seconds);
				if(OnTimerTick != null) OnTimerTick(plantGrowDuration);
				foreach(GameObject g in currentPlants) g.GetComponent<Plant>().UpdatePlantStage((int)plantGrowDuration.TotalSeconds);
				yield return new WaitForSeconds(1f);
			}
		}
		//stage harvest. dll
	}

	IEnumerator StartWaterCooldown()
	{
		bool flag = true;
		while(flag){
			DateTime waterAvailableTime = DateTime.Parse(PlayerPrefs.GetString(PlayerPrefKeys.Game.Garden.SEED_WATER_COOLDOWN+fieldIndex.ToString()));
			if(DateTime.Now.CompareTo(waterAvailableTime) >= 0){
				foreach(SpriteRenderer s in wetSoil) s.enabled = false;
				StopAllCoroutines();
				flag = false;
			}else{
				yield return new WaitForSeconds(1f);
			}
		}
	}
	#endregion
}