using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MovableFurniture : MonoBehaviour {
	#region attributes
	[Header("MovableFurniture Attributes")]
	public Collider2D thisCollider;
	public Animator thisAnim;
	public SpriteRenderer thisSprite;
	public bool flagEditMode = false;

	[Header("leave empty for BathroomAppliances Object")] [SerializeField] private Rigidbody2D thisRigidbody;
	protected List<Collider2D> collidersToIgnore = new List<Collider2D>();
	protected bool endDrag = false;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	//collider modules
	protected void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.FLOOR || other.tag == Tags.IMMOVABLE_FURNITURE) collidersToIgnore.Add(other.transform.parent.GetComponent<Collider2D>());
	}

	//collider modules
	protected void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == Tags.MOVABLE_FURNITURE) Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),thisCollider);
	}

	//event trigger modules
	public virtual void BeginDrag()
	{
		if(!flagEditMode && !endDrag){
			thisAnim.SetBool(AnimatorParameters.Bools.HOLD,true);
			thisRigidbody.simulated = false;
			thisSprite.sortingLayerName = SortingLayers.HELD;

			if(collidersToIgnore.Count != 0){
				foreach(Collider2D c in collidersToIgnore) Physics2D.IgnoreCollision(c,thisCollider,false);
			}
			collidersToIgnore.Clear();
		}
	}

	//event trigger modules
	public void Drag()
	{
		if(!flagEditMode && !endDrag){
			Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,9f);
			transform.localPosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
		}
	}

	//event trigger modules
	public virtual void EndDrag()
	{
		if(!flagEditMode && !endDrag){
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
		if(transform.localPosition.y >= 0){ 
			thisSprite.sortingOrder = 0;
		}else{
			int sortingOrder = 0;
			float abs = Mathf.Abs(transform.localPosition.y);
			float absSisa = abs - Mathf.Floor(abs);

			if(absSisa < 0.5f) sortingOrder = Mathf.FloorToInt(abs) * 2 + 1;
			else sortingOrder = Mathf.CeilToInt(abs) * 2;

			thisSprite.sortingOrder = sortingOrder;
		}

		float zPos = Mathf.Abs(transform.localPosition.z);
		transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,-1f * (zPos+thisSprite.sortingOrder));

		thisSprite.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region coroutines
	protected IEnumerator ChangeDragState()
	{
		yield return null;
		if (endDrag) {
			if(collidersToIgnore.Count != 0){
				foreach(Collider2D c in collidersToIgnore){ 
					Physics2D.IgnoreCollision(thisCollider,c);
				}
			}
			endDrag = false;
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}