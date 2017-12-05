using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidBox : BaseFurniture {
	#region attributes
	[Header("FirstAidBox Attribtues")]
	public GameObject syringe;
	public GameObject medicine;
	public GameObject doorOpen;
	public GameObject doorClosed;
	public List<GameObject> contents = new List<GameObject>();
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Start()
	{
		Close();
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void Open()
	{
		doorOpen.SetActive(true);
		medicine.SetActive(true);
		syringe.SetActive(true);
		doorClosed.SetActive(false);
		foreach(GameObject g in contents){
			g.SetActive(true);
		}
		SoundManager.Instance.PlaySFXOneShot(SFXList.OpenThings);
	}

	public void Close()
	{
		doorOpen.SetActive(false);
		medicine.SetActive(false);
		syringe.SetActive(false);
		doorClosed.SetActive(true);
		foreach(GameObject g in contents){
			g.SetActive(false);
		}
		SoundManager.Instance.PlaySFXOneShot(SFXList.OpenThings);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}