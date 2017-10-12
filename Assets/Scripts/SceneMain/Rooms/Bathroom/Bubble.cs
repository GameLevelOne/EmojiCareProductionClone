using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour {
	public SpriteRenderer thisSprite;
	IEnumerator Start()
	{
		float rnd = Random.value;
		transform.localScale = new Vector3(rnd,rnd,rnd);
		float t = 0;
		while(t <= 1f){
			t += Time.deltaTime;
			thisSprite.color = Color.Lerp(Color.white,new Color(1,1,1,0),t);
			transform.Translate(new Vector3(0,0.05f,0));
			yield return new WaitForSeconds(Time.deltaTime);
		}

		Destroy(this.gameObject);
	}
}
