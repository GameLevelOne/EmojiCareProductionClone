using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GardenStall : BaseFurniture {
	public delegate void StallItemTick(TimeSpan duration);
	public event StallItemTick OnStallItemTick;
	public delegate void StallSeedTick (TimeSpan duration);
	public event StallSeedTick OnStallSeedTick;

	enum IngredientItems{ Cheese, Chicken, Egg, Fish, Flour, Meat }
	enum IngredientSeeds{ Cabbage, Carrot, Mushroom, Tomato, Potato}

	#region attributes
	[Header("Stall Attributes")]
	public GardenStallTimer gardenStallTimer;
	public UICoin uiCoin;
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

	[Header("Scene guided tutorial only")]
	public GuidedTutorialStork sceneGuidedTutorial;

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization

	void OnEnable(){
		if(AdmobManager.Instance) AdmobManager.Instance.OnFinishWatchVideoAds += OnFinishWatchVideoAds;
		GardenStallTimer.OnRefillStallWithGems += OnRefillStallWithGems;
	}

	void OnDisable(){
		if(AdmobManager.Instance) AdmobManager.Instance.OnFinishWatchVideoAds -= OnFinishWatchVideoAds;
		GardenStallTimer.OnRefillStallWithGems -= OnRefillStallWithGems;
	}

	public void Init()
	{
		if(!hasInit){
			hasInit = true;
			if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == ShortCode.SCENE_GUIDED_TUTORIAL){
				InitOnSceneGuidedTutorial ();
				gardenStallTimer.gameObject.SetActive (false);
			}else{
				gardenStallTimer.Init();

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
	}

	void InitOnSceneGuidedTutorial()
	{
		InstantiateItem (0, 1);
		InstantiateSeed (0, 0);
		InstantiateSeed (1, 1);
		InstantiateSeed (2, 3);

		sceneGuidedTutorial.RegisterSeedAndItemEvents ();
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
//		int rnd = -1;
//		do{
//			rnd = UnityEngine.Random.Range(0,AvailableItems.Count);
//		}while(ValidateRandomizedIngredientItem(rnd) == false);
		int rnd = RandomizeIngredientItem();
		GameObject tempItem = Instantiate(AvailableItems[rnd],stallItemParent[itemIndex]);
		tempItem.transform.localPosition = new Vector3(0f,0f,-1f);

		StallItem item = tempItem.GetComponent<StallItem>();
		item.Init(itemIndex);
		item.OnItemPicked += OnItemPicked;
		item.OnDragStallItem += ShowUICoin;
		item.OnEndDragStallItem += HideUICoin;


		currentItems.Add(tempItem);
		SetCurrentItemData(itemIndex,(int)item.type);
	}

	void InstantiateItem(int itemIndex, int itemIngredientIndex)
	{
//		int rnd = -1;
//		do{
//			rnd = UnityEngine.Random.Range(0,AvailableItems.Count);
//		}while(ValidateRandomizedIngredientItem(rnd) == false);
//		int rnd = RandomizeIngredientItem();
		GameObject tempItem = Instantiate(AvailableItems[itemIngredientIndex],stallItemParent[itemIndex]);
		tempItem.transform.localPosition = new Vector3(0f,0f,-1f);

		StallItem item = tempItem.GetComponent<StallItem>();
		item.Init(itemIndex);
		item.OnItemPicked += OnItemPicked;
		item.OnDragStallItem += ShowUICoin;
		item.OnEndDragStallItem += HideUICoin;


		currentItems.Add(tempItem);
		SetCurrentItemData(itemIndex,(int)item.type);

		if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == ShortCode.SCENE_GUIDED_TUTORIAL){
			sceneGuidedTutorial.item1 = item;
		}
	}

	int RandomizeIngredientItem()
	{
		List<int> tempRandomItem = new List<int>();

		for(int i = 0;i<AvailableItems.Count;i++){
			IngredientItems temp = (IngredientItems)i;

			int itemData = -1;

			switch(temp){
			case IngredientItems.Cheese: itemData = PlayerData.Instance.IngredientCheese; break;
			case IngredientItems.Chicken: itemData = PlayerData.Instance.IngredientChicken; break;
			case IngredientItems.Egg: itemData = PlayerData.Instance.IngredientEgg; break;
			case IngredientItems.Fish: itemData = PlayerData.Instance.IngredientFish; break;
			case IngredientItems.Flour: itemData = PlayerData.Instance.IngredientFlour; break;
			case IngredientItems.Meat: itemData = PlayerData.Instance.IngredientMeat; break;
			}

			if(itemData == 1){
				tempRandomItem.Add(i);
			}
		}

		int randomResult = UnityEngine.Random.Range(0,tempRandomItem.Count);
		return tempRandomItem[randomResult];
	}

	void InstantiateSeed(int seedIndex)
	{
//		int rnd = -1;
//		do{
//			rnd = UnityEngine.Random.Range(0,AvailableSeeds.Count);
//		}while(ValidateRandomizedIngredientSeed(rnd) == false);
		int rnd = RandomizeIngridientSeed();
		GameObject tempSeed = Instantiate(AvailableSeeds[rnd],stallSeedParent[seedIndex]);
		tempSeed.transform.localPosition = new Vector3(0f,0f,-1f);

		Seed seed = tempSeed.GetComponent<Seed>();
		seed.Init(seedIndex);
		seed.OnSeedPlanted += OnSeedPlanted;
		seed.OnDragSeed += ShowUICoin;
		seed.OnEndDragSeed += HideUICoin;


		currentSeeds.Add(tempSeed);
		SetCurrentSeedData(seedIndex,(int)seed.type);


	}

	void InstantiateSeed(int seedIndex, int seedIngredientIndex)
	{
//		int rnd = -1;
//		do{
//			rnd = UnityEngine.Random.Range(0,AvailableSeeds.Count);
//		}while(ValidateRandomizedIngredientSeed(rnd) == false);
//		int rnd = RandomizeIngridientSeed();
		GameObject tempSeed = Instantiate(AvailableSeeds[seedIngredientIndex],stallSeedParent[seedIndex]);
		tempSeed.transform.localPosition = new Vector3(0f,0f,-1f);

		Seed seed = tempSeed.GetComponent<Seed>();
		seed.Init(seedIndex);
		seed.OnSeedPlanted += OnSeedPlanted;
		seed.OnDragSeed += ShowUICoin;
		seed.OnEndDragSeed += HideUICoin;


		currentSeeds.Add(tempSeed);
		SetCurrentSeedData(seedIndex,(int)seed.type);

		if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == ShortCode.SCENE_GUIDED_TUTORIAL){
			switch(seedIndex){
			case 0:
				sceneGuidedTutorial.seed1 = seed;
				break;
			case 1:
				sceneGuidedTutorial.seed2 = seed;
				break;
			case 2:
				sceneGuidedTutorial.seed3 = seed;
				break;
			}
		}
	}

	int RandomizeIngridientSeed()
	{
		List<int> tempRandomSeed = new List<int>();

		for(int i = 0;i<AvailableSeeds.Count;i++){
			IngredientSeeds temp = (IngredientSeeds)i;
			string tempPrefKey = string.Empty;
			int seedData = -1;
			switch(temp){
			case IngredientSeeds.Cabbage: seedData = PlayerData.Instance.IngredientCabbage; break;
			case IngredientSeeds.Carrot: seedData = PlayerData.Instance.IngredientCarrot; break;
			case IngredientSeeds.Mushroom: seedData = PlayerData.Instance.IngredientMushroom; break;
			case IngredientSeeds.Potato: seedData = PlayerData.Instance.IngredientPotato; break;
			case IngredientSeeds.Tomato: seedData = PlayerData.Instance.IngredientTomato; break;
			}
			
			if(seedData == 1){
				tempRandomSeed.Add(i);
			}
		}

		int randomResult = UnityEngine.Random.Range(0,tempRandomSeed.Count);
		return tempRandomSeed[randomResult];
	}

	//delegate events
	void OnItemPicked(StallItem currentItem)
	{
		currentItem.OnItemPicked -= OnItemPicked;
		currentItem.OnDragStallItem -= ShowUICoin;
		currentItem.OnEndDragStallItem -= HideUICoin;

		int index = currentItem.itemIndex;
		SetCurrentItemData(currentItem.itemIndex,-1);
		
		currentItems[index] = null;
	}
	void OnSeedPlanted(Seed currentSeed)
	{
		currentSeed.OnSeedPlanted -= OnSeedPlanted;
		currentSeed.OnDragSeed -= ShowUICoin;
		currentSeed.OnEndDragSeed -= HideUICoin;

		int index = currentSeed.seedIndex;
		SetCurrentSeedData(currentSeed.seedIndex,-1);

		currentSeeds[index] = null;
	}

	void OnFinishWatchVideoAds(AdEvents eventName){
		if(eventName == AdEvents.RestockStall){
			RestockItems ();
		} else if(eventName == AdEvents.RestockSeeds){
			RestockSeeds ();
		}
	}

	void OnRefillStallWithGems (PopupEventType eventName)
	{
		if(eventName == PopupEventType.RestockStall){
			RestockItems ();
		} else if(eventName == PopupEventType.RestockSeeds){
			RestockSeeds ();
		}
	}

	//coin panel
	void ShowUICoin(int price)
	{
		uiCoin.ShowUI(price,true,true,false);
	}
	void HideUICoin(bool isBought)
	{
		uiCoin.CloseUI(isBought);
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
