using System.Collections;
using UnityEngine;

public class Sponge : TriggerableFurniture {
	#region attributes
	public delegate void SpongeEvent();
	public event SpongeEvent OnSpongePicked;
	public event SpongeEvent OnSpongeReleased;

	[Header("Sponge Attributes")]
	public Animator thisAnim;
	public GameObject bubble;
	public float spongeSpeed = 1f;
	bool isBrushing = false;

	[Header("Do Not Modify")]
	public float foamState = 0;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	//animation event
	public void ChangeSprite()
	{
		thisSprite[currentVariant].sprite = variant[currentVariant].sprite[1];
	}

	//collider modules
	protected void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI_BODY){
			if(holding){
				//system
				if(foamState > 0){
					float foamValue = Time.fixedDeltaTime * spongeSpeed;
					foamState -= foamValue;
					other.GetComponent<EmojiBody>().ModEmojiFoamedValue(foamValue);
				}else{
					if(foamState < 0) foamState = 0;

					if(thisSprite[currentVariant].sprite != variant[currentVariant].sprite[0]) 
						thisSprite[currentVariant].sprite = variant[currentVariant].sprite[0];
				}

				//animation
				if(other.transform.parent.GetComponent<Emoji>().emojiExpressions.currentExpression != EmojiExpressionState.BATHING){
					other.transform.parent.GetComponent<Emoji>().emojiExpressions.SetExpression(EmojiExpressionState.BATHING,-1);
					StartCoroutine(_Bubbles);
				}
				if(SoundManager.Instance.SFXSource.clip != SoundManager.Instance.SFXClips[(int)SFXList.Sponge]){
					SoundManager.Instance.PlaySFX(SFXList.Sponge);
				}
			}
		}
	}

	public void BeginDrag()
	{
//		Debug.Log ("sponge begin drag");
		if(OnSpongePicked != null) OnSpongePicked();
	}
	public void EndDrag()
	{
		if(OnSpongeReleased != null) OnSpongeReleased();
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI_BODY){
			StopCoroutine(_Bubbles);
			isBrushing = false;

			other.transform.parent.GetComponent<Emoji>().emojiExpressions.ResetExpressionDuration();
			SoundManager.Instance.StopSFX();
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void ApplySoapLiquid()
	{
		if(foamState <= 0) PlayerData.Instance.PlayerEmoji.emojiExpressions.SetExpression(EmojiExpressionState.LIKE,2f);
		foamState = 10f;
		thisAnim.SetTrigger(AnimatorParameters.Triggers.ANIMATE);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region coroutines
	const string _Bubbles = "Bubbles";
	IEnumerator Bubbles()
	{
		if(!isBrushing){
			isBrushing = true;
			while(true){
				Instantiate(bubble,transform.position,Quaternion.identity);
				yield return new WaitForSeconds(0.25f);
			}
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}