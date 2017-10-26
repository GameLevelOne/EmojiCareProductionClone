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

	protected override void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI_BODY){
			if(soapLiquid.enabled == true){
				other.GetComponent<EmojiBody>().StartFoaming();
				other.transform.parent.GetComponent<Emoji>().emojiExpressions.SetExpression(EmojiExpressionState.BATHING,-1);
				StartCoroutine(_Bubbles);
			}
		}
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI_BODY){
			StopCoroutine(_Bubbles);
			other.GetComponent<EmojiBody>().StopFoaming();
			other.transform.parent.GetComponent<Emoji>().emojiExpressions.ResetExpressionDuration();
		}
	}

	const string _Bubbles = "Bubbles";
	IEnumerator Bubbles()
	{
		while(true){
			Instantiate(bubble,transform.position,Quaternion.identity);
			yield return new WaitForSeconds(0.25f);
		}
	}
}