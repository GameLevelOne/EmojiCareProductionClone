using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour {
	#region attributes
	public GameObject cropObject;

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void BeginDrag()
	{
		GameObject tempCrop = Instantiate(cropObject,transform.position,Quaternion.identity);
		tempCrop.GetComponent<Crop>().Init(this.gameObject);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
