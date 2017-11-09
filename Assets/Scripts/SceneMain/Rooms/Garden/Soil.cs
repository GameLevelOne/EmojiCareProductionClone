using System.Collections;
using UnityEngine;
using System;

public class Soil : MonoBehaviour {
	[Header("Reference")]
	public GameObject seedObject;
	public UIPlantProgress[] UIProgress;
	public GameObject[] plantObjects;
	public Sprite[] spriteIcons;
	public Transform[] SoilObjects;
	public bool[] flagHasPlant = new bool[]{false,false,false};

	public string[] prefKeyHasSeed = new string[3]{
		PlayerPrefKeys.Game.HAS_SEED0,
		PlayerPrefKeys.Game.HAS_SEED1,
		PlayerPrefKeys.Game.HAS_SEED2
	};
	public string[] prefKeySeedType = new string[3]{
		PlayerPrefKeys.Game.SEED_TYPE0,
		PlayerPrefKeys.Game.SEED_TYPE1,
		PlayerPrefKeys.Game.SEED_TYPE2
	};
	public string[] prefKeySeedGrowDuration = new string[3]{
		PlayerPrefKeys.Game.SEED_GROW_DURATION0,
		PlayerPrefKeys.Game.SEED_GROW_DURATION1,
		PlayerPrefKeys.Game.SEED_GROW_DURATION2
	};
	public string[] prefKeyHarvestTime = new string[3]{
		PlayerPrefKeys.Game.SEED_HARVEST_TIME0,
		PlayerPrefKeys.Game.SEED_HARVEST_TIME1,
		PlayerPrefKeys.Game.SEED_HARVEST_TIME2
	};

	#region attributes
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init()
	{
		for(int i = 0;i<SoilObjects.Length;i++){
			if(PlayerPrefs.GetInt(prefKeyHasSeed[i]) == 1){
				DateTime finishtime = DateTime.Parse(PlayerPrefs.GetString(prefKeyHarvestTime[i]));
				if(DateTime.Now.CompareTo(finishtime) >= 0){
					int typeIndex = PlayerPrefs.GetInt(prefKeySeedType[i]);
					GameObject tempPlant = Instantiate(plantObjects[typeIndex],SoilObjects[i].GetChild(0).position,Quaternion.identity);
					tempPlant.transform.SetParent(SoilObjects[i],true);
				}
				AddSeed(
					i,
					(IngredientType) PlayerPrefs.GetInt(prefKeySeedType[i]),
					PlayerPrefs.GetInt(prefKeySeedGrowDuration[i])
				);
			}
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void AddSeed(int index, IngredientType type, int growDuration)
	{
		PlayerPrefs.SetInt(prefKeyHasSeed[index],1);
		PlayerPrefs.SetInt(prefKeySeedType[index],GetSeedTypeIndex(type));
		PlayerPrefs.SetString(prefKeyHarvestTime[index],string.Empty);

		GameObject tempSeedObject = Instantiate(seedObject,SoilObjects[index].GetChild(0).position,Quaternion.identity);
		tempSeedObject.transform.SetParent(SoilObjects[index],true);
		tempSeedObject.GetComponent<PlantedSeed>().Init(plantObjects[index],type,SoilObjects[index],index,growDuration,UIProgress[index],spriteIcons[index]);
	}

	public void HarvestCrop(int index)
	{
		PlayerPrefs.SetInt(prefKeyHasSeed[index],0);
	}

	IngredientType GetIngredientType(int index)
	{
		if(index == 0) return IngredientType.Cabbage;
		else if (index == 1) return IngredientType.Carrot;
		else if (index == 2) return IngredientType.Mushroom;
		else  return IngredientType.Tomato;
	}

	int GetSeedTypeIndex(IngredientType type)
	{
		if(type == IngredientType.Cabbage) return 0;
		else if(type == IngredientType.Carrot) return 1;
		else if(type == IngredientType.Mushroom) return 2;
		else return 3;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules


	public void RegisterPlantEvent(Plant plant)
	{
		plant.OnPlantDestroyed += OnPlantDestroyed;
	}

	void OnPlantDestroyed (int soilIndex)
	{
		PlayerPrefs.SetInt(prefKeyHasSeed[soilIndex],0);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}