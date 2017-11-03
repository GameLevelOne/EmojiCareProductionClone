using System.Collections;
using UnityEngine;

public class WateringCan : MonoBehaviour {
	#region attributes
	Vector3 startPos;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Awake()
	{
		startPos = transform.position;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void Drag()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,19f);
		transform.position = Camera.main.ScreenToWorldPoint(tempMousePosition);
	}

	public void EndDrag()
	{
		StartCoroutine(Return());
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	IEnumerator Return()
	{
		Vector3 currentPos = transform.position;
		float t = 0;
		while(t < 1){
			transform.position = Vector3.Lerp(currentPos,startPos,t);
			t += Time.deltaTime*5;
			yield return null;
		}
		transform.position = startPos;
	}
}