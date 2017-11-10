using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardType{
	Coin,Gem,Ingredients,Costume
}

public enum HatType{
	One,Two,Three,COUNT
}

public class GachaReward : MonoBehaviour {
	List<HatType> unlockedHats = new List<HatType> ();

	float rateCoin = 0f;
	float rateGem = 0f;
	float rateIngredients = 0f;
	float rateCostume = 0f;

	int minGem = 0;
	int maxGem = 0;
	int minCoin = 0;
	int maxCoin = 0;

	void Start(){
		//TEMP
		SetGachaRates (0.7f, 0.05f, 0.2f, 0.05f);
		SetMinMaxCoinGem (10, 100, 1, 3);
		unlockedHats.Add (HatType.One);
	}

	public void SetGachaRates(float rateCoin,float rateGem,float rateIngredients,float rateCostume){
		this.rateCoin = rateCoin;
		this.rateGem = rateGem;
		this.rateIngredients = rateIngredients;
		this.rateCostume = rateCostume;
	}

	public void SetMinMaxCoinGem(int minCoin,int maxCoin,int minGem,int maxGem){
		this.minCoin = minCoin;
		this.maxCoin = maxCoin;
		this.minGem = minGem;
		this.maxGem = maxGem;
	}

	public void StartGacha (){
		RewardType type = RewardType.Coin;
		if (Random.value >= 0f && Random.value < rateGem) {
			type = RewardType.Gem;
		} else if (Random.value >= rateGem && Random.value < (rateGem + rateCostume)) {
			type = RewardType.Costume;
		} else if (Random.value >= (rateGem + rateCostume) && Random.value < (rateGem + rateCostume + rateIngredients)) {
			type = RewardType.Ingredients;
		} else {
			type = RewardType.Coin;
		}
		ProcessReward (type);
	}

	void ProcessReward(RewardType type){
		if(type == RewardType.Gem){
			PlayerData.Instance.PlayerGem = Random.Range (minGem, (maxGem + 1));
		} else if(type == RewardType.Costume){
			CheckDuplicateCostume (Random.Range (0, (int)HatType.COUNT));
		} else if(type == RewardType.Ingredients){
			PlayerData.Instance.inventory.ModIngredientValue ((IngredientType)(Random.Range (0, (int)IngredientType.COUNT)),1);
		} else if(type == RewardType.Coin){
			PlayerData.Instance.PlayerCoin = Random.Range (minCoin, (maxCoin + 1));
		}	
	}

	void CheckDuplicateCostume(int result){
		bool dupeHat = false;
		foreach(HatType item in unlockedHats){
			if(item == (HatType)result){
				dupeHat = true;
			}
		}
		if(dupeHat){
			StartGacha ();
		}
	}

}
