using UnityEngine;

public class TriggerableFurniture : BaseFurniture {
	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI){
			other.transform.parent.GetComponent<Emoji>().hygiene.ModStats(1f);
		}
	}
}
