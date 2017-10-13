using System.Collections;
using UnityEngine;

public class Food : TriggerableFurniture {
	#region attributes
	public float[] foodFactor;
	public bool hold = false;
	Vector3 startPos;

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Start()
	{
		startPos = transform.localPosition;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public override void OnTriggerEnter2D(Collider2D other)
	{
		print(other.name);
		if(other.tag == Tags.EMOJI){
			StopAllCoroutines();
			other.transform.parent.GetComponent<Emoji>().emojiExpressions.SetExpression(FaceExpression.Eat,2f);
			other.transform.parent.GetComponent<Emoji>().ModAllStats(foodFactor);
			Destroy(this.gameObject);
		}
	}


	public void BeginDrag()
	{
		thisSprite[currentVariant].sortingLayerName = SortingLayers.HELD;
		hold = true;
	}

	public void Drag()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,9f);
		transform.localPosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
	}

	public void EndDrag()
	{
		thisSprite[currentVariant].sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
		hold = false;
		StartCoroutine(CheckEmoji());
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	#region coroutines
	IEnumerator CheckEmoji()
	{
		yield return null;
		Vector3 temp = transform.localPosition;
		float t = 0f;
		while(t<= 1f){
			transform.localPosition = Vector3.Lerp(temp,startPos,t);
			t+= Time.deltaTime * 4;
			yield return new WaitForSeconds(Time.deltaTime);
		}

	}
	#endregion
}
