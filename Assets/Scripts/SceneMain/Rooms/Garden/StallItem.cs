using System.Collections;
using UnityEngine;

public class StallItem : MonoBehaviour {
	public delegate void GoodHarvested(StallItem item);
	public event GoodHarvested OnItemPicked;
	#region attributes
	[Header("Reference")]
	public Rigidbody2D thisRigidBody;
	public Collider2D thisCollider;
	public SpriteRenderer thisSprite;
	public Animator thisAnim;

	[Header("Custom Attribute")]
	public IngredientType type;
	public int price;

	[Header("Do Not Modify")]
	public int itemIndex;
	public bool inStall = true;
	Vector3 startPos;
	#endregion

	#region events
	public delegate void DragStallItem(int price);
	public static event DragStallItem OnDragStallItem;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init(int index)
	{
		startPos = transform.localPosition;
		itemIndex = index;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	//colliders
	void OnTriggerEnter2D(Collider2D other)
	{
		if(!inStall){
			if(other.tag == Tags.BASKET){
				StopAllCoroutines();
				PlayerData.Instance.inventory.ModIngredientValue(type,1);

				if(OnItemPicked != null) OnItemPicked(this);

				Destroy(this.gameObject);
			}
		}
	}

	//event triggers
	public void BeginDrag()
	{
		inStall = false;
		thisSprite.sortingLayerName = SortingLayers.HELD;
		thisAnim.SetBool(AnimatorParameters.Bools.HOLD,true);
		thisRigidBody.simulated = false;
		thisCollider.enabled = false;
		if(OnDragStallItem!=null){
			OnDragStallItem (price);
		}
	}

	public void Drag()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,19f);
		transform.position = Camera.main.ScreenToWorldPoint(tempMousePosition);
	}

	public void EndDrag()
	{
		StartCoroutine(Return());
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	IEnumerator Return()
	{
		thisSprite.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
		thisAnim.SetBool(AnimatorParameters.Bools.HOLD,false);
		thisRigidBody.simulated = true;
		thisCollider.enabled = true;

		yield return null;

		float t = 0;
		Vector3 currentPos = transform.localPosition;
		while (t < 1){
			transform.localPosition = Vector3.Lerp(currentPos,startPos,t);
			t+= Time.deltaTime*5;
			yield return null;
		}
		transform.localPosition = startPos;
		inStall = true;
	}
}
