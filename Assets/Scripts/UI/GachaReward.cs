using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RewardType{
	Coin,Gem,Ingredients,Costume
}

public enum AnimState{
	OpenGacha,CloseGacha
}

public class GachaReward : BaseUI {
	[Header("Gacha Attribute")]
	public Button touchArea;
	public Text textGachaCount;
	public Text rewardAmount;
	public Text textGachaCountInScreen;
	public Image rewardIcon;
	public Animator gachaAnim;
	public GameObject screenGacha;
	public GameObject buttonGacha;
	public GameObject shiningImage;
	public GameObject buttonBack;

	public Sprite iconCoin;
	public Sprite iconGem;
	public Sprite[] iconIngredients;

	public int gachaCount = 0;

	[Header("Chances (1 = 100%)")]
	[Range(0,1)] public float rateCoin = 0.7f;
	[Range(0,1)] public float rateGem = 0.05f;
	[Range(0,1)] public float rateIngredients = 0.22f;
	[Range(0,1)] public float rateCostume = 0f;

	[Header("Reward Value Range")]
	public int minGem = 1;
	public int maxGem = 3;
	public int minCoin = 10;
	public int maxCoin = 100;

	List<HatType> unlockedHats = new List<HatType> ();

	AnimState currentAnimState = AnimState.CloseGacha;

	//constants
	const string gachaPrefKey = "GachaCount";
	const string gachaAnimParameter = "OpenGacha";
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initializations
	void Start(){
//		Init ();
		//GetGachaReward();
		//GetGachaReward();
	}

	void OnDestroy(){
		UnregisterEmojiEvents();
	}

	public void Init(){

		gachaCount = PlayerPrefs.GetInt (gachaPrefKey, 0);
		textGachaCount.text = gachaCount.ToString ();
		if(gachaCount <= 0) buttonGacha.SetActive(false);
	}

	public void RegisterEmojiEvents()
	{
		PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent -= OnEmojiSleepEvent;
	}

	public void UnregisterEmojiEvents()
	{
		PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent -= OnEmojiSleepEvent;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	//button gacha
	public void OpenScreenGacha ()
	{
		if (gachaCount > 0) {
			base.ShowUI (screenGacha);
			gachaAnim.SetBool (gachaAnimParameter, false);
			textGachaCountInScreen.gameObject.SetActive(true);
			textGachaCountInScreen.text = gachaCount.ToString();
			buttonGacha.SetActive (false);
		}
	}

	public void CloseScreenGacha()
	{
		currentAnimState = AnimState.CloseGacha;
		base.CloseUI(screenGacha);

		if(gachaCount > 0){
			buttonGacha.SetActive (true);
		}
	}

	//button gacha screen
	public void TapGachaPack(){
		if(currentAnimState == AnimState.CloseGacha){
			if (SoundManager.Instance)
				SoundManager.Instance.PlaySFXOneShot (SFXList.Achievement);
			currentAnimState = AnimState.OpenGacha;

			gachaCount--;
			PlayerPrefs.SetInt (gachaPrefKey, gachaCount);
			textGachaCount.text = gachaCount.ToString ();
			if(gachaCount <= 0){
				textGachaCountInScreen.gameObject.SetActive(false);
			}else{
				textGachaCountInScreen.gameObject.SetActive(true);
				textGachaCountInScreen.text = gachaCount.ToString();
			}


			gachaAnim.SetBool (gachaAnimParameter, true);
			StartGacha ();
		}else if(currentAnimState == AnimState.OpenGacha){
			currentAnimState = AnimState.CloseGacha;
			if(gachaCount > 0){
				gachaAnim.SetBool (gachaAnimParameter, false);
			}else{
				base.CloseUI(screenGacha);
			}
		}


		//StartCoroutine (WaitForAnim (AnimState.OpenGacha.ToString ()));

//		if(currentAnimState == AnimState.CloseGacha){
//			//opening
//
//		} else if(currentAnimState == AnimState.OpenGacha){
//			//closing
//			currentAnimState = AnimState.CloseGacha;
//			if(gachaCount>0){
//				StartCoroutine (WaitForAnim (AnimState.CloseGacha.ToString ()));
//			} else{
//				base.CloseUI (screenGacha);
//			}
//		}
	}

	//GACHA MECHANICS
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
		int gem = 0;
		int coin = 0;

		if(type == RewardType.Gem){
			gem = Random.Range (minGem, (maxGem + 1));
			PlayerData.Instance.PlayerGem += gem;
//			Debug.Log ("Gem: "+gem);
			UpdateRewardDisplay (type, IngredientType.COUNT,gem);
		} else if(type == RewardType.Costume){
			CheckDuplicateCostume (Random.Range (0, (int)HatType.COUNT));
			//update reward
		} else if(type == RewardType.Ingredients){
			IngredientType ingredient =	(IngredientType) Random.Range (0, (int)IngredientType.COUNT);
//			Debug.Log ("Ingredients: "+ingredient);
			PlayerData.Instance.inventory.ModIngredientValue (ingredient,1);
			UpdateRewardDisplay (type, ingredient, 1);
		} else if(type == RewardType.Coin){
			coin = Random.Range (minCoin, (maxCoin + 1));
//			Debug.Log("coin "+coin.ToString());
			PlayerData.Instance.PlayerCoin += coin;
			UpdateRewardDisplay (type, IngredientType.COUNT,coin);
		}	
	}

