using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DragCamera : MonoBehaviour {
	//constants
	const float roomWidth = 7.2f;

	float distance = 0f;
	bool snapState = false;

	public void OnBeginDrag()
	{
		if(!snapState){
			float x = getWorldPositionFromTouchInput().x;
			distance = transform.position.x - x;
		}
	}

	public void OnDrag()
	{
		if(!snapState) transform.position = new Vector3(getWorldPositionFromTouchInput().x + distance,0f,0f);
	}

	public void OnEndDrag()
	{
		Vector3 startPos = transform.position;
		Vector3 endpos = new Vector3(getXEndPosition(startPos.x),0f,0f);
		StartCoroutine(SmoothSnap(startPos,endpos));
	}

	Vector3 getWorldPositionFromTouchInput()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,10f);
		return Camera.main.ScreenToWorldPoint(tempMousePosition);
	}

	float getXEndPosition(float xPosOnEndDrag)
	{
		if(xPosOnEndDrag >= 3.6f){
			return 0;
		}else{
			float ratio = Mathf.Abs(xPosOnEndDrag) / roomWidth;
			float tenths = ratio - Mathf.Floor(ratio);

			int index = Mathf.FloorToInt(ratio);
			if(tenths > 0.5f) index++;

			return -1f * (index * roomWidth);
		}
	}


	IEnumerator SmoothSnap(Vector3 startPos, Vector3 endPos)
	{
		snapState = true;
		float t = 0;
		while(t <= 1){
			t += Time.deltaTime*6f;
			transform.position = Vector3.Lerp(startPos,endPos,Mathf.SmoothStep(0,1,Mathf.SmoothStep(0,1,t)));
			yield return new WaitForSeconds(Time.deltaTime);
		}
		transform.position = endPos;
		snapState = false;
		yield return null;
	}
}