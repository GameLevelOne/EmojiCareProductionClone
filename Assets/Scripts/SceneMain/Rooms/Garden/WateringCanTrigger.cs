using System.Collections;
using UnityEngine;

public class WateringCanTrigger : MonoBehaviour {
	#region attributes
	public GameObject waterObj;
	public Transform parent;

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.SOIL){
			other.GetComponent<GardenField>().Watered();
		}
	}

	public void Water()
	{
		StartCoroutine(_StartWatering);
	}

	public void Stop()
	{
		StopCoroutine(_StartWatering);
		StartCoroutine(StopWatering());
	}

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	const string _StartWatering = "StartWatering";
	IEnumerator StartWatering()
	{
		print("WATERING");
		float t = 0;
		while (t < 1){
			parent.eulerAngles = Vector3.Lerp(Vector3.zero,new Vector3(0,0,-45f),t);
			t+=Time.deltaTime*5;
			yield return null;
		}
		parent.eulerAngles = new Vector3(0,0,-45f);
		while(true){
			Vector3 waterPosition = new Vector3(parent.position.x+0.8f,parent.position.y-1.3f,0f);
			GameObject temp = (GameObject) Instantiate(waterObj,waterPosition,Quaternion.identity);
			yield return new WaitForSeconds(0.4f);
		}
	}



	IEnumerator StopWatering()
	{
		StopCoroutine(_StartWatering);
		float t = 0;
		while (t < 1){
			parent.eulerAngles = Vector3.Lerp(new Vector3(0,0,-45f),Vector3.zero,t);
			t+=Time.deltaTime*5;
			yield return null;
		}
		parent.eulerAngles = Vector3.zero;
	}
}
