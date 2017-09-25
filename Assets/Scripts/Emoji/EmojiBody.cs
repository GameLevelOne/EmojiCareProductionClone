using System.Collections;
using UnityEngine;

public class EmojiBody : MonoBehaviour {
	public void Reset()
	{
		GetComponent<Animator>().SetInteger(AnimatorParameters.Ints.BODY_STATE,(int)BodyAnimation.Idle);
	}

}
