using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : BaseFurniture {
	public List<GameObject> ingredients = new List<GameObject>();
	public Transform content;
	public GameObject cookingSmoke;
	public Cookbook cookBook;
	public bool hasIngredient;
	public bool isCooking;
	public GameObject cookedFoodObject = null;
	public GameObject cookingBar;

	public void AddIngredient(GameObject ingredient)
	{
		print("Inserting "+ingredient+" to pan");
		if(cookedFoodObject == null || isCooking){
			if(!hasIngredient) StartCoroutine(StoveOn());
			ingredient.transform.SetParent(content);
			ingredients.Add(ingredient);
			CheckIngredientCombination();
		}
	}

	void ClearIngredient()
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
		}
	}

	public void CheckIngredientCombination()
	{
		int foodIndex = -1;
		int correct = 0;

		for(int i = 0;i<cookBook.recipes.Length;i++){
			if(ingredients.Count == cookBook.recipes[i].ingredients.Count){

				for(int j = 0;j < ingredients.Count;j++){
					foreach(IngredientType t in cookBook.recipes[i].ingredients){
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
		if(foodIndex == -1) {
			print("Wrong recipe");
			return;
		}
		print("COOKING "+cookBook.recipes[foodIndex].foodObject.name);
		StartCoroutine(Cook(cookBook.recipes[foodIndex]));
	}

	public void PointerClick()
	{
		if(!isCooking) ClearIngredient();
	}

	IEnumerator StoveOn()
	{
		hasIngredient = true;
		while(true){
			float rndX = Random.Range(-0.3f,0.3f);
			float rndScale = Random.Range(0.3f,0.5f);
			GameObject s = Instantiate(cookingSmoke,transform);
			s.transform.localPosition = new Vector3(rndX,0.4f,0);
			s.transform.localScale = new Vector3(rndScale,rndScale,1f);
			yield return new WaitForSeconds(0.05f);
		}
	}

	IEnumerator Cook(Foods food)
	{
		//bar timer selama duration
		isCooking = true;
		for(int i = 0;i<ingredients.Count;i++) Destroy(ingredients[i]);
		cookingBar.GetComponent<UICookBar>().duration = food.cookDuration;
		cookingBar.SetActive(true);
		print("Now cooking for "+food.cookDuration+" seconds");

		float current = 0;
		while(current < food.cookDuration){
			cookingBar.GetComponent<UICookBar>().UpdateBar(current);
			current += Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		}

		//instantiate food
		cookedFoodObject = Instantiate(food.foodObject,transform.parent);
		cookedFoodObject.transform.localPosition = new Vector3(2.8f,2f,-1f);
		cookingBar.SetActive(false);
		isCooking = false;
		StopAllCoroutines();
		ClearIngredient();
	}
}
