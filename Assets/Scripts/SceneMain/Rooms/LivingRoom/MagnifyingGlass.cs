using System.Collections;
using UnityEngine;

public class MagnifyingGlass : TriggerableFurniture {
	#region attributes
	[Header("MagnifyingGlass Attributes")]
	public FloatingStatsManager popupStats;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	protected override void OnTriggerEnter2D (Collider2D other)
	{
		base.OnTriggerEnter2D (other);
		if(holding && other.tag == Tags.EMOJI_BODY){
				//show stats popup

		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI_BODY){
				//hide stats popup

		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
