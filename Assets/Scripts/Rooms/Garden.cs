using System.Collections;
using UnityEngine;
using System;

public class Garden : BaseRoom {
	[Header("Garden Attributes")]
	public GameObject[] Goods;
	public GameObject[] Seeds;
	public Transform stallTransform;
	[Header("Generated Objects")]
	public GameObject[] AvailableGoods;
	public GameObject[] AvailableSeeds;

	public void Init()
	{
		InitIngredientStall();
	}

	void InitIngredientStall()
	{
		
	}

	void ResetSeedsAndGoods()
	{
		string tempKey = string.Empty;
		int rnd = -1;
		for(int i = 0;i<4;i++){
			tempKey = PlayerPrefKeys.Game.GOODS + i.ToString();
			rnd = UnityEngine.Random.Range(0,6);
			AvailableGoods[i] = Instantiate(Goods[rnd],stallTransform);
			//atur2 transform sama init dll

			tempKey = PlayerPrefKeys.Game.SEEDS + i.ToString();
			rnd = UnityEngine.Random.Range(0,4);
			AvailableSeeds[i] = Instantiate(Seeds[rnd],stallTransform);
			//atur2 transform sama init dll

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
