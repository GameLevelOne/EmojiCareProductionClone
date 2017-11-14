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

	int gachaCount = 10;
	int minGem = 0;
	int maxGem = 0;
	int minCoin = 0;
	int maxCoin = 0;

	void Start(){
		Init ();
	}

	//INIT TEMP DATA
	void Init(){
		SetGachaRates (0.7f, 0.05f, 0.22f, 0.03f);
		SetMinMaxCoinGem (10, 100, 1, 3);
		unlockedHats.Add (HatType.One);
		//SimulateGacha ();
	}

	public void OpenGacha(){
		//TODO: add animations later
		Debug.Log ("GachaCount: " + gachaCount);
		if(gachaCount>0){
			gachaCount--;
			StartGacha ();

		}
	}

	void SimulateGacha()
	{
		int coin = 0;
		int gem = 0;
		int ingr = 0;
		int hat = 0;
		for(int i=0;i<100;i++){
			if (Random.value >= 0f && Random.value < rateGem) {
				gem++;
			} else if (Random.value >= rateGem && Random.value < (rateGem + rateCostume)) {
				hat++;
			} else if (Random.value >= (rateGem + rateCostume) && Random.value < (rateGem + rateCostume + rateIngredients)) {
				ingr++;
			} else {
				coin++;
			}
		}
		Debug.Log ("total coin: " + coin.ToString ());
		Debug.Log ("total gem: " + gem.ToString ());
		Debug.Log ("total ingredients: " + ingr.ToString ());
		Debug.Log ("total hat: " + hat.ToString ());
		Debug.Log ("rate coin: " + ((float)coin / 100).ToString());
		Debug.Log ("rate gem: " + ((float)gem / 100).ToString ());
		Debug.Log ("rate ingredients: " + ((float)ingr / 100).ToString ());
		Debug.Log ("rate hat: " + ((float)hat / 100).ToString ());
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

	public void GetGachaReward(){
		gachaCount++; 
	}

	void ProcessReward(RewardType type){
		string rewardDebug = "";
		if(type == RewardType.Gem){
			int gem = Random.Range (minGem, (maxGem + 1));
			PlayerData.Instance.PlayerGem += gem;
			rewardDebug = "Gem " + gem.ToString ();
		} else if(type == RewardType.Costume){
			CheckDuplicateCostume (Random.Range (0, (int)HatType.COUNT));
		} else if(type == RewardType.Ingredients){
			IngredientType ingredient =	(IngredientType) Random.Range (0, (int)IngredientType.COUNT);
			Debug.Log ("Ingredients: "+ingredient);
			PlayerData.Instance.inventory.ModIngredientValue (ingredient,1);
		} else if(type == RewardType.Coin){
			int coin = Random.Range (minCoin, (maxCoin + 1));
			Debug.Log("coin "+coin.ToString());
			PlayerData.Instance.PlayerCoin += coin;
		}	
	}

	void CheckDuplicateCostume(int result){
		bool dupeHat = false;
		foreach(HatType item in unlockedHats){
			if(item == (HatType)result){
				dupeHat = true;
				Debug.Log ("Dupe hat");
			}
		}
		if(dupeHat){
			Debug.Log ("reroll");
			StartGacha ();
		}else{
			unlockedHats.Add ((HatType)result);
			Debug.Log ("Hat " + (HatType)result);
		}
	}

}
