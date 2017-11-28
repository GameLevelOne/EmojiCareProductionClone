using System.Collections;
using UnityEngine;

public class RandomPassingToyObject : MonoBehaviour {
	public delegate void Finish();
	public event Finish OnFinish;

	#region attributes
	float speedMin;
	float speedMax;

	float speed;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void init(float speedMin, float speedMax)
	{
		this.speedMin = speedMin;
		this.speedMax = speedMax;
	}

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
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}
