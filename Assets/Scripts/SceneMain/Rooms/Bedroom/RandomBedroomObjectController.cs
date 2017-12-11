using System.Collections;
using UnityEngine;

public class RandomBedroomObjectController : MonoBehaviour {
	#region attributes
	[Header("RandomBedroomObjectController Attributes")]
	public GameObject[] RandomBedroomObjects;
	public float objectDelay;
	public float yMin, yMax;
	public bool hasInit = false;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init()
	{
		if(!hasInit){
			hasInit = true;
			PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent += OnSleepEvent;
			PlayerData.Instance.PlayerEmoji.playerInput.OnEmojiWake += OnEmojiWake;
		}
	}


	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void StartGeneratingObjects()
	{
		StartCoroutine(_GenerateRandomObjects);
	}

	void OnSleepEvent(bool sleep)
	{
		if(sleep) StartGeneratingObjects();
	}

	void OnEmojiWake ()
	{
		print("RANDOMBEDROOMOBJECT STOP GENERATE");
		StopCoroutine(_GenerateRandomObjects);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	const string _GenerateRandomObjects = "GenerateRandomObjects";
	IEnumerator GenerateRandomObjects()
	{
		while(true)
		{
			int rnd = Random.Range(0,RandomBedroomObjects.Length);
			GameObject temp = Instantiate(RandomBedroomObjects[rnd],transform);
			temp.transform.localPosition = new Vector3(temp.transform.localPosition.x,Random.Range(yMin,yMax),0f);
			yield return new WaitForSeconds(objectDelay);
		}
	}
}
