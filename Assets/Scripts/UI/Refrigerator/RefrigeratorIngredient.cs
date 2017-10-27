using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefrigeratorIngredient : MonoBehaviour {
	public delegate void UpdateQuantity(IngredientType type, int value);
	public static event UpdateQuantity OnUpdateQuantity;
	public delegate void ObjEndDrag(GameObject obj);
	public static event ObjEndDrag OnObjEndDrag;

	public IngredientType ingredientType;
	public GameObject objPrefab;
	public Transform refriObjParent;
	public Transform heldObjParent;
	public Vector3 startPos;

	GameObject instantiatedObj;
	bool enteredBowl = false;

	public void SetObjParents(Transform refri,Transform held){
		refriObjParent=refri;
		heldObjParent=held;
	}

	public void BeginDrag(){
		instantiatedObj = Instantiate(objPrefab,refriObjParent,false);
		instantiatedObj.transform.localPosition = startPos;
		instantiatedObj.name=ingredientType.ToString();
		transform.SetParent(heldObjParent,false);
		transform.localScale = new Vector3(1.1f,1.1f,1);

		OnUpdateQuantity(ingredientType,-1);
	}

	public void OnDrag(){
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0f);
		Vector3 objectPos = Camera.main.transform.InverseTransformPoint(tempMousePosition);
		transform.position = new Vector3(objectPos.x,objectPos.y+20,0);
	}

	public void EndDrag ()
	{
		if (enteredBowl) {
			StartCoroutine(MoveObjToPlate(gameObject));
		} else {
			OnUpdateQuantity(ingredientType,1);
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == Tags.UI.BOWL){
			Debug.Log("mangkok");
			enteredBowl=true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if(col.tag == Tags.UI.BOWL){
			enteredBowl=false;
		}
	}

	IEnumerator MoveObjToPlate(GameObject obj){
		obj.transform.localScale = new Vector3(0.6f,0.6f,1);
		float t = 0;
		int rand = Random.Range(-2,6)*20;
		Vector3 targetPos = new Vector3(rand,-600,0);
		while(t<1){
			obj.transform.localPosition = Vector3.Lerp(obj.transform.localPosition,targetPos,t*10);
			t+=Time.deltaTime;
			yield return null;
		}
	}
}
