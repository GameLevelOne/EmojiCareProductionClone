using System.Collections;
using UnityEngine;
using System;

public class PlantedSeed : MonoBehaviour {
	#region attributes
	public UIPlantProgress UIProgress;
	public GameObject plantObject;
	public IngredientType type;
	public Transform parent;
	public Sprite spriteIcon;
	public Collider2D thisCollider;
	public int growDuration;
	public int soilIndex;
	string[] prefKeyToDelete;
	bool hasWatered = false;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init(GameObject plantObject, IngredientType type, Transform parent, int soilIndex, int growthDuration, UIPlantProgress UIProgress,Sprite spriteIcon, string[] prefKeyToDelete, bool hasWatered)
	{
		this.plantObject = plantObject;
		this.type = type;
		this.parent = parent;
		transform.parent = parent;
		this.soilIndex = soilIndex;
		this.growDuration = growthDuration;
		this.UIProgress = UIProgress;
		this.spriteIcon = spriteIcon;
		this.prefKeyToDelete = prefKeyToDelete;
		this.hasWatered = hasWatered;

		UIProgress.OnFinishGrowing += FinishedGrowing;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void Grow()
	{
		thisCollider.enabled = false;
		DateTime harvestTime = DateTime.Now.Add(TimeSpan.FromSeconds((double)growDuration));
		print("harvest time = "+harvestTime);
		PlayerPrefs.SetString(
			GetPrefKeyHarvestTime(soilIndex),
			harvestTime.ToString()
		);

		UIProgress.Init(spriteIcon,harvestTime);
		
	}

	string GetPrefKeyHarvestTime(int soilIndex)
	{
		if(soilIndex == 0) return PlayerPrefKeys.Game.SEED_HARVEST_TIME0;
		else if(soilIndex == 1) return PlayerPrefKeys.Game.SEED_HARVEST_TIME1;
		else return PlayerPrefKeys.Game.SEED_HARVEST_TIME2;
	}

	void FinishedGrowing()
	{
		Vector3 pos = new Vector3(transform.position.x,transform.position.y,-1f);
		GameObject tempPlantObj = (GameObject) Instantiate(plantObject,pos,Quaternion.identity);
//		tempPlantObj.GetComponent<Plant>().Init(parent,soilIndex,prefKeyToDelete);
//		parent.parent.GetComponent<Soil>().RegisterPlantEvent(tempPlantObj.GetComponent<Plant>());
		Destroy(this.gameObject);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
