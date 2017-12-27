using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICookbook : BaseUI {
	public GameObject flipPageButton;
	public GameObject flippedPage;
	public GameObject[] pages;
	public GameObject[] Recipes;

	int totalPage = 3;
	int currentPage = 1;
	int counter=1;

	void OnEnable(){
//		ValidateRecipeAvailablility();
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
		Recipes[0].SetActive(PlayerData.Instance.RecipeCaesarSalad == 1 ? true : false);
		Recipes[1].SetActive(PlayerData.Instance.RecipeRamen == 1 ? true : false);
		Recipes[2].SetActive(PlayerData.Instance.RecipeBurger == 1 ? true : false);
		Recipes[3].SetActive(PlayerData.Instance.RecipeGrilledFish == 1 ? true : false);
		Recipes[4].SetActive(PlayerData.Instance.RecipePizza == 1 ? true : false);
		Recipes[5].SetActive(PlayerData.Instance.RecipeSundubu == 1 ? true : false);
		Recipes[6].SetActive(PlayerData.Instance.RecipeChickenAndFries == 1 ? true : false);
		Recipes[7].SetActive(PlayerData.Instance.RecipeBaconBakedPotato == 1 ? true : false);
		Recipes[8].SetActive(PlayerData.Instance.RecipeSkewer == 1 ? true : false);
		Recipes[9].SetActive(PlayerData.Instance.RecipeSteak == 1 ? true : false);
	}
}
