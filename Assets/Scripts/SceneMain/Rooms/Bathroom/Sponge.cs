using System.Collections;
using UnityEngine;

public class Sponge : TriggerableFurniture {
	[Header("Sponge Attributes")]
	public SpriteRenderer soapLiquid;
	public GameObject bubble;

	[Header("Do Not Modify")]
	public float foamState = 0;

	public void ApplySoapLiquid(Sprite liquidSprite)
	{
		if(soapLiquid.enabled == false){ 
			PlayerData.Instance.PlayerEmoji.emojiExpressions.SetExpression(EmojiExpressionState.LIKE,2f);
			soapLiquid.enabled = true;
			foamState = 10f;
		}
		soapLiquid.sprite = liquidSprite;
	}

	public void RemoveSoapLiquid()
	{
		soapLiquid.enabled = false;
	}

	protected void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI_BODY){
			if(holding){
				if(soapLiquid.enabled == true){
					if(other.transform.parent.GetComponent<Emoji>().emojiExpressions.currentExpression != EmojiExpressionState.BATHING){
						other.GetComponent<EmojiBody>().StartFoaming();
						other.transform.parent.GetComponent<Emoji>().emojiExpressions.SetExpression(EmojiExpressionState.BATHING,-1);
						StartCoroutine(_Bubbles);
					}
					if(SoundManager.Instance.SFXSource.clip != SoundManager.Instance.SFXClips[(int)SFXList.Sponge]){
						SoundManager.Instance.PlaySFX(SFXList.Sponge);
					}
				}
			}

		}
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI_BODY){
			StopCoroutine(_Bubbles);
			other.GetComponent<EmojiBody>().StopFoaming();
			other.transform.parent.GetComponent<Emoji>().emojiExpressions.ResetExpressionDuration();
			SoundManager.Instance.StopSFX();
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