using System.Collections;
using UnityEngine;

public class BathroomAppliances : MovableFurniture {
	public delegate void ApplyEmoji(EmojiExpressionState expression);
	public event ApplyEmoji OnApplyEmoji;
	public delegate void HoldEvent(bool flagHold);
	public event HoldEvent OnHoldEvent;

	#region attributes
	[Header("BathroomAppliances Attributes")]
	public TriggerableFurniture thisTriggerable;

	public float speed;

	protected Vector3 fixedPosition;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initializations
	void Awake()
	{
		Init();
	}
		
	protected void Init()
	{
		fixedPosition = transform.localPosition;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	protected virtual void OnTriggerEnter2D(Collider2D other)
	{

	}

	//event trigger modules
	public override void BeginDrag()
	{
		if(!flagEditMode && !endDrag){
			thisTriggerable.holding = true;
			if(thisAnim) thisAnim.SetBool(AnimatorParameters.Bools.HOLD,true);
			thisSprite.sortingLayerName = SortingLayers.HELD;

			if(OnHoldEvent != null) OnHoldEvent(true);
		}
	}

	//event trigger modules
	public override void EndDrag()
	{
		if(!flagEditMode && !endDrag){
			thisTriggerable.holding = false;
			endDrag = true;

			if(thisAnim) thisAnim.SetBool(AnimatorParameters.Bools.HOLD,false);
			thisSprite.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;

			StartCoroutine(BackToFixedPosition());
			if(OnHoldEvent != null) OnHoldEvent(false);
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
