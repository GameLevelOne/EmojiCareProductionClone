using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefrigeratorIngredient : MonoBehaviour {
	public Text textQuantity;
	public GameObject objPrefab;
	public Transform objParent;

	public int quantity = 99;

	GameObject instantiatedObj;
	bool enteredBowl = false;

	void OnEnable(){
		textQuantity.text = quantity.ToString();
		RefrigeratorIngredientTrigger.OnEnterBowl += OnEnterBowl;
	}

	void OnDisable(){
		RefrigeratorIngredientTrigger.OnEnterBowl -= OnEnterBowl;
	}

	void OnEnterBowl (){
		enteredBowl=true;
	}

	public void BeginDrag(){
		instantiatedObj = Instantiate(objPrefab,objParent,false);
		instantiatedObj.transform.localScale = new Vector3(1.1f,1.1f,1);
	
		UpdateQuantity(false);
	}

	public void OnDrag(){
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0f);
		instantiatedObj.transform.position = Camera.main.transform.InverseTransformPoint(tempMousePosition);
	}

	public void EndDrag(){
		if(enteredBowl){
			UpdateQuantity(false);
			instantiatedObj.transform.localScale = new Vector3(0.6f,0.6f,1);
			StartCoroutine(MoveObjToPlate(instantiatedObj.transform.localPosition));
		} else{
			UpdateQuantity(true);
			Destroy(instantiatedObj);
		}
	}

	void UpdateQuantity (bool isPositive)
	{
		if (isPositive) {
			quantity++;
		} else {
			quantity--;
		}
		textQuantity.text = quantity.ToString();
	}

	IEnumerator MoveObjToPlate(Vector3 currentPos){
		float t = 0;
		int rand = Random.Range(-2,6)*20;
		Vector3 targetPos = new Vector3(rand,-600,0);
		while(t<1){
			instantiatedObj.transform.localPosition = Vector3.Lerp(currentPos,targetPos,t*10);
			t+=Time.deltaTime;
			yield return null;
		}
	}
}
