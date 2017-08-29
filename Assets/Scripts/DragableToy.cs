using System.Collections;
using UnityEngine;

public class DragableToy : MonoBehaviour {

	public void BeginDrag()
	{
		GetComponent<Animator>().SetBool("hold",true);
		GetComponent<CircleCollider2D>().enabled = false;
		GetComponent<Rigidbody2D>().simulated = false;
	}

	public void Drag()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,10f);
		transform.localPosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
	}

	public void EndDrag()
	{
		GetComponent<CircleCollider2D>().enabled = true;
		GetComponent<Rigidbody2D>().simulated = true;
		GetComponent<Animator>().SetBool("hold",false);
	}
}
