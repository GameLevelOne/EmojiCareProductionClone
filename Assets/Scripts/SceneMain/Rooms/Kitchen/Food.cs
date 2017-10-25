using System.Collections;
using UnityEngine;

public class Food : TriggerableFurniture {
	#region attributes
	public float[] foodFactor;
	public bool hold = false;
	public Collider2D thisCollider;
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
		if(other.tag == Tags.EMOJI_BODY){
			Emoji emoji = other.transform.parent.GetComponent<Emoji>();
			float hungerValue = emoji.hunger.StatValue / emoji.hunger.MaxStatValue;

			if(hungerValue >= 0.95f){ //reject
				emoji.playerInput.Reject();
			}else{ //eat
				StopCoroutine(_Return);
				other.transform.parent.GetComponent<Emoji>().ModAllStats(foodFactor);
				emoji.playerInput.Eat();
				Destroy(this.gameObject);
			}
		}
	}


	public void BeginDrag()
	{
		thisSprite[currentVariant].sortingLayerName = SortingLayers.HELD;
		thisCollider.enabled = false;
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
		thisCollider.enabled = true;
		StartCoroutine(_Return);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	#region coroutines
	const string _Return = "Return";
	IEnumerator Return()
	{
		yield return null;
		Vector3 temp = transform.localPosition;
		float t = 0f;
		while(t<= 1f){
			transform.localPosition = Vector3.Lerp(temp,startPos,t);
			t+= Time.deltaTime * 4;
			yield return null;
		}
	}
	#endregion
}
