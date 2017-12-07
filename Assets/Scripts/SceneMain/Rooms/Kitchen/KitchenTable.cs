using System.Collections;
using UnityEngine;

public class KitchenTable : ActionableFurniture {
	#region attributes
	[Header("Table Attribute")]

	public GameObject[] Objects;
	public bool isOpened = false;

	Sprite spriteClose;
	Sprite spriteOpen;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public override void InitVariant ()
	{
		base.InitVariant ();
		spriteClose = variant[currentVariant].sprite[1];
		spriteOpen = variant[currentVariant].sprite[2];
		thisSprite[1].sprite = spriteClose;
		isOpened = false;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public override void PointerClick ()
	{
		if(!isOpened){
			StartCoroutine(LiftCurtain());
		}
	}

	IEnumerator LiftCurtain()
	{
		isOpened = true;
		//open table curtain
		thisSprite[1].sprite = spriteOpen;

		//randomize object
		int rnd = Random.Range(0,Objects.Length);
		Objects[rnd].SetActive(true);
		yield return new WaitForSeconds(3f);

		thisSprite[1].sprite = spriteClose;

		Objects[rnd].SetActive(false);
		isOpened = false;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public override void SetCurrentVariant ()
	{
		spriteClose = variant[currentVariant].sprite[1];
		spriteOpen = variant[currentVariant].sprite[2];
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
