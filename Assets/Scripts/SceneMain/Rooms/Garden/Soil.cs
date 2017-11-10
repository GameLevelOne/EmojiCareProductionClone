using System.Collections;
using UnityEngine;
using System;

public class Soil : MonoBehaviour {
	[Header("Reference")]
	public Seed[] seeds;
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

	/// <summary>
	/// 0 = Cabbage, 1 = Carrot, 2 = Mushroom, 3 = Tomato
	/// </summary>
	public string[] prefKeySeedType = new string[3]{
		PlayerPrefKeys.Game.SEED_TYPE0,
		PlayerPrefKeys.Game.SEED_TYPE1,
		PlayerPrefKeys.Game.SEED_TYPE2
	};

	public string[] prefKeyHarvestTime = new string[3]{
		PlayerPrefKeys.Game.SEED_HARVEST_TIME0,
		PlayerPrefKeys.Game.SEED_HARVEST_TIME1,
		PlayerPrefKeys.Game.SEED_HARVEST_TIME2
	};

	bool hasInit = false;
	#region attributes
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init()
	{
		if(!hasInit){
			hasInit = true;

			for(int i = 0;i<SoilObjects.Length;i++){
				if(PlayerPrefs.GetInt(prefKeyHasSeed[i],0) == 1){
					if(PlayerPrefs.HasKey(prefKeyHarvestTime[i])){
						DateTime harvestTime = DateTime.Parse(PlayerPrefs.GetString(prefKeyHarvestTime[i]));
						if(DateTime.Now.CompareTo(harvestTime) >= 0){
							int typeIndex = PlayerPrefs.GetInt(prefKeySeedType[i]);
							GameObject tempPlant = Instantiate(plantObjects[typeIndex],SoilObjects[i].GetChild(0).position,Quaternion.identity);
							tempPlant.transform.SetParent(SoilObjects[i],true);
						}else{
							print("WATER YES");
							int typeIndex = PlayerPrefs.GetInt(prefKeySeedType[i]);
							TimeSpan tempDur = DateTime.Parse(PlayerPrefs.GetString(prefKeyHarvestTime[i])).Subtract(DateTime.Now);
							int growDur = (int)tempDur.TotalSeconds;

							AddSeed(i,(IngredientType)typeIndex,growDur,true);
						}
					}else{
						print("WATER NO");
						int typeIndex = PlayerPrefs.GetInt(prefKeySeedType[i]);
						AddSeed(i,(IngredientType)typeIndex,seeds[typeIndex].growthDuration,false);
					}
				}
			}
		}


		//enable or disble UI Progress
		for(int i = 0;i<UIProgress.Length;i++){
			print(i);
			if(PlayerPrefs.HasKey(prefKeyHarvestTime[i])){
				DateTime harvestTime = DateTime.Parse(PlayerPrefs.GetString(prefKeyHarvestTime[i]));
				if(DateTime.Now.CompareTo(harvestTime) < 0){
					print("SINI SINI");
					UIProgress[i].Show();
				}
			}
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void AddSeed(int index, IngredientType type, int growDuration, bool hasWatered)
	{
		string[] prefKeyToDelete = new string[]{
			prefKeyHasSeed[index],
			prefKeySeedType[index],
			prefKeyHarvestTime[index]
		};

		PlayerPrefs.SetInt(prefKeyHasSeed[index],1);
		PlayerPrefs.SetInt(prefKeySeedType[index],GetSeedTypeIndex(type));
		if(hasWatered) PlayerPrefs.SetString(prefKeyHarvestTime[index],DateTime.Now.Add(TimeSpan.FromSeconds((double)growDuration)).ToString());

		GameObject tempSeedObject = Instantiate(seedObject,SoilObjects[index].GetChild(0).position,Quaternion.identity);
		tempSeedObject.transform.SetParent(SoilObjects[index],true);
		tempSeedObject.GetComponent<PlantedSeed>().Init(plantObjects[index],type,SoilObjects[index],index,growDuration,UIProgress[index],spriteIcons[index],prefKeyToDelete,hasWatered);

		if(hasWatered) tempSeedObject.GetComponent<PlantedSeed>().Grow();
		
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