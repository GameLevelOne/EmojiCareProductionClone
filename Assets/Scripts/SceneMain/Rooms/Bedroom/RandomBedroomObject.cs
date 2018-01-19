using System.Collections;
using UnityEngine;

public class RandomBedroomObject : MonoBehaviour {
	#region attributes
	public SpriteRenderer thisSprite;
	public float speedMin, speedMax;
	public float rotateMin, rotateMax;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	IEnumerator Start()
	{
		StartCoroutine(RandomColor());

		//position
		Vector3 randomRotation = new Vector3(0,0,Random.Range(rotateMin,rotateMax));

		//rotation
		Quaternion tempRotation = thisSprite.transform.localRotation;
		tempRotation.eulerAngles = new Vector3(0,0,Random.Range(0,360f));
		thisSprite.transform.localRotation = tempRotation;

		//scale
		float randomScale = Random.Range(1f,1.75f);
		thisSprite.transform.localScale = new Vector3(randomScale,randomScale,1f);

		float speed = Random.Range(speedMin,speedMax);
		while(true){
			transform.Translate(Vector3.right * speed);
			thisSprite.transform.Rotate(randomRotation);

			if(transform.localPosition.x >= 5f) break;

			yield return null;
		}
		Destroy(gameObject);
	}

	IEnumerator RandomColor()
	{
		while(true){
			thisSprite.color = new Color(Random.value,Random.value,Random.value,0.35f);
			yield return new WaitForSeconds(Random.Range(0.5f,2f));
		}

	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
