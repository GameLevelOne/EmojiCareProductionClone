using System.Collections;
using UnityEngine;

public class Seed : MonoBehaviour {
	public delegate void SeedPlanted(int index);
	public event SeedPlanted OnSeedPlanted;
	#region attributes

	public IngredientType type;
	public Rigidbody2D thisRigidbody;
	public Collider2D thisCollider;
	public GameObject PlantObject;
	public int growthDuration;
	public int seedIndex;
	Vector3 startPos;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init(int idx)
	{
		startPos = transform.localPosition;
		seedIndex = idx;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.SOIL){
			int soilIndex = int.Parse(other.name);
			string tempPrefKey = other.transform.parent.GetComponent<Soil>().prefKeyHasSeed[soilIndex];
			if(PlayerPrefs.GetInt(tempPrefKey) == 0){
				other.transform.parent.GetComponent<Soil>().AddSeed(soilIndex,type,growthDuration);
				Destroy(this.gameObject);
			}
		}
	}

	public void BeginDrag()
	{
		thisRigidbody.simulated =false;
		thisCollider.enabled = false;
	}

	public void Drag()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,19f);
		transform.position = Camera.main.ScreenToWorldPoint(tempMousePosition);
	}

	public void EndDrag()
	{
		StartCoroutine(Return());	
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	IEnumerator Return()
	{
		thisCollider.enabled = true;
		thisRigidbody.simulated = true;
		yield return null;
		Vector3 currentPos = transform.localPosition;
		float t = 0;
		while(t < 1){
			transform.localPosition = Vector3.Lerp(currentPos,startPos,t);
			t+= Time.deltaTime*5f;
			yield return null;
		}
		transform.localPosition = startPos;
	}
}
