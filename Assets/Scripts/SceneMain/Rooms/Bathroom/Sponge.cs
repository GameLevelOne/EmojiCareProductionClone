using System.Collections;
using UnityEngine;

public class Sponge : TriggerableFurniture {
	[Header("Sponge Attributes")]
	public SpriteRenderer soapLiquid;
	public GameObject bubble;

	public void ApplySoapLiquid(Sprite liquidSprite)
	{
		if(soapLiquid.enabled == false) soapLiquid.enabled = true;
		soapLiquid.sprite = liquidSprite;
	}
		
	public void RemoveSoapLiquid()
	{
		soapLiquid.enabled = false;
	}

	public override void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI){
			if(soapLiquid.enabled == true){
				other.transform.parent.GetComponent<Emoji>().hygiene.ModStats(0.5f);
//				other.transform.parent.GetComponent<Emoji>().emojiExpressions.SetExpression(FaceExpression.Blushed,-1);
				StartCoroutine(Bubbles());
			}else{
//				other.transform.parent.GetComponent<Emoji>().emojiExpressions.SetExpression(FaceExpression.Upset,-1);
			}

		}
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		
		if(other.tag == Tags.EMOJI){
			StopAllCoroutines();
			other.transform.parent.GetComponent<Emoji>().emojiExpressions.ResetExpressionDuration();
		}
	}



	IEnumerator Bubbles()
	{
		while(true){
			Instantiate(bubble,transform.position,Quaternion.identity);
			yield return new WaitForSeconds(0.25f);
		}
	}
}