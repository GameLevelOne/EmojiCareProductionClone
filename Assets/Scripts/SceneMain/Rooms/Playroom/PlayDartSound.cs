using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDartSound : MonoBehaviour {

	public void PlaySound(){
		if (SoundManager.Instance)
			SoundManager.Instance.PlaySFXOneShot (SFXList.DartHitBoard);
	}
}
