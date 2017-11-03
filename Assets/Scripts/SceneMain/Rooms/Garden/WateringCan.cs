using System.Collections;
using UnityEngine;

public class WateringCan : MonoBehaviour {
	#region attributes
	public Collider2D wateringCanTrigger;
	Vector3 startPos;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Awake()
	{
		startPos = transform.localPosition;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void BeginDrag()
	{
		wateringCanTrigger.enabled = true;
		wateringCanTrigger.gameObject.GetComponent<WateringCanTrigger>().Water();
	}

	public void Drag()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,19f);
		transform.position = Camera.main.ScreenToWorldPoint(tempMousePosition);
	}

	public void EndDrag()
	{
		wateringCanTrigger.gameObject.GetComponent<WateringCanTrigger>().Stop();
		StartCoroutine(Return());
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	IEnumerator Return()
	{
		wateringCanTrigger.enabled = false;
		Vector3 currentPos = transform.localPosition;
		float t = 0;
		while(t < 1){
			transform.localPosition = Vector3.Lerp(currentPos,startPos,t);
			t += Time.deltaTime*5;
			yield return null;
		}
		transform.localPosition = startPos;
	}
}