	void UpdateRewardDisplay(RewardType type,IngredientType ingredientType = IngredientType.COUNT,int amount=1){
		if(type == RewardType.Coin){
			rewardIcon.sprite = iconCoin;
			rewardAmount.text = "x" + amount.ToString();
		} else if(type == RewardType.Gem){
			rewardIcon.sprite = iconGem;
			rewardAmount.text = "x" + amount.ToString();
		} else if(type == RewardType.Ingredients){
			rewardIcon.sprite = iconIngredients [(int)ingredientType];
			rewardAmount.text = "1 " + ingredientType.ToString();
		} 
	}

	void CheckDuplicateCostume(int result){
		bool dupeHat = false;
		foreach(HatType item in unlockedHats){
			if(item == (HatType)result){
				dupeHat = true;
//				Debug.Log ("Dupe hat");
			}
		}
		if(dupeHat){
//			Debug.Log ("reroll");
			StartGacha ();
		}else{
			unlockedHats.Add ((HatType)result);
//			Debug.Log ("Hat " + (HatType)result);
		}
	}

	void OnEmojiSleepEvent(bool isSleeping){
		buttonGacha.GetComponent<Button> ().interactable = !isSleeping;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	//this is called when 1 emoji expression progress reach 100%
	public void GetGachaReward(){
		gachaCount++; 
		PlayerPrefs.SetInt(gachaPrefKey,gachaCount);
		textGachaCount.text = gachaCount.ToString ();

		if(gachaCount > 0) buttonGacha.SetActive(true);
	}

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region coroutines
	IEnumerator WaitForAnim(string animBool){
		gachaAnim.SetBool (animBool, true);

		yield return new WaitForSeconds (0.35f);
		gachaAnim.SetBool (animBool, false);

	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	//unused
//	void SimulateGacha()
//	{
//		int coin = 0;
//		int gem = 0;
//		int ingr = 0;
//		int hat = 0;
//		for(int i=0;i<100;i++){
//			if (Random.value >= 0f && Random.value < rateGem) {
//				gem++;
//			} else if (Random.value >= rateGem && Random.value < (rateGem + rateCostume)) {
//				hat++;
//			} else if (Random.value >= (rateGem + rateCostume) && Random.value < (rateGem + rateCostume + rateIngredients)) {
//				ingr++;
//			} else {
//				coin++;
//			}
//		}
//		Debug.Log ("total coin: " + coin.ToString ());
//		Debug.Log ("total gem: " + gem.ToString ());
//		Debug.Log ("total ingredients: " + ingr.ToString ());
//		Debug.Log ("total hat: " + hat.ToString ());
//		Debug.Log ("rate coin: " + ((float)coin / 100).ToString());
//		Debug.Log ("rate gem: " + ((float)gem / 100).ToString ());
//		Debug.Log ("rate ingredients: " + ((float)ingr / 100).ToString ());
//		Debug.Log ("rate hat: " + ((float)hat / 100).ToString ());
//	}
}
