using System.Collections;
using UnityEngine;

public class PlantedSeed : MonoBehaviour {
	#region attributes
	public GameObject plantObject;
	public IngredientType type;
	public Transform parent;
	public int soilIndex;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init(GameObject plantObject, IngredientType type, Transform parent, int soilIndex)
	{
		this.plantObject = plantObject;
		this.type = type;
		this.parent = parent;
		transform.parent = parent;
		this.soilIndex = soilIndex;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void Grow()
	{
		Vector3 pos = new Vector3(transform.position.x,transform.position.y,-1f);
		GameObject tempPlantObj = (GameObject) Instantiate(plantObject,pos,Quaternion.identity);
		tempPlantObj.GetComponent<Plant>().Init(parent,soilIndex);
		parent.parent.GetComponent<Soil>().RegisterPlantEvent(tempPlantObj.GetComponent<Plant>());
		Destroy(this.gameObject);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
