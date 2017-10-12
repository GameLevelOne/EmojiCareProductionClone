using System.Collections;
using UnityEngine;

public class Sponge : TriggerableFurniture {
	[Header("Sponge Attributes")]
	public SpriteRenderer soapLiquid;
	public GameObject bubble;
	public EmojiExpressionController expressionController;

	public void ApplySoapLiquid(Sprite liquidSprite)
	{
		if(soapLiquid.enabled == false) soapLiquid.enabled = true;
		soapLiquid.sprite = liquidSprite;
	}
		
	public override void OnTriggerEnter2D(Collider2D other)
	{
		base.OnTriggerEnter2D(other);
		if(other.tag == Tags.EMOJI){
			expressionController.SetEmojiExpression(FaceExpression.Blushed);
			StartCoroutine(Bubbles());
		}
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		
		if(other.tag == Tags.EMOJI){
			StopAllCoroutines();
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