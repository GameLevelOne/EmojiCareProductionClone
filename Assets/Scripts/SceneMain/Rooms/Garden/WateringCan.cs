using System.Collections;
using UnityEngine;

public class WateringCan : MonoBehaviour {
	public delegate void WateringCanEvent();
	public event WateringCanEvent OnWateringCanPicked;
	public event WateringCanEvent OnWateringCanReleased;

	#region attributes
	public Collider2D wateringCanTrigger;
	public SpriteRenderer thisSprite;
	public Vector2 offset = new Vector2(0.5f,0.3f);
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
		thisSprite.sortingLayerName = SortingLayers.HELD;

		if(OnWateringCanPicked != null) OnWateringCanPicked();
	}

	public void Drag()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,19f);
		Vector3 newPos = Camera.main.ScreenToWorldPoint(tempMousePosition);
		transform.position = new Vector3(newPos.x+offset.x,newPos.y+offset.y,-1);
	}

	public void EndDrag()
	{
		wateringCanTrigger.gameObject.GetComponent<WateringCanTrigger>().Stop();
		thisSprite.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
		StartCoroutine(Return());

		if(OnWateringCanReleased != null) OnWateringCanReleased();
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