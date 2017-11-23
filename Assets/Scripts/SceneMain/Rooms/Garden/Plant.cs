using System.Collections;
using UnityEngine;

public enum PlantState{
	Idle = 0,
	Animate,
	Grow
}

public class Plant : MonoBehaviour {
	public delegate void PlantDestroyed(int index);
	public event PlantDestroyed OnPlantDestroyed;
	#region attributes
	[Header("NEW!")]
	public PlantSO plantSO;
	public SpriteRenderer thisSprite;
	public Animator thisAnim;
	public PlantAnimationEvent plantAnimationEvent;

	public bool flagHarvest = false;
	public int currentStage = -1;
	public int plantLocationIndex;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Start()
	{
		currentStage = 0;
	}

	public void Init(int locationIndex)
	{
		plantLocationIndex = locationIndex;
	}

	///<summary>
	///<para> 0% - 29% = 0</para>
	///<para>30% - 69% = 1</para>
	///<para>70% - 99% = 2</para>
	///<para>100% = 3 (harvest)</para>
	/// </summary>
	public void UpdatePlantStage(int durationInSeconds)
	{
		int totalDuration = plantSO.GrowTime * 60;
		int timeLeft = totalDuration - durationInSeconds;
		float ratio = ((float)timeLeft / (float)totalDuration);

		if(ratio >= 0 && ratio < 0.3f){//stage 0
			currentStage = 0 ;
		}else if(ratio >= 0.3f && ratio < 0.7f){//stage 1
			if(currentStage != 1) {
				currentStage = 1 ;
				Animate(); 
			}
		}else if(ratio >= 0.7f && ratio < 1f){//stage 2
			if(currentStage != 2){
				currentStage = 2 ;
				Animate();
			}
		}else{
			if(currentStage != 3){//harvest
				currentStage = 3 ;
				Animate();
			}
			flagHarvest = true;
		}
	}

	void Animate()
	{
		plantAnimationEvent.SetCurrentSprite(plantSO.plantStages[currentStage]);
		thisAnim.SetInteger(AnimatorParameters.Ints.STATE,(int)PlantState.Grow);
	}

	void DestroyPlant()
	{
		if(OnPlantDestroyed != null) OnPlantDestroyed(plantLocationIndex);
		Destroy(gameObject);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void PointerClick()
	{
		if(!flagHarvest){
			if (thisAnim.GetInteger(AnimatorParameters.Ints.STATE)!=1)
				thisAnim.SetInteger(AnimatorParameters.Ints.STATE,(int)PlantState.Animate);
		}else{
			GameObject crop = (GameObject) Instantiate(plantSO.cropObject,transform.parent);
			crop.transform.localPosition = new Vector3(0,0,-1f);
			crop.transform.SetParent(transform.parent.parent.parent.parent);
			crop.GetComponent<Crop>().thisRigidbody.AddForce(new Vector2(0,200f));
			DestroyPlant();
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
