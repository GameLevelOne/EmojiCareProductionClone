using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Garden : BaseRoom {
	[Header("Garden Attributes")]
	public GameObject[] Goods;
	public GameObject[] Seeds;
	public Soil soil;
	public Transform stallContent;

	[Header("Generated Objects")]
	public List<GameObject> AvailableGoods = new List<GameObject>();
	public List<GameObject> AvailableSeeds = new List<GameObject>();
	public bool hasInit = false;

	public void Init()
	{
		if(!hasInit){
			hasInit = true;
			InitIngredientStall();
		}
		soil.Init();
	}

	void InitIngredientStall()
	{
		if(PlayerPrefs.HasKey(PlayerPrefKeys.Player.LAST_TIME_PLAYED)){
			if(isChangingDay()){
				print("A");
				RandomizeSeedsAndGoods();
			}else{
				print("B");
				LoadSeedsAndGoods();
			}
		}else{
			print("C");
			RandomizeSeedsAndGoods();
		}
	}

	void OnGoodHarvested(int index)
	{
		AvailableGoods[index].GetComponent<Good>().OnGoodHarvested -= OnGoodHarvested;

		string tempKey = PlayerPrefKeys.Game.GOODS + index.ToString();
		PlayerPrefs.SetInt(tempKey,-1);
	}

	void OnSeedPlanted(int index)
	{
		AvailableSeeds[index].GetComponent<Seed>().OnSeedPlanted -= OnSeedPlanted;

		string tempKey = PlayerPrefKeys.Game.SEEDS + index.ToString();
		PlayerPrefs.SetInt(tempKey,-1);
	}

	void LoadSeedsAndGoods()
	{
		for (int i = 0;i < 4;i++){ //goods
			string tempKey = PlayerPrefKeys.Game.GOODS + i.ToString();
			int index = PlayerPrefs.GetInt(tempKey,-1);
			if(index != -1){
				GameObject tempGood = (GameObject)Instantiate (Goods [index], stallContent.parent);
				tempGood.transform.localPosition = stallContent.FindChild("Goods").GetChild(i).localPosition;
				tempGood.GetComponent<Good>().Init(i);
				tempGood.GetComponent<Good>().OnGoodHarvested += OnGoodHarvested;
				AvailableGoods.Add( tempGood );

			}

			tempKey = PlayerPrefKeys.Game.SEEDS + i.ToString();
			index = PlayerPrefs.GetInt(tempKey,-1);
			if(index != -1){
				GameObject tempSeed = (GameObject)Instantiate (Seeds [index], stallContent.parent);
				tempSeed.transform.localPosition = stallContent.FindChild("Seeds").GetChild(i).localPosition;
				tempSeed.GetComponent<Seed>().Init(i);
				tempSeed.GetComponent<Seed>().OnSeedPlanted += OnSeedPlanted;
				AvailableSeeds.Add ( tempSeed );

			}

		}
	}

	void RandomizeSeedsAndGoods()
	{
		if(AvailableGoods.Count > 0) foreach(GameObject g in AvailableGoods) Destroy(g);
		if(AvailableSeeds.Count > 0) foreach(GameObject g in AvailableSeeds) Destroy(g);
		AvailableGoods.Clear();
		AvailableSeeds.Clear();
		string tempKey = string.Empty;
		int rnd = -1;
		for(int i = 0;i<4;i++){ //goods
			
			tempKey = PlayerPrefKeys.Game.GOODS + i.ToString();
			rnd = UnityEngine.Random.Range(0,6);

			PlayerPrefs.SetInt(tempKey,rnd);

			GameObject tempGood = (GameObject)Instantiate (Goods [rnd], stallContent.parent);
			tempGood.transform.localPosition = stallContent.FindChild("Goods").GetChild(i).localPosition;
			tempGood.GetComponent<Good>().Init(i);
			tempGood.GetComponent<Good>().OnGoodHarvested += OnGoodHarvested;
			AvailableGoods.Add(tempGood);



			tempKey = PlayerPrefKeys.Game.SEEDS + i.ToString();
			rnd = UnityEngine.Random.Range(0,4);

			PlayerPrefs.SetInt(tempKey,rnd);

			GameObject tempSeed = (GameObject)Instantiate (Seeds [rnd], stallContent.parent);
			tempSeed.transform.localPosition = stallContent.FindChild("Seeds").GetChild(i).localPosition;
			tempSeed.GetComponent<Seed>().Init(i);
			tempSeed.GetComponent<Seed>().OnSeedPlanted += OnSeedPlanted;
			AvailableSeeds.Add (tempSeed);

		}
	}

	bool isChangingDay()
	{
		DateTime lastTime = PlayerData.Instance.PlayerEmoji.lastTimePlayed;
		TimeSpan deltaTime = DateTime.Now - lastTime;
		if(deltaTime.Days >= 1){
			return true;
		}else{
			return false;
		}
	}
}
