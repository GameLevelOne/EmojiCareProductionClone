using System.Collections;
using UnityEngine;

public class ShowerTrigger : MonoBehaviour {
	public GameObject showerWater;
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI_BODY){
			float hygieneMod = other.GetComponent<EmojiBody>().foamState;
			other.transform.parent.GetComponent<Emoji>().hygiene.statsModifier = hygieneMod;
			other.transform.parent.GetComponent<Emoji>().emojiExpressions.SetExpression(EmojiExpressionState.BATHING,-1);
			StartCoroutine(_ShowerWater);
		}

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI_BODY){
			StopCoroutine(_ShowerWater);
			other.GetComponent<EmojiBody>().foamState = 1f;
			other.transform.parent.GetComponent<Emoji>().ResetEmojiStatsModifier();
			other.transform.parent.GetComponent<Emoji>().emojiExpressions.ResetExpressionDuration();
		}

	}

	const string _ShowerWater = "ShowerWater";
	IEnumerator ShowerWater()
	{
		while(true){
			Instantiate(showerWater,new Vector3(transform.position.x,transform.position.y-1f,transform.position.z),Quaternion.identity);
			yield return new WaitForSeconds(0.4f);
		}
	}
}
