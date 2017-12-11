using System.Collections.Generic;
using UnityEngine;

public class CropHolder : MonoBehaviour {
	#region attributes
	static CropHolder instance;
	public static CropHolder Instance{
		get{return instance;}
	}
	public List<GameObject> cropObjects = new List<GameObject>();
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Awake()
	{
		instance = this;
	}

	public void AddCrop(GameObject cropObject)
	{
		cropObject.GetComponent<Crop>().OnCropDestroyed += OnCropDestroyed;
		print("REGISTERED");
		cropObjects.Add(cropObject);
	}

	void OnCropDestroyed (GameObject cropObject)
	{
		cropObject.GetComponent<Crop>().OnCropDestroyed -= OnCropDestroyed;
		cropObjects.Remove(cropObject);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void HideCrops()
	{
		foreach(GameObject g in cropObjects) g.SetActive(false);
	}

	public void ShowCrops()
	{
		foreach(GameObject g in cropObjects) g.SetActive(true);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
