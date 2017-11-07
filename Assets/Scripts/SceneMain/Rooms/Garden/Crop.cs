using System.Collections;
using UnityEngine;

public class Crop : MonoBehaviour {
	#region attributes
	public bool flagHolding = true;
	public GameObject plantObject;
	public Rigidbody2D thisRigidbody;
	public Collider2D thisCollider;
	public IngredientType type;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Awake()
	{
		thisRigidbody.simulated = false;
		thisCollider.enabled = false;
	}

	public void Init(GameObject self)
	{
		plantObject = self;
	}

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.BASKET){
			StopAllCoroutines();
			PlayerData.Instance.inventory.ModIngredientValue(type,1);
			Destroy(plantObject);
			Destroy(this.gameObject);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	void Update()
	{
		if(flagHolding){
			Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,19f);
			transform.localPosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
			if(Input.GetMouseButtonUp(0)){
				flagHolding = false;
				StartCoroutine(Destroy());
			}
		}
	}

	IEnumerator Destroy()
	{
		thisRigidbody.simulated = true;
		thisCollider.enabled = true;
		yield return null;
		yield return null;
		Destroy(this.gameObject);
	}
}
