using System.Collections;
using UnityEngine;

public class RandomPassingToyManager : MonoBehaviour {
	#region attributes
	[Header("Attribute")]
	public RoomController roomController;
	public GameObject[] toyList;

	[Header("DelayRange (Random)")]
	public float delayMin = 5f;
	public float delayMax = 10f;

	[Header("ToySpeed (Random)")]
	public float toySpeedMin = 0.1f;
	public float toySpeedMax = 0.25f;

	[Header("Do Not Modify")]
	public GameObject tempToyObject;
	#endregion
	//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization

	#endregion
	//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void SendToy()
	{
		float xScale = Random.value > 0.5f ? -1f : 1f;
		transform.localScale = new Vector3(xScale,1,1);

		tempToyObject = Instantiate(toyList[Random.Range(0,toyList.Length)],transform);
		RandomPassingToyObject toyObj = tempToyObject.GetComponent<RandomPassingToyObject>();
		toyObj.init(toySpeedMin,toySpeedMax);
		toyObj.OnFinish += Cycle;
	}
	#endregion
	//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void Cycle()
	{
		
		StartCoroutine(_CycleToys);
	}

	public void Stop()
	{
		StopCoroutine(_CycleToys);
		if(tempToyObject){ 
			Destroy(tempToyObject);
			tempToyObject = null;
		}
	}
	#endregion
	//-------------------------------------------------------------------------------------------------------------------------------------------------	
	#region coroutines
	const string _CycleToys = "CycleToys";
	IEnumerator CycleToys()
	{
		yield return new WaitForSeconds(Random.Range(delayMax,delayMax));
		SendToy();
	}
	#endregion
}
