using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RewardType{
	Coin,Gem,Ingredients,Costume
}

public enum HatType{
	One,Two,Three,COUNT
}

public enum AnimState{
	OpenGacha,CloseGacha
}

public class GachaReward : BaseUI {
	public Button touchArea;
	public Text gachaCountTextInButton;
	public Text rewardAmount;
	public Image rewardIcon;
	public Animator gachaAnim;
	public GameObject screenGacha;
	public GameObject buttonGacha;

	public Sprite iconCoin;
	public Sprite iconGem;
	public Sprite[] iconIngredients;

	List<HatType> unlockedHats = new List<HatType> ();

	float rateCoin = 0f;
	float rateGem = 0f;
	float rateIngredients = 0f;
	float rateCostume = 0f;

	int gachaCount = 0;
	int minGem = 0;
	int maxGem = 0;
	int minCoin = 0;
	int maxCoin = 0;

	AnimState currentAnimState = AnimState.CloseGacha;

	//TEMP
	string gachaPrefKey = "GachaCount";

	void Start(){
		Init ();
	}

	//INIT TEMP DATA
	public void Init(){
		SetGachaRates (0.7f, 0.05f, 0.22f, 0.03f);
		SetMinMaxCoinGem (10, 100, 1, 3);
		unlockedHats.Add (HatType.One);
		gachaCount = PlayerPrefs.GetInt (gachaPrefKey, 0);
		gachaCountTextInButton.text = gachaCount.ToString ();
	}

	public void OpenGacha(){
		//TODO: add animations later
		Debug.Log ("GachaCount: " + gachaCount);
		if(gachaCount>0){
			gachaCount--;
			StartGacha ();
		}
	}

	public void TapGachaPack(){
		if(currentAnimState == AnimState.CloseGacha){
			//opening
			currentAnimState = AnimState.OpenGacha;
			gachaCount--;
			PlayerPrefs.SetInt (gachaPrefKey, gachaCount);
			gachaCountTextInButton.text = gachaCount.ToString ();
			StartGacha ();
			StartCoroutine (WaitForAnim (AnimState.OpenGacha.ToString ()));
		} else if(currentAnimState == AnimState.OpenGacha){
			//closing
			currentAnimState = AnimState.CloseGacha;
			if(gachaCount>0){
				StartCoroutine (WaitForAnim (AnimState.CloseGacha.ToString ()));
			} else{
				base.CloseUI (screenGacha);
			}
		}
	}

	public void OpenScreenGacha ()
	{
		if (gachaCount > 0) {
			base.ShowUI (screenGacha);
			buttonGacha.SetActive (false);
		}
	}

	IEnumerator WaitForAnim(string animBool){
		gachaAnim.SetBool (animBool, true);
		touchArea.interactable = false;
		yield return new WaitForSeconds (0.35f);
		gachaAnim.SetBool (animBool, false);
		touchArea.interactable = true;
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
		gachaCountTextInButton.text = gachaCount.ToString ();
	}

	void ProcessReward(RewardType type){
		int gem = 0;
		int coin = 0;

		if(type == RewardType.Gem){
			gem = Random.Range (minGem, (maxGem + 1));
			PlayerData.Instance.PlayerGem += gem;
			Debug.Log ("Gem: "+gem);
			UpdateRewardDisplay (type, IngredientType.COUNT,gem);
		} else if(type == RewardType.Costume){
			CheckDuplicateCostume (Random.Range (0, (int)HatType.COUNT));
			//update reward
		} else if(type == RewardType.Ingredients){
			IngredientType ingredient =	(IngredientType) Random.Range (0, (int)IngredientType.COUNT);
			Debug.Log ("Ingredients: "+ingredient);
			PlayerData.Instance.inventory.ModIngredientValue (ingredient,1);
			UpdateRewardDisplay (type, ingredient, 1);
		} else if(type == RewardType.Coin){
			coin = Random.Range (minCoin, (maxCoin + 1));
			Debug.Log("coin "+coin.ToString());
			PlayerData.Instance.PlayerCoin += coin;
			UpdateRewardDisplay (type, IngredientType.COUNT,coin);
		}	
	}

	void UpdateRewardDisplay(RewardType type,IngredientType ingredientType = IngredientType.COUNT,int amount=1){
		if(amount == 1){
			rewardAmount.gameObject.SetActive (false);
		} else{
			rewardAmount.gameObject.SetActive (true);
			rewardAmount.text = "x" + amount.ToString ();
		}

		if(type == RewardType.Coin){
			rewardIcon.sprite = iconCoin;
		} else if(type == RewardType.Gem){
			rewardIcon.sprite = iconGem;
		} else if(type == RewardType.Ingredients){
			rewardIcon.sprite = iconIngredients [(int)ingredientType];
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
