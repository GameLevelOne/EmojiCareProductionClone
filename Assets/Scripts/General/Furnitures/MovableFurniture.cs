using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MovableFurniture : Furniture {
	#region attributes
	List<Collider2D> floorColliders = new List<Collider2D>();

	Animator thisAnim;
	Rigidbody2D thisRigidbody;
	Collider2D thisCollider;
	protected SpriteRenderer thisSprite;

	bool endDrag = false;

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initializations
	void Awake()
	{
		Init();
	}

	void Init()
	{
		thisAnim = transform.GetChild(0).GetComponent<Animator>();
		thisSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
		thisRigidbody = GetComponent<Rigidbody2D>();
		thisCollider = GetComponent<Collider2D>();
		
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics

	//collider modules
	void OnTriggerEnter2D(Collider2D other)
	{
//		print("Trigger: "+other.name);
		if(other.tag == Tags.FLOOR || other.tag == Tags.IMMOVABLE_FURNITURE) {
			floorColliders.Add(other.transform.parent.GetComponent<Collider2D>());
		}
	}

	void OnCollisionEnter2D(Collision2D other){
//		print("Collider: "+other.gameObject.name);
		if(other.gameObject.tag == Tags.MOVABLE_FURNITURE){
			Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),thisCollider);
		}
	}

	//event trigger modules
	public void BeginDrag()
	{
		if(!editMode || !endDrag){
			thisAnim.SetBool(AnimatorParameters.Bools.HOLD,true);
			thisRigidbody.simulated = false;
			thisSprite.sortingOrder = 100;

			if(floorColliders.Count != 0){
				foreach(Collider2D c in floorColliders) Physics2D.IgnoreCollision(c,thisCollider,false);
			}
			floorColliders.Clear();
		}
	}

	public void Drag()
	{
		if(!editMode || !endDrag){
			Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,9f);
			transform.localPosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
		}
	}

	public void EndDrag()
	{
		if(!editMode || !endDrag){
			endDrag = true;

			thisAnim.SetBool(AnimatorParameters.Bools.HOLD,false);
			thisRigidbody.velocity = Vector2.zero;
			thisRigidbody.simulated = true;
			AdjustSortingOrder();

			StartCoroutine(ChangeDragState());
		}
	}

	protected virtual void AdjustSortingOrder()
	{
		if(transform.localPosition.y >= 0) thisSprite.sortingOrder = 0;
		else{
			int sortingOrder = 0;
			float abs = Mathf.Abs(transform.localPosition.y);
			float absSisa = abs - Mathf.Floor(abs);

			if(absSisa < 0.5f) sortingOrder = Mathf.FloorToInt(abs) * 2 + 1;
			else sortingOrder = Mathf.CeilToInt(abs) * 2;

			thisSprite.sortingOrder = sortingOrder;
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region coroutines
	IEnumerator ChangeDragState()
	{
		yield return null;
		if (endDrag) {
			if(floorColliders.Count != 0){
				foreach(Collider2D c in floorColliders){ 
					Physics2D.IgnoreCollision(c,thisCollider);
				}
			}
			endDrag = false;
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}