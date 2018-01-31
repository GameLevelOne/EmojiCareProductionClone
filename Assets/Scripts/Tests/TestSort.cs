using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSort : MonoBehaviour {
	public TestRecipe recipePrefab;
	TestRecipe[] recipes = new TestRecipe[5];

	int[] a = new int[5]{33,161,200,5,43};
	int temp=0;

	List<int> testList = new List<int>();

	void Start(){
		InvokeRepeating ("AddNotif", 0, 5);
		StartCoroutine (AutoDelete ());
	}

	public void AddNotif(){
		testList.Add (10);
//		Debug.Log ("EVENT ADD - member count:" + testList.Count.ToString ());
	}

	IEnumerator AutoDelete(){
		while(true){
			if(testList.Count>=1){
				yield return new WaitForSeconds (2);
				testList.RemoveAt (0);
//				Debug.Log ("EVENT REMOVE - member count:" + testList.Count.ToString ());
				yield return null;
			}
			yield return null;
		}
	}

	public void Sort(){
		for(int i=1;i<5;i++){
			for(int j=0;j<i;j++){
				if(a[i]<a[j]){
					temp=a[i];
					a[i]=a[j];
					a[j]=temp;
				}
			}
		}

//		for(int i=0;i<5;i++){
////			Debug.Log(a[i]);
//		}
	}

	public void Init(){
		//r1 = carrot,egg
		//r2 = chicken,carrot
		//r3 = carrot,egg,chicken

		TestRecipe r1 = Instantiate(recipePrefab) as TestRecipe;
		List<Ingredients> r1List = new List<Ingredients>();
		r1List.Add(Ingredients.Carrot);
		r1List.Add(Ingredients.Tomato);
		r1List.Add(Ingredients.Meat);
		r1.InitRecipe(r1List);

		TestRecipe r2 = Instantiate(recipePrefab) as TestRecipe;
		List<Ingredients> r2List = new List<Ingredients>();
		r2List.Add(Ingredients.Cabbage);
		r2List.Add(Ingredients.Carrot);
		r2List.Add(Ingredients.Tomato);
		r2List.Add(Ingredients.Chicken);
		r2.InitRecipe(r2List);

		TestRecipe r3 = Instantiate(recipePrefab) as TestRecipe;
		List<Ingredients> r3List = new List<Ingredients>();
		r3List.Add(Ingredients.Flour);
		r3List.Add(Ingredients.Egg);
		r3List.Add(Ingredients.Meat);
		r3.InitRecipe(r3List);

		TestRecipe r4 = Instantiate(recipePrefab) as TestRecipe;
		List<Ingredients> r4List = new List<Ingredients>();
		r4List.Add(Ingredients.Flour);
		r4List.Add(Ingredients.Cheese);
		r4List.Add(Ingredients.Mushroom);
		r4List.Add(Ingredients.Tomato);
		r4.InitRecipe(r4List);

		TestRecipe r5 = Instantiate(recipePrefab) as TestRecipe;
		List<Ingredients> r5List = new List<Ingredients>();
		r5List.Add(Ingredients.Cabbage);
		r5List.Add(Ingredients.Tomato);
		r5List.Add(Ingredients.Fish);
		r5.InitRecipe(r5List);

		recipes[0]=r1;
		recipes[1]=r2;
		recipes[2]=r3;
		recipes[3]=r4;
		recipes[4]=r5;

		TestInput();
	}

	public void TestInput ()
	{	
		int correctRecipeIdx = -1;
		List<Ingredients> testList = new List<Ingredients> ();
		List<int> correctRecipes = new List<int>();
		testList.Add (Ingredients.Egg);
		testList.Add (Ingredients.Chicken);
		testList.Add(Ingredients.Cabbage);
		testList.Add(Ingredients.Fish);
		testList.Add(Ingredients.Tomato);
		testList.Add (Ingredients.Carrot);

		//testList.Add(Ingredients.Meat);

		foreach (Ingredients item in testList) {
			for (int i = 0; i < recipes.Length; i++) {
				foreach (Ingredients item2 in recipes[i].ingredients) {
					if (item == item2) {
						recipes [i].correctIngredientCounter++;
//						Debug.Log (recipes [i].correctIngredientCounter.ToString ());
					}
				}
				if (recipes [i].correctIngredientCounter == recipes [i].ingredients.Count) {
					correctRecipeIdx = i;
					break;
				}
			}
		}



		for(int i=0;i<recipes.Length;i++){
			foreach(Ingredients item2 in recipes[i].ingredients){
				if(testList[0] == item2){
					correctRecipes.Add(i);
				}
			}
		}

//		if (correctRecipeIdx != -1) {
////			Debug.Log ("correct recipe: ");
//			foreach (Ingredients item in recipes[correctRecipeIdx].ingredients) {
//				Debug.Log (item.ToString ());
//			}
//		}
	}
}
