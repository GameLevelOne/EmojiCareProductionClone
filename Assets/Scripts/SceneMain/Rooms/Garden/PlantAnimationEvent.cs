using System.Collections;
using UnityEngine;

public class PlantAnimationEvent : MonoBehaviour {
	#region attributes
	public SpriteRenderer thisSprite;
	public Animator thisAnim;
	public Collider2D thisCollider;
	public Sprite currentSprite;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	//animation event
	public void OnAnimBegin()
	{
		thisCollider.enabled = false;
	}
	public void OnAnimEnd()
	{
		thisAnim.SetInteger(AnimatorParameters.Ints.STATE,(int)PlantState.Idle);
		thisCollider.enabled = true;
	}

	public void ChangeStage()
	{
		thisSprite.sprite = currentSprite;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void SetCurrentSprite(Sprite spr)
	{
		currentSprite = spr;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
