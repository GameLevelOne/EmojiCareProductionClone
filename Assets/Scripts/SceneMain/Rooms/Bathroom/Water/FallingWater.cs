using System.Collections;
using UnityEngine;

public class FallingWater : MonoBehaviour {
	#region attributes
	[Header("FallingWater Attributes")]
	public float fallSpeed = 0.2f;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	IEnumerator Start()
	{
		while(true){
			transform.Translate(Vector3.down * fallSpeed);
			yield return null;
		}
	}

	void OnDestroy()
	{
		transform.parent.GetComponent<WaterFlow>().fallingWaterObjects.Remove(gameObject);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.WATER_END){
			StopAllCoroutines();
			transform.parent.GetComponent<WaterFlow>().Splash();
			Destroy(gameObject);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
