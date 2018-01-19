using System.Collections;
using UnityEngine;

public class RandomPassingToyManager : MonoBehaviour {
	#region attributes
	[Header("Attribute")]
	public RoomController roomController;
	public GameObject[] toyList;

	[Header("DelayRange")]
	public float delayMin = 2f;
	public float delayMax = 4f;

	[Header("Do Not Modify")]
	public GameObject tempToyObject;
	public bool isCycling = false;
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
//		tempToyObject.GetComponent<RandomPassingToyObject>().OnFinish += Cycle;
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
		print("STOP STOP STOP");
		StopCoroutine(_CycleToys);
		if(tempToyObject){ 
			//Destroy(tempToyObject);
			tempToyObject.GetComponent<RandomPassingToyObject>().Fade();
			tempToyObject = null;
		}
	}
	#endregion
	//-------------------------------------------------------------------------------------------------------------------------------------------------	
	#region coroutines
	const string _CycleToys = "CycleToys";
	IEnumerator CycleToys()
	{
		yield return new WaitForSeconds(2f);
		SendToy();

		while(true){
			yield return new WaitForSeconds(Random.Range(delayMax,delayMax));
			SendToy();
		}

	}
	#endregion
}