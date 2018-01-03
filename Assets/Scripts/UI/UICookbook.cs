using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICookbook : BaseUI {
	public GameObject flipPageButton;
	public GameObject flippedPage;
	public GameObject[] pages;
	public GameObject[] Recipes;

	int totalPage = 3;
	int currentPage = 1;
	int counter=1;

	void OnEnable(){
		ValidateRecipeAvailablility();
		SetActivePage (1);
	}

	public void OnClickNext(){
		if(currentPage == totalPage){
			currentPage = totalPage;
		} else{
			currentPage++;
		}
		SetActivePage (currentPage);
	}

	public void OnClickPrev(){
		if(currentPage == 1){
			currentPage = 1;
		} else{
			currentPage--;
		}
		SetActivePage (currentPage);
	}

	void SetActivePage(int page){
		for(int i=0;i<pages.Length;i++){
			if(i == (page-1)){
				pages[i].SetActive (true);
			} else{
				pages[i].SetActive (false);
			}
		}
		if(page == totalPage){
			flipPageButton.SetActive (false);
		} else{
			flipPageButton.SetActive (true);
		}
	}

	void ValidateRecipeAvailablility()
	{
		//TEST DATA
		PlayerData.Instance.RecipeRamen = 1;
		PlayerData.Instance.RecipeBurger = 1;
		PlayerData.Instance.RecipeGrilledFish = 1;
		PlayerData.Instance.RecipePizza = 1;
		PlayerData.Instance.RecipeSundubu = 1;
		PlayerData.Instance.RecipeChickenAndFries = 1;
		PlayerData.Instance.RecipeBaconBakedPotato = 1;

//		Recipes[0].SetActive(PlayerData.Instance.RecipeCaesarSalad == 1 ? true : false);
//		Recipes[1].SetActive(PlayerData.Instance.RecipeRamen == 1 ? true : false);
//		Recipes[2].SetActive(PlayerData.Instance.RecipeBurger == 1 ? true : false);
//		Recipes[3].SetActive(PlayerData.Instance.RecipeGrilledFish == 1 ? true : false);
//		Recipes[4].SetActive(PlayerData.Instance.RecipePizza == 1 ? true : false);
//		Recipes[5].SetActive(PlayerData.Instance.RecipeSundubu == 1 ? true : false);
//		Recipes[6].SetActive(PlayerData.Instance.RecipeChickenAndFries == 1 ? true : false);
//		Recipes[7].SetActive(PlayerData.Instance.RecipeBaconBakedPotato == 1 ? true : false);
//		Recipes[8].SetActive(PlayerData.Instance.RecipeSkewer == 1 ? true : false);
//		Recipes[9].SetActive(PlayerData.Instance.RecipeSteak == 1 ? true : false);

		SetRecipeDisplay (0,PlayerData.Instance.RecipeCaesarSalad);
		SetRecipeDisplay (1,PlayerData.Instance.RecipeRamen);
		SetRecipeDisplay (2,PlayerData.Instance.RecipeBurger);
		SetRecipeDisplay (3,PlayerData.Instance.RecipeGrilledFish);
		SetRecipeDisplay (4,PlayerData.Instance.RecipePizza);
		SetRecipeDisplay (5,PlayerData.Instance.RecipeSundubu);
		SetRecipeDisplay (6,PlayerData.Instance.RecipeChickenAndFries);
		SetRecipeDisplay (7,PlayerData.Instance.RecipeBaconBakedPotato);
		SetRecipeDisplay (8,PlayerData.Instance.RecipeSkewer);
		SetRecipeDisplay (9,PlayerData.Instance.RecipeSteak);

//		int counter = 0;
//		for(int i=0;i<Recipes.Length;i++){
//			if(Recipes[i].activeInHierarchy){
//				counter++;
//			}
//		}
//
//		if(counter >=1 && counter <=4){
//			totalPage = 1;
//		} else if(counter>=5 && counter<=7){
//			totalPage = 2;
//		} else{
//			totalPage = 3;
//		}
	}

	void SetRecipeDisplay(int recipeIdx,int unlockValue){
		foreach(Transform child in Recipes[recipeIdx].transform){
			if (unlockValue == 1)
				child.GetComponent<Image> ().color = new Color (1, 1, 1, 1);
			else
				child.GetComponent<Image> ().color = new Color (0, 0, 0, 0.5f);
		}
	}
}
