using System.Collections;
using UnityEngine;

public class Toy : MovableFurniture {
	#region attributes
	public float force = 5f;
	Vector3 prevPos;
	Vector3 deltaPos;
	#endregion
	//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	protected override void OnCollisionEnter2D(Collision2D other)
	{
		
		base.OnCollisionEnter2D(other);
		if(other.gameObject.tag == Tags.EMOJI){
			other.gameObject.GetComponent<EmojiPlayerInput>().OnToyBumped();
			SoundManager.Instance.PlaySFXOneShot(SFXList.Bounce);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public override void BeginDrag()
	{
		base.BeginDrag();
		if(!flagEditMode && !endDrag){
			thisRigidbody.simulated = false;


			prevPos = transform.position;
		}
	}

	public override void Drag()
	{
		base.Drag();
		if(!flagEditMode && !endDrag){
			deltaPos = new Vector3(transform.position.x - prevPos.x, transform.position.y - prevPos.y, -1);
			prevPos = transform.position;
		}
	}

	public override void EndDrag()
	{	
		if(!flagEditMode && !endDrag){
			endDrag = true;
			thisRigidbody.angularVelocity = 0f;
			thisRigidbody.velocity = Vector2.zero;
			thisRigidbody.simulated = true;
			thisCollider.enabled = true;
			thisSprite.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
			Vector2 forceDirection = new Vector3(deltaPos.x * force, deltaPos.y * force);
			print("Force = "+forceDirection);
			thisRigidbody.AddForce(forceDirection);
			StartCoroutine(ChangeDragState());
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	

}
