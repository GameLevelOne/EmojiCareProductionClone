using System.Collections;
using UnityEngine;

public class ShowerTrigger : MonoBehaviour {
	public Shower shower;
	public GameObject showerWater;
	public Vector2 offset;
	bool flagWatering = false;
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI_BODY){
			if(shower.holding){
				if(other.transform.parent.GetComponent<Emoji>().emojiExpressions.currentExpression != EmojiExpressionState.BATHING){
					float hygieneMod = other.GetComponent<EmojiBody>().foamState;
					other.transform.parent.GetComponent<Emoji>().hygiene.statsModifier = hygieneMod;
					other.transform.parent.GetComponent<Emoji>().emojiExpressions.SetExpression(EmojiExpressionState.BATHING,-1);
					if(flagWatering == false) StartCoroutine(_ShowerWater);

				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI_BODY){
			StopCoroutine(_ShowerWater);
			other.GetComponent<EmojiBody>().foamState = 1f;
			other.transform.parent.GetComponent<Emoji>().ResetEmojiStatsModifier();
			other.transform.parent.GetComponent<Emoji>().emojiExpressions.ResetExpressionDuration();
			flagWatering = false;
			SoundManager.Instance.StopSFX();
		}
	}

	const string _ShowerWater = "ShowerWater";
	IEnumerator ShowerWater()
	{
		flagWatering = true;
		SoundManager.Instance.PlaySFX(SFXList.Shower);
		while(true){
			Instantiate(showerWater,new Vector3(transform.position.x+offset.x,transform.position.y+offset.y,transform.position.z),Quaternion.identity);
			yield return new WaitForSeconds(0.4f);
		}
	}
}
