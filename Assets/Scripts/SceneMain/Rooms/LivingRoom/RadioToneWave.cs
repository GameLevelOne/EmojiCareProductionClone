using System.Collections;
using UnityEngine;

public class RadioToneWave : MonoBehaviour {
	public SpriteRenderer tone,wave;

	public void ChangeToneAndWaveColor()
	{
		tone.color = new Color(Random.value,Random.value,Random.value);
		wave.color = new Color(Random.value,Random.value,Random.value);
	}
}
