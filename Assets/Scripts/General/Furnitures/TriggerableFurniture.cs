using System.Collections;
using UnityEngine;

public class TriggerableFurniture : BaseFurniture {
	#region attributes
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	//triggers and collisions
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI){
			//do something
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
