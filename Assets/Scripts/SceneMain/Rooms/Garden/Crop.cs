using System.Collections;
using UnityEngine;

public class Crop : MonoBehaviour {
	#region attributes
	public Rigidbody2D thisRigidbody;
	public Collider2D thisCollider;
	public SpriteRenderer thisSprite;
	public IngredientType type;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
//	void Start()
//	{
//		thisRigidbody.AddForce(new Vector2(0,10000f));
//	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.BASKET){
			PlayerData.Instance.inventory.ModIngredientValue(type,1);
			other.transform.parent.GetComponent<Basket>().Animate();
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == Tags.CROP){
			Physics2D.IgnoreCollision(thisCollider,other.collider);
		}
	}

	public void BeginDrag()
	{
		thisRigidbody.simulated = false;
		thisCollider.enabled = false;
		thisSprite.sortingLayerName = SortingLayers.HELD;
	}
	public void Drag()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,19f);
		transform.localPosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
	}
	public void EndDrag()
	{
		thisRigidbody.simulated = true;
		thisCollider.enabled = true;
		thisSprite.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	void OnApplicationQuit()
	{
		PlayerData.Instance.inventory.ModIngredientValue(type,1);
	}
}
