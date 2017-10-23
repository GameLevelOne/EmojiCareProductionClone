using System.Collections;
using UnityEngine;

public class BathroomAppliances : MovableFurniture {
	public delegate void ApplyEmoji(EmojiExpressionState expression);
	public event ApplyEmoji OnApplyEmoji;

	#region attributes
	[Header("BathroomAppliances Attributes")]
	public float speed;

	Vector3 fixedPosition;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initializations
	void Awake()
	{
		Init();
	}
		
	void Init()
	{
		fixedPosition = transform.localPosition;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI){
//			if(OnApplyEmoji != null) OnApplyEmoji(EmojiExpressionState.Blushed);
		}
	}

	//event trigger modules
	public override void BeginDrag()
	{
		if(!flagEditMode && !endDrag){
			thisAnim.SetBool(AnimatorParameters.Bools.HOLD,true);
			thisSprite.sortingLayerName = SortingLayers.HELD;

			if(collidersToIgnore.Count != 0){
				foreach(Collider2D c in collidersToIgnore) Physics2D.IgnoreCollision(c,thisCollider,false);
			}
			collidersToIgnore.Clear();
		}
	}

	//event trigger modules
	public override void EndDrag()
	{
		if(!flagEditMode && !endDrag){
			endDrag = true;

			thisAnim.SetBool(AnimatorParameters.Bools.HOLD,false);
			thisSprite.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;

			StartCoroutine(BackToFixedPosition());
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region coroutines
	protected IEnumerator BackToFixedPosition()
	{
		yield return null;
		float t = 0;
		while(t < 1f){
			t += Time.fixedDeltaTime * speed;
			transform.localPosition = Vector3.Lerp(transform.localPosition,fixedPosition,Mathf.SmoothStep(0,1,Mathf.SmoothStep(0,1,Mathf.SmoothStep(0,1,t))));
			yield return new WaitForSeconds(Time.deltaTime);
		}
		transform.localPosition = fixedPosition;
		endDrag = false;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}
