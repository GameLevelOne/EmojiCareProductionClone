using System.Collections;
using UnityEngine;

public class ShowerTrigger : MonoBehaviour {
	[Header("ShowerTrigger Attribute")]
	public BathroomAppliances thisMovable;
	public Shower shower;
	public GameObject showerWater;
	public Vector2 offset;
	bool flagWatering = false;

	public delegate void EnterShower();
	public static event EnterShower OnEnterShower;
	public delegate void ExitShower();
	public static event ExitShower OnExitShower;

	public void Init()
	{
		thisMovable.OnHoldEvent += OnHoldEvent;
	}

	void OnDestroy()
	{
		thisMovable.OnHoldEvent -= OnHoldEvent;
	}

	void OnHoldEvent (bool flagHold)
	{
		if(flagHold){
			if(flagWatering == false) StartCoroutine(_ShowerWater);
		}else{
			StopCoroutine(_ShowerWater);
			flagWatering = false;
			SoundManager.Instance.StopSFX();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == Tags.EMOJI_BODY){
			if (OnEnterShower != null)
				OnEnterShower ();
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI_BODY){
			if(shower.holding){
				if(other.transform.parent.GetComponent<Emoji>().emojiExpressions.currentExpression != EmojiExpressionState.BATHING){
					shower.ModEmojiHygiene();

					if(other.transform.parent.GetComponent<Emoji>().emojiExpressions.currentExpression != EmojiExpressionState.BATHING)
					other.transform.parent.GetComponent<Emoji>().emojiExpressions.SetExpression(EmojiExpressionState.BATHING,-1);
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI_BODY){
			shower.StopDecreasingEmojiFoamState();
			if (OnExitShower != null)
				OnExitShower ();
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
