using System.Collections;
using UnityEngine;

public class CookingSmoke : MonoBehaviour {
	public SpriteRenderer thisSprite;

	IEnumerator Start()
	{
		float t = 0f;
		while(t <= 1f){
			t += Time.deltaTime * 2;
			thisSprite.color = Color.Lerp(Color.white,new Color(1,1,1,0),t);
			transform.Translate(new Vector3(0f,0.01f,0f));
			yield return new WaitForSeconds(Time.deltaTime);
		}
		Destroy(this.gameObject);
	}
}
