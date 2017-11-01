using System.Collections;
using UnityEngine;

public class Toy : TriggerableFurniture {
	#region attributes

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	protected override void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == Tags.BOX){
			
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == Tags.BOX){
			
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules


	public void EndDrag()
	{
		
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
