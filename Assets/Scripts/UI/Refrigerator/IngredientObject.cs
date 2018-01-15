using System.Collections;
using UnityEngine;

public class IngredientObject : MonoBehaviour {
	public delegate void IngredientCancel(IngredientObject obj);
	public event IngredientCancel OnIngredientCancel;

	#region attributes
	public Vector2 initialPos;
	public IngredientType type;

	public bool initialized = true;

	Vector3 smallerScale = new Vector3(0.7f,0.7f,1f);
	UIBowl bowl = null;
	bool interactable = true;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.UI.BOWL && bowl == null){
			bowl = other.GetComponent<UIBowl>();
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == Tags.UI.BOWL && bowl != null){
			bowl = null;
		}
	}

	public void BeginDrag()
	{
		if(interactable){
			GetComponent<RectTransform>().SetParent(bowl.GetComponent<RectTransform>().parent,true);
		StartCoroutine(_StartHold);
		bowl.RemoveObject(this.gameObject);
		}

	}

	public void Drag()
	{
		if(interactable){
			Vector3 touchWorldPos = GetTouchWorldPosition();
		transform.position = new Vector3(touchWorldPos.x,touchWorldPos.y+20f,touchWorldPos.z);
		}
	}

	public void EndDrag()
	{
		if(interactable){
			CheckBowl();
		}
	}

	void CheckBowl()
	{
		if(bowl != null){
			if(bowl.ingredientObjects.Count >= UIBowl.MAX_INGREDIENT){
				StartCoroutine(_LerpToTarget,initialPos);
			}else{
				GetComponent<RectTransform>().SetParent(bowl.GetComponent<RectTransform>(),true);
				bowl.AddObject(this.gameObject);
				Vector2 targetPos = new Vector2(Random.Range(-100f,100f),120f);
				StartCoroutine(_LerpToTarget,targetPos);
			}
		}else{
			StartCoroutine(_LerpToTarget,initialPos);
		}
	}

	Vector3 GetTouchWorldPosition()
	{
		Vector3 touchPos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0f);
		return Camera.main.transform.InverseTransformPoint(touchPos);
	}

	public void ReturnIngredient()
	{
		PlayerData.Instance.inventory.ModIngredientValue(type,1);
		Destroy(gameObject);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	void LateUpdate()
	{
		if(initialized){
			Vector3 touchPos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0f);
			Vector3 touchWorldPos = Camera.main.transform.InverseTransformPoint(touchPos);
			transform.position = new Vector3(touchWorldPos.x,touchWorldPos.y+20f,touchWorldPos.z);

			if(Input.GetMouseButtonUp(0)){
				if(PlayerData.Instance.FirstCook == 0){
					interactable = false;
				}else if(PlayerData.Instance.FirstCook == 1){
					interactable = true;
				}

				initialized = false;
				CheckBowl();
			}
		}
	}
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region coroutine
	const string _LerpToTarget = "LerpToTarget";
	IEnumerator LerpToTarget(Vector2 targetPos)
	{
		Vector2 startPos = GetComponent<RectTransform>().anchoredPosition;
		float t = 0;
		while(t <= 1){
			GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(startPos,targetPos,t);
			GetComponent<RectTransform>().localScale = Vector3.Lerp(Vector3.one,smallerScale,t);
			t+= Time.deltaTime * 6f;
			yield return null;
		}
		if(bowl == null){
			if(OnIngredientCancel != null) OnIngredientCancel(this);
		}
	}

	const string _StartHold = "StartHold";
	IEnumerator StartHold()
	{
		float t = 0;
		while(t < 1){
			GetComponent<RectTransform>().localScale = Vector3.Lerp(smallerScale,Vector3.one,t);
			t+= Time.deltaTime * 6f;
			yield return null;
		}
		GetComponent<RectTransform>().localScale = Vector3.one;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}