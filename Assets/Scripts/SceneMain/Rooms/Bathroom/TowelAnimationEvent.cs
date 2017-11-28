using System.Collections;
using UnityEngine;

public class TowelAnimationEvent : MonoBehaviour {
	#region attributes
	public Towel towel;
	public Animator thisAnim;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void PointerDown()
	{
		thisAnim.SetBool(AnimatorParameters.Bools.DRAG,true);
	}

	public void PointerUp()
	{
		thisAnim.SetBool(AnimatorParameters.Bools.DRAG,false);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void ChangeSpriteDrag()
	{
		towel.thisSprite[towel.currentVariant].sprite = towel.variant[towel.currentVariant].sprite[1];
	}

	public void ChangeSpriteIdle()
	{
		towel.thisSprite[towel.currentVariant].sprite = towel.variant[towel.currentVariant].sprite[0];
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
