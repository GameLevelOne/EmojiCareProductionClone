using System.Collections;
using UnityEngine;

public class SyringeAnimationEvent : MonoBehaviour {
	#region attributes
	public Syringe syringe;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void OnSyringeStab()
	{
		syringe.AnimEvent_OnStabEmoji();
	}
	public void OnAnimEnd()
	{
		syringe.ResetPosition();
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
