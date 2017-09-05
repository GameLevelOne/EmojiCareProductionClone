using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableToy : MonoBehaviour {

	bool endDrag = false;
	List<Collider2D> daftar = new List<Collider2D>();

	void OnTriggerEnter2D(Collider2D other)
	{
		if (endDrag && other.tag == Tags.FLOOR) daftar.Add(other.transform.parent.GetComponent<Collider2D>());
	}

	public void BeginDrag()
	{
		GetComponent<Animator>().SetBool("hold",true);
		GetComponent<Rigidbody2D>().simulated = false;

		if(daftar.Count != 0){
			foreach(Collider2D c in daftar){
				Physics2D.IgnoreCollision(c,GetComponent<Collider2D>(),false);
			}
		}

		daftar = new List<Collider2D>();
	}

	public void Drag()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,10f);
		transform.localPosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
	}

	public void EndDrag()
	{
		endDrag = true;
		GetComponent<Animator>().SetBool("hold",false);
		StartCoroutine(changeDragState());
	}


	public void OnFinishedEndDragAnimation()
	{
		if(endDrag){
			Debug.Log("-------End drag------------");


			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			GetComponent<Rigidbody2D>().simulated = true;
		}
	}

	IEnumerator changeDragState()
	{
		yield return null;
		if (endDrag) {
			Debug.Log("------change end drag state-----");
			endDrag = false;
			if(daftar.Count != 0){
				foreach(Collider2D c in daftar){
					Physics2D.IgnoreCollision(c,GetComponent<Collider2D>());
				}
			}
		}
	}
}
