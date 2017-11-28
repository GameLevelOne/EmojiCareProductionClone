using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlow : MonoBehaviour {
	#region attributes
	[Header("WaterFlow Attributes")]
	public BoxCollider2D thisCollider;
	public GameObject fallingWater;
	public GameObject waterSplash;

	[Header("Custom Attributes")]
	public float delayPerObject = 0.2f;

	[Header("Leave Empty")]
	public List<GameObject> fallingWaterObjects = new List<GameObject>();
	public GameObject tempWaterSplash;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void OnEnable()
	{
		StartCoroutine(_WaterFlowing);
	}

	void OnDisable()
	{
		StopCoroutine(_WaterFlowing);
		foreach(GameObject g in fallingWaterObjects) Destroy(g);
		fallingWaterObjects.Clear();

	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void Splash()
	{
		if(tempWaterSplash == null){
			tempWaterSplash = (GameObject) Instantiate(waterSplash,transform);
			tempWaterSplash.transform.localPosition = new Vector3(0f,thisCollider.offset.y,0f);
		}

	}

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	#region coroutines
	const string _WaterFlowing = "WaterFlowing";
	IEnumerator WaterFlowing()
	{
		while(true){
			fallingWaterObjects.Add((GameObject)Instantiate(fallingWater,transform));
			yield return new WaitForSeconds(delayPerObject);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
