using System.Collections;
using UnityEngine;

public class Towel : MovableFurniture {
	#region attributes
	public float speed;
	Vector3 fixedPosition;
	#endregion

	protected override void Init()
	{
		if(transform.GetChild(0).GetComponent<Animator>() != null) thisAnim = transform.GetChild(0).GetComponent<Animator>();
		if(transform.GetChild(0).GetComponent<SpriteRenderer>() != null) thisSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
		if(GetComponent<Collider2D>() != null) thisCollider = GetComponent<Collider2D>();
		fixedPosition = transform.localPosition;
	}

	#region mechanics
	//collider triggers
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI){
			//do something
		}
	}

	//event trigger
	public override void BeginDrag()
	{
		if(!editMode || !endDrag){
			thisAnim.SetBool(AnimatorParameters.Bools.HOLD,true);
			thisSprite.sortingOrder = 100;
		}
	}

	public override void EndDrag()
	{
		if(!editMode || !endDrag){
			endDrag = true;

			thisAnim.SetBool(AnimatorParameters.Bools.HOLD,false);
			thisSprite.sortingOrder = 0;
			StartCoroutine(BackToFixedPosition());
		}
	}
	#endregion

	IEnumerator BackToFixedPosition()
	{
		yield return null;
		float t = 0;
		while(t < 1f){
			t += Time.fixedDeltaTime * speed;
			transform.localPosition = Vector3.Lerp(transform.localPosition,fixedPosition,Mathf.SmoothStep(0,1,Mathf.SmoothStep(0,1,Mathf.SmoothStep(0,1,t))));
			yield return new WaitForSeconds(Time.deltaTime);
		}
		print(t);
		transform.localPosition = fixedPosition;
		endDrag = false;
	}     
}
