using System.Collections;
using UnityEngine;

public class Sponge : MovableFurniture {
	#region attributes
	public SpriteRenderer soapLiquid;
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
	public override void BeginDrag()
	{
		if(!editMode || !endDrag){
			thisAnim.SetBool(AnimatorParameters.Bools.HOLD,true);
			thisSprite.sortingOrder = 100;
			soapLiquid.sortingOrder = 101;
			thisSprite.sortingLayerName = SortingLayers.HELD;
			soapLiquid.sortingLayerName = SortingLayers.HELD;
		}
	}

	public override void EndDrag()
	{
		if(!editMode || !endDrag){
			endDrag = true;

			thisAnim.SetBool(AnimatorParameters.Bools.HOLD,false);
			thisSprite.sortingOrder = 0;
			soapLiquid.sortingOrder = 1;
			thisSprite.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
			soapLiquid.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
			soapLiquid.enabled = false;
			StartCoroutine(BackToFixedPosition());
		}
	}
	#endregion

	public void ApplySoapLiquid()
	{
		if(soapLiquid.enabled == false) soapLiquid.enabled = true;
	}

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
