using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanState{
	Idle = 0,
	Open,
	Close,
	OpenClose,
	Cooking,
	Animate
}

public class Pan : BaseFurniture {
	public delegate void CookingDone();
	public event CookingDone OnCookingDone;
	#region attributes
	[Header("Pan Attributes")]
	public List<GameObject> ingredients = new List<GameObject>();
	public Transform content;
	public ParticleSystem smoke;
	public Cookbook cookBook;

	public GameObject cookingBar;
	public Animator thisAnim;
	public UIIngredientsInPan ingredientContentUI;
	public Plate plate;

	public bool hasIngredient = false;
	public bool isCooking;

	const int MAX_INGREDIENT = 5;
	Foods foodToCook;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initializations
	public void RegisterIngredientPickEvents(Ingredient ingredient)
	{
		ingredient.OnIngredientPicked += Animate;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void Animate(PanState state)
	{
		thisAnim.SetInteger(AnimatorParameters.Ints.STATE,(int)state);
	}

	void ShowContent()
	{
		ingredientContentUI.ShowIngredient();
	}

	void DestroyIngredients()
	{
		//unregister and destroy
		foreach(GameObject g in ingredients){ 
			g.GetComponent<Ingredient>().OnIngredientPicked -= Animate;
			Destroy(g);
		}

		ingredients.Clear();
		hasIngredient = false;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------

	#region public modules
	public void AddIngredient(GameObject ingredient)
	{
		if(ingredients.Count < MAX_INGREDIENT){
			if(!isCooking){
				if(!hasIngredient){
					hasIngredient = true;
					if(smoke.isStopped) smoke.Play();
				}
				ingredient.transform.SetParent(content);
				ingredients.Add(ingredient);
				CheckIngredientCombination();
				print("Inserting "+ingredient+" to pan, current ingredient = "+ingredients.Count);
				SoundManager.Instance.PlaySFXOneShot(SFXList.Bong);
			}
		}else{
			print("Max Ingredient amount reached (5)");
		}

	}

	public void ClearIngredient()
	{
		if(ingredients.Count > 0){
			for(int i = 0;i<ingredients.Count;i++){
				ingredients[i].transform.position = new Vector3(0f,0.5f,-1f);
				ingredients[i].transform.SetParent(transform.parent,true);
				ingredients[i].GetComponent<Ingredient>().thisCollider.enabled = true;
				ingredients[i].GetComponent<Ingredient>().thisRigidbody.simulated = true;
				ingredients[i].SetActive(true);
			}
			ingredients.Clear();
			StopAllCoroutines();
			hasIngredient = false;
			Animate(PanState.OpenClose);
		}
		if(smoke.isPlaying) smoke.Stop();
	}

	public void CheckIngredientCombination()
	{
		int foodIndex = -1;
		int correct = 0;

		//for every recipes
		for(int i = 0;i<cookBook.recipes.Length;i++){

//			if(isUnlocked(i)){ <<<<<<< YANG INI
				//if total ingredients amount match the recipe's ingredient amount
				if(ingredients.Count == cookBook.recipes[i].ingredients.Count){

					//match every ingredient in pan with recipe, correct value will add if match
					for(int j = 0;j < ingredients.Count;j++){
						foreach(IngredientType t in cookBook.recipes[i].ingredients){
							print("Ingredient "+ingredients[j].GetComponent<Ingredient>().type+" compare to "+t);
							if(ingredients[j].GetComponent<Ingredient>().type == t) correct++;
						}
					}


					if(correct == cookBook.recipes[i].ingredients.Count) {
						foodIndex = i;
						break;
					}
					else correct = 0;
				}
			}
//		} <<<<<<<<<<<YANG INI


		if(foodIndex == -1) {
			print("Wrong recipe");
			return;
		}
		print("Cooking "+cookBook.recipes[foodIndex].foodObject.name+" for "+cookBook.recipes[foodIndex].cookDuration+" seconds.");
		foodToCook = cookBook.recipes[foodIndex];
		StartCoroutine(Cook());
	}

	/// <summary>
	/// Is the recipe unlocked?
	/// </summary>
	bool isUnlocked(int index){
		bool temp = false;
		switch(index){
		case 0: temp = PlayerData.Instance.RecipeSteak == 1 ? true : false; break;
		case 1: temp = PlayerData.Instance.RecipeCaesarSalad == 1 ? true : false; break;
		case 2: temp = PlayerData.Instance.RecipeRamen == 1 ? true : false; break;
		case 3: temp = PlayerData.Instance.RecipePizza == 1 ? true : false; break;
		case 4: temp = PlayerData.Instance.RecipeSundubu == 1 ? true : false; break;
		case 5: temp = PlayerData.Instance.RecipeBurger == 1 ? true : false; break;
		case 6: temp = PlayerData.Instance.RecipeGrilledFish == 1 ? true : false; break;
		case 7: temp = PlayerData.Instance.RecipeChickenAndFries == 1 ? true : false; break;
		case 8: temp = PlayerData.Instance.RecipeBaconBakedPotato == 1 ? true : false; break;
		case 9: temp = PlayerData.Instance.RecipeSkewer == 1 ? true : false; break;
		}

		return temp;
	}

	//event triggers
	public void PointerClick()
	{
		if(!isCooking){
			if(ingredients.Count <= 0){
				Animate(PanState.Animate);
			}else{
				if(!isCooking){
					//show content
					ShowContent();
					Animate(PanState.Animate);
				}
			}
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region coroutines
//	IEnumerator StoveOn()
//	{
//		hasIngredient = true;
//		while(true){
//			float rndX = Random.Range(-0.3f,0.3f);
//			float rndScale = Random.Range(0.3f,0.5f);
//			GameObject s = Instantiate(cookingSmoke,transform);
//			s.transform.localPosition = new Vector3(rndX,0.4f,0);
//			s.transform.localScale = new Vector3(rndScale,rndScale,1f);
//			yield return new WaitForSeconds(0.05f);
//		}
//	}

	IEnumerator Cook()
	{
		DestroyIngredients();
		PlayerData.Instance.PlayerEmoji.emojiExpressions.SetExpression(EmojiExpressionState.WHISTLE,3f);
		isCooking = true;

		Animate(PanState.Cooking);

		cookingBar.GetComponent<UICookBar>().duration = foodToCook.cookDuration;
		cookingBar.SetActive(true);

		SoundManager.Instance.PlaySFX(SFXList.Cook);

		float current = 0;
		while(current < foodToCook.cookDuration){
			cookingBar.GetComponent<UICookBar>().UpdateBar(current);
			current += Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		}

		//-----DONE COOKING-----
		if(OnCookingDone != null) OnCookingDone();
		SoundManager.Instance.StopSFX();
		SoundManager.Instance.PlaySFXOneShot(SFXList.Ding);

		StopAllCoroutines();
		Animate(PanState.OpenClose);

		if(smoke.isPlaying) smoke.Stop();
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region PlateAnimationEvent
	public void MovePlate()
	{
		StartCoroutine(MovingPlate());
	}
	IEnumerator MovingPlate()
	{
		//MOVE PLATE TO TOP OF PAN
		Vector3 plateStartPos = plate.transform.localPosition;
		Vector3 plateTargetPos = new Vector3(1.456f,2.548f,-1f);

		plate.thisCollider.enabled = false;
		plate.thisRigidbody.simulated = false;
		float current = 0;
		while(current < 1){
			plate.transform.localPosition = Vector3.Lerp(plateStartPos,plateTargetPos,current);
			current += Time.deltaTime * 4f;
			yield return null;
		}
		plate.transform.localPosition = plateTargetPos;
		plate.thisCollider.enabled = true;
		plate.thisRigidbody.simulated = true;
	}

	public void InstantiateFood()
	{
		//instantiate food, GIVE UP FORCE
		GameObject foodObject = Instantiate(foodToCook.foodObject,transform.parent);
		foodObject.transform.localPosition = new Vector3(1.456f,3f,-1f);
		foodObject.GetComponent<Food>().thisRigidbody.AddForce(new Vector2(0,300f));
		cookingBar.SetActive(false);
		isCooking = false;
	}
	#endregion
}
