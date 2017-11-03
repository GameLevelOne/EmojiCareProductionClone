using System.Collections;
using UnityEngine;

public class WateringCanTrigger : MonoBehaviour {
	#region attributes
	Transform parent;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == Tags.PLANT){
			StopAllCoroutines();
			StartCoroutine(StartWatering());
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	IEnumerator StartWatering()
	{
		float t = 0;
		while (t < 1){
			parent.eulerAngles = Vector3.Lerp(Vector3.zero,new Vector3(0,0,-45f),t);
			t+=Time.deltaTime*5;
			yield return null;
		}
		parent.eulerAngles = new Vector3(0,0,-45f);
	}

	IEnumerator StopWatering()
	{
		float t = 0;
		while (t < 1){
			parent.eulerAngles = Vector3.Lerp(new Vector3(0,0,-45f),Vector3.zero,t);
			t+=Time.deltaTime*5;
			yield return null;
		}
		parent.eulerAngles = Vector3.zero;
	}
}
