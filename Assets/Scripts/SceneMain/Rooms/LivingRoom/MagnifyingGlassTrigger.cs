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
		if(parent.holding && other.tag == Tags.EMOJI_BODY){
			//show stats popup
			Debug.Log ("msukkk");
			parent.ShowStatsPopup();
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(parent.holding && other.tag == Tags.EMOJI_BODY){
			//hide stats popup
			parent.HideStatsPopup();

		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
