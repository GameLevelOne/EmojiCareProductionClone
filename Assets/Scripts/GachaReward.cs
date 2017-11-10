using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardType{
	Coin,Gem,Ingredients,Costume
}

public class GachaReward : MonoBehaviour {
	
	float rateCoin = 0f;
	float rateGem = 0f;
	float rateIngredients = 0f;
	float rateCostume = 0f;

	void Start(){
		//TEMP
		SetGachaRates (0.7f, 0.05f, 0.2f, 0.05f);
	}

	public void SetGachaRates(float rateCoin,float rateGem,float rateIngredients,float rateCostume){
		this.rateCoin = rateCoin;
		this.rateGem = rateGem;
		this.rateIngredients = rateIngredients;
		this.rateCostume = rateCostume;
	}

	public void StartGacha (){
		RewardType type = RewardType.Coin;
		if (Random.value >= 0f && Random.value < rateGem) {
			
		} else if (Random.value >= rateGem && Random.value < (rateGem + rateCostume)) {
			
		} else if (Random.value >= (rateGem + rateCostume) && Random.value < (rateGem + rateCoin + rateCostume)) {
			
		} else {
			
		}
		ProcessReward (type);
	}

	void ProcessReward(RewardType type){
		if(type == RewardType.Gem){
			
		} else if(type == RewardType.Costume){
			
		} else if(type == RewardType.Ingredients){
			
		} else if(type == RewardType.Coin){
			
		}	
	}

	void CheckDuplicateCostume(){

	}

}
