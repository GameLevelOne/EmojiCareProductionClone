using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour {
	public delegate void PlantDestroyed(int index);
	public event PlantDestroyed OnPlantDestroyed;

	#region attributes
	public GameObject cropObject;
	public int soilIndex;
	public string[] prefKeyToDelete;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init(Transform parent, int soilIndex, string[] prefKeyToDelete)
	{
		transform.SetParent(parent,true);
		this.soilIndex = soilIndex;
		this.prefKeyToDelete = prefKeyToDelete;
	}

	void OnDestroy()
	{
		print("ON DESTROY CALLED > PLANT.CS");
		foreach (string s in prefKeyToDelete){
			PlayerPrefs.DeleteKey(s);
		}
		if(OnPlantDestroyed != null) OnPlantDestroyed(soilIndex);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void BeginDrag()
	{
		GameObject tempCrop = Instantiate(cropObject,transform.position,Quaternion.identity);
		tempCrop.GetComponent<Crop>().Init(this.gameObject);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
