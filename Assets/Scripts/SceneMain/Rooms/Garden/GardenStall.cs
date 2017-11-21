using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GardenStall : BaseFurniture {
	#region attributes
	[Header("Stall Attributes")]
	public List<GameObject> AvailableItems = new List<GameObject>();
	public List<GameObject> AvailableSeeds = new List<GameObject>();

	public Transform[] stallItemParent;
	public Transform[] stallSeedParent;

	public Soil soil;

	public bool hasInit = false;
	[Header("Custom Variables")]
	public Vector3 itemScale;

	[Header("Value in minutes")]
	public int itemRestockTime = 90;
	public int seedRestockTime = 45;

	[Header("Do Not Modify")]
	public List<GameObject> currentItems;
	public List<GameObject> currentSeeds;

	const int MAX_ITEM = 4;
	const int MAX_SEED = 4;

	TimeSpan itemRestockDuration;
	TimeSpan seedRestockDuration;

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init()
	{
		if(!hasInit){
			hasInit = true;

			if(!PlayerPrefs.HasKey(PlayerPrefKeys.Game.Garden.ITEM_CURRENT)){
				RestockItems();
				RestockSeeds();
			}else{
				ValidateItemRestocking();
				ValidateSeedRestocking();
				StartCoroutine(_RunItemRestockTime);
				StartCoroutine(_RunSeedRestockTime);
			}
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void ValidateItemRestocking()
	{
		DateTime restockTime = DateTime.Parse(PlayerPrefs.GetString(PlayerPrefKeys.Game.Garden.ITEM_RESTOCK_TIME));
		if(DateTime.Now.CompareTo(restockTime) >= 0){
			RestockItems();

			DateTime newRestockTime = restockTime.Add(TimeSpan.FromMinutes(itemRestockTime));
			while(newRestockTime.CompareTo(DateTime.Now) <= 0){
				newRestockTime = newRestockTime.Add(TimeSpan.FromMinutes(itemRestockTime));
			}
			PlayerPrefs.SetString(PlayerPrefKeys.Game.Garden.ITEM_RESTOCK_TIME,newRestockTime.ToString());
			itemRestockDuration = DateTime.Now.Subtract(newRestockTime);
		}else{
			LoadItems();
		}
	}

	void ValidateSeedRestocking()
	{
		DateTime restockTime = DateTime.Parse(PlayerPrefs.GetString(PlayerPrefKeys.Game.Garden.SEED_RESTOCK_TIME));
		if(DateTime.Now.CompareTo(restockTime) >= 0){
			RestockSeeds();

			DateTime newRestockTime = restockTime.Add(TimeSpan.FromMinutes(itemRestockTime));
			while(newRestockTime.CompareTo(DateTime.Now) <= 0){
				newRestockTime = newRestockTime.Add(TimeSpan.FromMinutes(itemRestockTime));
			}
			PlayerPrefs.SetString(PlayerPrefKeys.Game.Garden.SEED_RESTOCK_TIME,newRestockTime.ToString());
			seedRestockDuration = DateTime.Now.Subtract(newRestockTime);
		}else{
			LoadSeeds();
		}
	}

	public void LoadItems()
	{
		for(int i = 0;i<MAX_ITEM;i++){
			GameObject tempItem = Instantiate(AvailableItems[LoadCurrentItemData(i)],stallItemParent[i]);
			tempItem.transform.localPosition = new Vector3(0f,0f,-1f);
			tempItem.transform.localScale = itemScale;
			currentItems.Add(tempItem);
			SaveCurrentItemData(i,tempItem.GetComponent<StallItem>().type);
		}
	}
	public void LoadSeeds()
	{
		for(int i = 0;i<MAX_ITEM;i++){
			GameObject tempSeed = Instantiate(AvailableItems[LoadCurrentSeedData(i)],stallSeedParent[i]);
			tempSeed.transform.localPosition = new Vector3(0f,0f,-1f);
			tempSeed.transform.localScale = itemScale;
			currentSeeds.Add(tempSeed);
			SaveCurrentSeedData(i,tempSeed.GetComponent<Seed>().type);
		}
	}

	public void RestockItems()
	{
		foreach(GameObject g in currentItems) Destroy(g);
		currentItems.Clear();

		for(int i = 0;i<MAX_ITEM;i++){
			int rnd = UnityEngine.Random.Range(0,AvailableItems.Count);
			GameObject tempItem = Instantiate(AvailableItems[rnd],stallItemParent[i]);
			tempItem.transform.localPosition = new Vector3(0f,0f,-1f);
			tempItem.transform.localScale = itemScale;
			currentItems.Add(tempItem);
			SaveCurrentItemData(i,tempItem.GetComponent<StallItem>().type);
		}
	}
	public void RestockSeeds()
	{
		foreach(GameObject g in currentSeeds) Destroy(g);
		currentSeeds.Clear();

		for(int i = 0;i<MAX_ITEM;i++){
			int rnd = UnityEngine.Random.Range(0,AvailableSeeds.Count);
			GameObject tempSeed = Instantiate(AvailableItems[rnd],stallSeedParent[i]);
			tempSeed.transform.localPosition = new Vector3(0f,0f,-1f);
			tempSeed.transform.localScale = itemScale;
			currentSeeds.Add(tempSeed);
			SaveCurrentSeedData(i,tempSeed.GetComponent<Seed>().type);
		}
	}


	//save load seed
	void SaveCurrentItemData(int index, IngredientType type)
	{
		PlayerPrefs.SetInt(PlayerPrefKeys.Game.Garden.ITEM_CURRENT+index.ToString(),(int)type);
	}
	void SaveCurrentSeedData(int index, IngredientType type)
	{
		PlayerPrefs.SetInt(PlayerPrefKeys.Game.Garden.SEED_CURRENT+index.ToString(),(int)type);
	}

	int LoadCurrentItemData(int index)
	{
		return PlayerPrefs.GetInt(PlayerPrefKeys.Game.Garden.ITEM_CURRENT);
	}
	int LoadCurrentSeedData(int index)
	{
		return PlayerPrefs.GetInt(PlayerPrefKeys.Game.Garden.SEED_CURRENT);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	#region coroutines
	const string _RunItemRestockTime = "RunItemRestockTime";
	IEnumerator RunItemRestockTime()
	{
		while(true){
			DateTime restockTime = DateTime.Parse(PlayerPrefs.GetString(PlayerPrefKeys.Game.Garden.ITEM_RESTOCK_TIME));
			if(DateTime.Now.CompareTo(restockTime) >= 0){
				RestockItems();
				DateTime newRestockTime = DateTime.Now.Add(TimeSpan.FromMinutes(itemRestockTime));
				PlayerPrefs.SetString(PlayerPrefKeys.Game.Garden.ITEM_RESTOCK_TIME,newRestockTime.ToString());
			}else{
				itemRestockDuration = restockTime.Subtract(DateTime.Now);
			}

			yield return new WaitForSeconds(1);
		}
	}

	const string _RunSeedRestockTime = "RunSeedRestockTime";
	IEnumerator RunSeedRestockTime()
	{
		while(true){
			DateTime restockTime = DateTime.Parse(PlayerPrefs.GetString(PlayerPrefKeys.Game.Garden.SEED_RESTOCK_TIME));
			if(DateTime.Now.CompareTo(restockTime) >= 0){
				RestockSeeds();
				DateTime newRestockTime = DateTime.Now.Add(TimeSpan.FromMinutes(seedRestockTime));
				PlayerPrefs.SetString(PlayerPrefKeys.Game.Garden.SEED_RESTOCK_TIME,newRestockTime.ToString());
			}else{
				seedRestockDuration = restockTime.Subtract(DateTime.Now);
			}
			yield return new WaitForSeconds(1);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}
