using System.Collections;
using UnityEngine;

public class AlarmClock : ActionableFurniture {
	bool ringing = false;

	//event triggers
	public override void PointerClick()
	{
		if(!ringing){
			StartCoroutine(Ring());
		}
	}
		
	IEnumerator Ring()
	{
		ringing = true;
		Transform obj = transform.GetChild(0);
		float t = 0;
		while(t <= 1.5f){
			t += Time.deltaTime;
			obj.localPosition = new Vector3(Random.Range(-0.03f,0.03f),Random.Range(-0.01f,0.01f),0f);
			yield return new WaitForSeconds(Time.deltaTime);
		}
		obj.localPosition = Vector3.zero;
		ringing = false;
	}

	
}
