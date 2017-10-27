using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EmojiTriggerFall : MonoBehaviour {
	public Collider2D bodyCollider;
	public List<Collider2D> colliderToIgnore = new List<Collider2D>();

	public bool isFalling = false;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.FLOOR || other.tag == Tags.IMMOVABLE_FURNITURE){
			if(other.isTrigger && isFalling){ 
				print("adding "+other.transform.parent.name);
				colliderToIgnore.Add(other.transform.parent.GetComponent<Collider2D>());
			}
		} 
	}

	public void ClearColliderList()
	{
		if(colliderToIgnore.Count != 0){
			foreach(Collider2D c in colliderToIgnore)
				Physics2D.IgnoreCollision(bodyCollider,c,false);
		}

		colliderToIgnore.Clear();
	}

	public void IgnoreCollision()
	{
		if(colliderToIgnore.Count != 0){
			foreach(Collider2D c in colliderToIgnore){
				print("Ignoring "+c.name);
				Physics2D.IgnoreCollision(bodyCollider,c);
			}
		}
		bodyCollider.enabled = true;
	}
}
