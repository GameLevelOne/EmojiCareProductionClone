using System.Collections;
using UnityEngine;

public class ShowerTrigger : MonoBehaviour {
	public GameObject showerWater;
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI){
			other.transform.parent.GetComponent<Emoji>().hygiene.ModStats(0.5f);
			other.transform.parent.GetComponent<Emoji>().emojiExpressions.SetExpression(FaceExpression.Blushed,false);
			StartCoroutine(ShowerWater());
		}

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI){
			StopAllCoroutines();
		}

	}

	IEnumerator ShowerWater()
	{
		while(true){
			Instantiate(showerWater,new Vector3(transform.position.x,transform.position.y-1f,transform.position.z),Quaternion.identity);
			yield return new WaitForSeconds(0.4f);
		}
	}
}
