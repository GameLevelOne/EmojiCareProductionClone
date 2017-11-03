using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Garden : BaseRoom {
	[Header("Garden Attributes")]
	public GameObject[] Goods;
	public GameObject[] Seeds;
	public Transform stallContent;
	[Header("Generated Objects")]
	public List<GameObject> AvailableGoods = new List<GameObject>();
	public List<GameObject> AvailableSeeds = new List<GameObject>();

	public void Init()
	{
		InitIngredientStall();
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
		string tempKey = PlayerPrefKeys.Game.SEEDS + index.ToString();
		PlayerPrefs.SetInt(tempKey,-1);
	}

	void LoadSeedsAndGoods()
	{
		for (int i = 0;i < 4;i++){ //goods
			string tempKey = PlayerPrefKeys.Game.GOODS + i.ToString();
			int index = PlayerPrefs.GetInt(tempKey,-1);
			if(index != -1){
				AvailableGoods.Add( (GameObject) Instantiate(Goods[index],stallContent.parent) );
				AvailableGoods[i].transform.localPosition = stallContent.FindChild("Goods").GetChild(i).localPosition;
				AvailableGoods[i].GetComponent<Good>().Init(i);
				AvailableGoods[i].GetComponent<Good>().OnGoodHarvested += OnGoodHarvested;
			}

//			if(i < 2){ //seeds
//				tempKey = PlayerPrefKeys.Game.SEEDS + i.ToString();
//				index = PlayerPrefs.GetInt(tempKey,-1);
//				if(index != -1){
//					AvailableSeeds.Add ( (GameObject) Instantiate(Seeds[index],stallContent.parent));
//					AvailableSeeds[i].transform.localPosition = stallContent.FindChild("Seeds").GetChild(i).localPosition;
//				}
//			}
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

			AvailableGoods.Add((GameObject) Instantiate(Goods[rnd],stallContent.parent));
			AvailableGoods[i].transform.localPosition = stallContent.FindChild("Goods").GetChild(i).localPosition;
			AvailableGoods[i].GetComponent<Good>().Init(i);
			AvailableGoods[i].GetComponent<Good>().OnGoodHarvested += OnGoodHarvested;

//			if(i < 2){ //seeds
//				
//				tempKey = PlayerPrefKeys.Game.SEEDS + i.ToString();
//				rnd = UnityEngine.Random.Range(0,4);
//				PlayerPrefs.SetInt(tempKey,rnd);
//
//				AvailableSeeds.Add ((GameObject) Instantiate(Seeds[rnd],stallContent.parent));
//				AvailableSeeds[i].transform.localPosition = stallContent.FindChild("Seeds").GetChild(i).localPosition;
//			}
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
