using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DragCamera : MonoBehaviour {
	public Transform cameraTransform;

	public void OnBeginDrag()
	{
		
	}
	public void OnDrag()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);
//		print(tempMousePosition);
		cameraTransform.localPosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
		print(Camera.main.ScreenToWorldPoint(tempMousePosition));
	}
	public void OnEndDrag()
	{
		
	}
}
