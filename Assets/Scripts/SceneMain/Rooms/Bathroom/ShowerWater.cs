using System.Collections;
using UnityEngine;

public class ShowerWater : MonoBehaviour {
	public SpriteRenderer thisSprite;
	IEnumerator Start()
	{
		float t = 0;
		while(t <= 1){
			t += Time.deltaTime;
			thisSprite.color = Color.Lerp(Color.white,new Color(1f,1f,1f,0f),t);
			transform.Translate(new Vector3(0,-0.075f,0));
			yield return new WaitForSeconds(Time.deltaTime);
		}

		Destroy(this.gameObject);
	}
}
