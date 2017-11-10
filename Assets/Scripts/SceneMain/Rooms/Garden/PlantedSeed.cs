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
	public int growDuration;
	public int soilIndex;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init(GameObject plantObject, IngredientType type, Transform parent, int soilIndex, int growthDuration, UIPlantProgress UIProgress,Sprite spriteIcon)
	{
		this.plantObject = plantObject;
		this.type = type;
		this.parent = parent;
		transform.parent = parent;
		this.soilIndex = soilIndex;
		this.growDuration = growthDuration;
		this.UIProgress = UIProgress;
		this.spriteIcon = spriteIcon;

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
		DateTime harvestTime = DateTime.Now.Add(TimeSpan.FromSeconds((double)growDuration));
		PlayerPrefs.SetString(
			soilIndex == 0 ? PlayerPrefKeys.Game.SEED_HARVEST_TIME0 : soilIndex == 1 ? PlayerPrefKeys.Game.SEED_HARVEST_TIME1 : PlayerPrefKeys.Game.SEED_HARVEST_TIME2,
			harvestTime.ToString()
		);

		UIProgress.Init(spriteIcon,harvestTime);


	}

	void FinishedGrowing()
	{
		Vector3 pos = new Vector3(transform.position.x,transform.position.y,-1f);
		GameObject tempPlantObj = (GameObject) Instantiate(plantObject,pos,Quaternion.identity);
		tempPlantObj.GetComponent<Plant>().Init(parent,soilIndex);
		parent.parent.GetComponent<Soil>().RegisterPlantEvent(tempPlantObj.GetComponent<Plant>());
		Destroy(this.gameObject);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
