using System.Collections;
using UnityEngine;

public class Toy : MovableFurniture {
	#region attributes
	public float force = 10f;
	Vector3 prevPos;
	Vector3 deltaPos;
	#endregion
	//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	protected override void OnTriggerEnter2D (Collider2D other)
	{
		base.OnTriggerEnter2D(other);
		if(other.tag == Tags.BOX){
			
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == Tags.BOX){
			
		}
	}

	protected override void OnCollisionEnter2D(Collision2D other)
	{
		base.OnCollisionEnter2D(other);
		if(other.gameObject.tag == Tags.EMOJI_BODY){
			
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public override void BeginDrag()
	{
		base.BeginDrag();
		if(!flagEditMode && !endDrag){
			thisRigidbody.angularVelocity = 0f;
			prevPos = transform.position;
		}
	}

	public override void Drag()
	{
		base.Drag();
		if(!flagEditMode && !endDrag){
			deltaPos = new Vector3(transform.position.x - prevPos.x, transform.position.y - prevPos.y, -1);
			print(deltaPos);
			prevPos = transform.position;
		}
	}

	public override void EndDrag()
	{	
		base.EndDrag();
		if(!flagEditMode && !endDrag){
			Vector3 tempDirection = deltaPos.normalized;
			print("FORCE = "+tempDirection);
			thisRigidbody.AddForce(tempDirection * force);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
