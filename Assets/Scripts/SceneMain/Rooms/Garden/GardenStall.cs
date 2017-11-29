using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GardenStall : BaseFurniture {
	public delegate void StallItemTick(TimeSpan duration);
	public event StallItemTick OnStallItemTick;
	public delegate void StallSeedTick (TimeSpan duration);
	public event StallSeedTick OnStallSeedTick;
	#region attributes
	[Header("Stall Attributes")]
	public List<GameObject> AvailableItems = new List<GameObject>();
	public List<GameObject> AvailableSeeds = new List<GameObject>();

	public Transform[] stallItemParent;
	public Transform[] stallSeedParent;

	public bool hasInit = false;

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

	void OnEnable(){
		if(AdmobManager.Instance) AdmobManager.Instance.OnFinishWatchVideoAds += OnFinishWatchVideoAds;
	}

	void OnDisable(){
		if(AdmobManager.Instance) AdmobManager.Instance.OnFinishWatchVideoAds -= OnFinishWatchVideoAds;
	}

	public void Init()
	{
		if(!hasInit){
			hasInit = true;

			if(!PlayerPrefs.HasKey(PlayerPrefKeys.Game.Garden.ITEM_RESTOCK_TIME)){
				RestockItems();
				RestockSeeds();
			
				itemRestockDuration = TimeSpan.FromMinutes(itemRestockTime);
				seedRestockDuration = TimeSpan.FromMinutes(seedRestockTime);
				DateTime itemNextRestockTime = DateTime.Now.Add(itemRestockDuration);
				DateTime seedNextRestockTime = DateTime.Now.Add(seedRestockDuration);
				PlayerPrefs.SetString(PlayerPrefKeys.Game.Garden.ITEM_RESTOCK_TIME,itemNextRestockTime.ToString());
				PlayerPrefs.SetString(PlayerPrefKeys.Game.Garden.SEED_RESTOCK_TIME,seedNextRestockTime.ToString());
			}else{
				ValidateItemRestocking();
				ValidateSeedRestocking();
			}
			StartCoroutine(_RunItemRestockTime);
			StartCoroutine(_RunSeedRestockTime);
		}
	}

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
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void LoadItems()
	{
		for(int i = 0;i<MAX_ITEM;i++){
			int typeIndex = GetCurrentItemData(i);

			if(typeIndex == -1) currentItems.Add(null);
			else InstantiateItem(i);
		}
	}
	public void LoadSeeds()
	{
		for(int i = 0;i<MAX_ITEM;i++){
			int typeIndex = GetCurrentSeedData(i);

			if(typeIndex == -1) currentSeeds.Add(null);
			else InstantiateSeed(i);
		}
	}


	public void RestockItems()
	{
		foreach(GameObject g in currentItems){ if(g != null) Destroy(g); }
		currentItems.Clear();

		for(int i = 0;i<MAX_ITEM;i++) InstantiateItem(i);
		DateTime newRestockTime = DateTime.Now.Add(TimeSpan.FromMinutes(itemRestockTime));
		PlayerPrefs.SetString(PlayerPrefKeys.Game.Garden.ITEM_RESTOCK_TIME,newRestockTime.ToString());
	}
	public void RestockSeeds()
	{
		foreach(GameObject g in currentSeeds){ if(g != null) Destroy(g); }
		currentSeeds.Clear();

		for(int i = 0;i<MAX_ITEM;i++) InstantiateSeed(i);
		DateTime newRestockTime = DateTime.Now.Add(TimeSpan.FromMinutes(seedRestockTime));
		PlayerPrefs.SetString(PlayerPrefKeys.Game.Garden.SEED_RESTOCK_TIME,newRestockTime.ToString());
	}

	//core mechanic
	void InstantiateItem(int itemIndex)
	{
		int rnd = UnityEngine.Random.Range(0,AvailableItems.Count);
		GameObject tempItem = Instantiate(AvailableItems[rnd],stallItemParent[itemIndex]);
		tempItem.transform.localPosition = new Vector3(0f,0f,-1f);

		StallItem item = tempItem.GetComponent<StallItem>();
		item.Init(itemIndex);
		item.OnItemPicked += OnItemPicked;

		currentItems.Add(tempItem);
		SetCurrentItemData(itemIndex,(int)item.type);
	}
	void InstantiateSeed(int seedIndex)
	{
		int rnd = UnityEngine.Random.Range(0,AvailableSeeds.Count);
		GameObject tempSeed = Instantiate(AvailableSeeds[rnd],stallSeedParent[seedIndex]);
		tempSeed.transform.localPosition = new Vector3(0f,0f,-1f);

		Seed seed = tempSeed.GetComponent<Seed>();
		seed.Init(seedIndex);
		seed.OnSeedPlanted += OnSeedPlanted;

		currentSeeds.Add(tempSeed);
		SetCurrentSeedData(seedIndex,(int)seed.type);
	}

	//delegate events
	void OnItemPicked(StallItem currentItem)
	{
		currentItem.OnItemPicked -= OnItemPicked;

		int index = currentItem.itemIndex;
		SetCurrentItemData(currentItem.itemIndex,-1);
		Destroy(currentItem.gameObject);
		
		currentItems[index] = null;
	}
	void OnSeedPlanted(Seed currentSeed)
	{
		currentSeed.OnSeedPlanted -= OnSeedPlanted;

		int index = currentSeed.seedIndex;
		SetCurrentSeedData(currentSeed.seedIndex,-1);
		Destroy(currentSeed.gameObject);

		currentSeeds[index] = null;
	}

	void OnFinishWatchVideoAds(AdEvents eventName){
		if(eventName == AdEvents.RestockStall){
			RestockItems ();
		} else if(eventName == AdEvents.RestockSeeds){
			RestockSeeds ();
		}
	}

	//save load seed
	void SetCurrentItemData(int itemIndex,int typeIndex)
	{
		PlayerPrefs.SetInt(PlayerPrefKeys.Game.Garden.ITEM_CURRENT+itemIndex.ToString(),typeIndex);
	}
	void SetCurrentSeedData(int seedIndex, int typeIndex)
	{
		PlayerPrefs.SetInt(PlayerPrefKeys.Game.Garden.SEED_CURRENT+seedIndex.ToString(),typeIndex);
	}


	int GetCurrentItemData(int itemIndex)
	{
		return PlayerPrefs.GetInt(PlayerPrefKeys.Game.Garden.ITEM_CURRENT+itemIndex.ToString());
	}
	int GetCurrentSeedData(int seedIndex)
	{
		return PlayerPrefs.GetInt(PlayerPrefKeys.Game.Garden.SEED_CURRENT+seedIndex.ToString());
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

			}else{
				itemRestockDuration = restockTime.Subtract(DateTime.Now);
				if (OnStallItemTick != null)
					OnStallItemTick (itemRestockDuration);
//				print("Item restock in "+itemRestockDuration.TotalMinutes+":"+itemRestockDuration.Seconds);
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

			}else{
				seedRestockDuration = restockTime.Subtract(DateTime.Now);
				if (OnStallSeedTick != null)
					OnStallSeedTick (seedRestockDuration);
//				print("Seed restock in "+seedRestockDuration.TotalMinutes+":"+seedRestockDuration.Seconds);
			}
			yield return new WaitForSeconds(1);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}
