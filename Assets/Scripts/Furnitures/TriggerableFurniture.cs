using UnityEngine;

public class TriggerableFurniture : BaseFurniture {
	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI){
			print(other.name);
		}
	}
}
