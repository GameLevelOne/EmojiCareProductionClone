using System.Collections;
using UnityEngine;

public class MagnifyingGlassTrigger : MonoBehaviour {
	#region attributes
	public MagnifyingGlass parent;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log (other.name);
		if(other.tag == Tags.EMOJI_BODY){
			//show stats popup
			if(parent.thisMovable.flagHolding){
//				Debug.Log ("msukkk");
				parent.ShowStatsPopup();
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI_BODY){
			if(parent.thisMovable.flagHolding){
				parent.HideStatsPopup();
			}
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
