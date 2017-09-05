using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovableFurniture : Furniture {
	#region attributes
	List<Collider2D> floorColliders;

	Animator thisAnim;
	Rigidbody2D thisRigidbody;
	BoxCollider2D thisCollider;

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
		thisAnim = GetComponent<Animator>();
		thisRigidbody = GetComponent<Rigidbody2D>();
		thisCollider = GetComponent<BoxCollider2D>();
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	//trigger modules
	void OnTriggerEnter2D(Collider2D other)
	{
		if (endDrag && other.tag == Tags.FLOOR) floorColliders.Add(other.transform.parent.GetComponent<Collider2D>());
	}

	//event trigger modules
	public void PointerEnter()
	{
		if(!editMode){

		}
	}

	public void BeginDrag()
	{
		if(!editMode){
			thisAnim.SetBool(AnimatorParameters.Bools.HOLD,true);
			if(floorColliders.Count != 0){
				foreach(Collider2D c in floorColliders) Physics2D.IgnoreCollision(c,thisCollider,false);
				floorColliders.Clear();
			}
		}
	}

	public void Drag()
	{
		if(!editMode){
			Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,10f);
			transform.localPosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
		}
	}

	public void EndDrag()
	{
		if(!editMode){
			endDrag = true;
			thisAnim.SetBool(AnimatorParameters.Bools.HOLD,false);
			StartCoroutine(ChangeDragState());
		}
	}

	//animation event modules
	public void AnimationEventResetDrag()
	{
		if(endDrag){
			thisRigidbody.velocity = Vector2.zero;
			thisRigidbody.simulated = true;
			endDrag = false;
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region coroutines
	IEnumerator ChangeDragState()
	{
		if (endDrag) {
			if(floorColliders.Count != 0){
				foreach(Collider2D c in floorColliders) Physics2D.IgnoreCollision(c,thisCollider);
			}
		}
		yield return null;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}