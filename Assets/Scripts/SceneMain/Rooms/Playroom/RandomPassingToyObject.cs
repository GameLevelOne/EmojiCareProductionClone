using System.Collections;
using UnityEngine;

public class RandomPassingToyObject : MonoBehaviour {
	public delegate void Finish();
	public event Finish OnFinish;
	public SpriteRenderer thisSprite;
	#region attributes
	[Header("Custom Attributes")]
	public float speedMin;
	public float speedMax;
	public float fadeSpeed;
	float speed;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	IEnumerator Start()
	{
		gameObject.SetActive(true);

		speed = Random.Range(speedMin,speedMax);

		while(transform.localPosition.x > -4f){
			transform.Translate(Vector3.left * speed);
			yield return null;
		}

		if(OnFinish != null) OnFinish();
		Destroy(gameObject);
	}

	public void Fade()
	{
		StartCoroutine(Fading());
	}

	IEnumerator Fading()
	{
		float t = 0;
		while(t <= 1f){
			t+= Time.deltaTime*fadeSpeed;
			thisSprite.color = Color.Lerp(Color.white,Color.clear,t);
			yield return null;
		}
		thisSprite.color = Color.clear;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}
