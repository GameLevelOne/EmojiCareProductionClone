using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenRefrigeratorContent : BaseUI {
	public Text[] textObjectQuantity;
	public Vector3[] ingredientStartPos;
	public GameObject[] ingredientPrefabs;

	public Transform refriObjParent;
	public Transform heldObjParent;

	//temp
	int[] quantity = new int[10];

	GameObject[] ingredientObjects = new GameObject[10];

	void Awake(){
		for(int i=0;i<10;i++){
			quantity[i]=99;
		}
	}

	void OnEnable(){
		RefrigeratorIngredient.OnUpdateQuantity += OnUpdateQuantity;
		for(int i=0;i<textObjectQuantity.Length;i++){
			//textObjectQuantity[i].text = PlayerData.Instance.inventory.GetIngredient((IngredientType)i).ToString();
			textObjectQuantity[i].text=quantity[i].ToString();
			//ingredientObjects[i] = Instantiate(ingredientPrefabs[i],refriObjParent,false);
			//ingredientObjects[i].GetComponent<RefrigeratorIngredient>().SetObjParents(refriObjParent,heldObjParent);
		}
	}

	void OnDisable(){
		RefrigeratorIngredient.OnUpdateQuantity -= OnUpdateQuantity;
	}

	void OnUpdateQuantity (IngredientType type,int value)
	{
		//PlayerData.Instance.inventory.SetIngredient(type,value);
		quantity[(int)type]+=value;
		//textObjectQuantity[(int)type].text = PlayerData.Instance.inventory.GetIngredient(type).ToString();
		textObjectQuantity[(int)type].text=quantity[(int)type].ToString();
	}
	
}
