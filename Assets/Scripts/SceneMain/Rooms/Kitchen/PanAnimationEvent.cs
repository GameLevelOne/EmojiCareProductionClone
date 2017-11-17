using System.Collections;
using UnityEngine;

public class PanAnimationEvent : MonoBehaviour {
	public Animator thisAnim;
	public void Reset()
	{
		thisAnim.SetInteger(AnimatorParameters.Ints.STATE,(int)PanState.Idle);
	}
}
