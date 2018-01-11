using System.Collections;
using UnityEngine;

public class Crop : MonoBehaviour {
	#region attributes
	public delegate void CropDestroyed(GameObject selfObject);
	public event CropDestroyed OnCropDestroyed;
	public delegate void StallItemHarvested();
	public static event StallItemHarvested OnStallItemHarvested;
	public delegate void CropEvent();
	public static event CropEvent OnCropPicked;
	public static event CropEvent OnCropReturned;

	public Rigidbody2D thisRigidbody;
	public Collider2D thisCollider;
	public SpriteRenderer thisSprite;
	public IngredientType type;
	#endregion

//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Start()
	{
		CropHolder.Instance.AddCrop(gameObject);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.BASKET){
			PlayerData.Instance.inventory.ModIngredientValue(type,1);
			other.transform.parent.GetComponent<Basket>().Animate();
			if(OnCropDestroyed != null){
				OnCropDestroyed(gameObject);
			}

			if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == ShortCode.SCENE_GUIDED_TUTORIAL){
				if (OnStallItemHarvested != null)
					OnStallItemHarvested ();
			}

			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == Tags.CROP){
			Physics2D.IgnoreCollision(thisCollider,other.gameObject.GetComponent<Crop>().thisCollider,true);
		}
		if(other.gameObject.tag == Tags.EMOJI_BODY){
			Physics2D.IgnoreCollision(thisCollider,other.gameObject.GetComponent<EmojiBody>().thisCollider,true);
		}
	}

	public void BeginDrag()
	{
		thisRigidbody.simulated = false;
		thisCollider.enabled = false;
		thisSprite.sortingLayerName = SortingLayers.HELD;

		if (OnCropPicked != null)
			OnCropPicked ();
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

		if (OnCropReturned != null)
			OnCropReturned ();
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
