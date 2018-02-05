using System.Collections;
using UnityEngine;

public class PanAnimationEvent : MonoBehaviour {
	public Pan pan;
	public Animator thisAnim;
	public void Reset()
	{
		thisAnim.SetInteger(AnimatorParameters.Ints.STATE,(int)PanState.Idle);
	}

	public void MovePlate()
	{
		pan.MovePlate();	
	}
	public void InstantiateFood()
	{
		if(pan.isCooking)	pan.InstantiateFood();
	}
}
