using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenMiscItemsManager : MonoBehaviour {
	public GardenBird gardenBird;
	public GardenButterfly gardenButterfly;
	public GardenStork gardenStork;

	public void Init(){
		gardenBird.gameObject.SetActive (true);
		gardenButterfly.gameObject.SetActive (true);
		gardenStork.gameObject.SetActive (true);
		gardenBird.Init ();
		gardenButterfly.Init ();
		gardenStork.Init ();
	}

	public void Hide(){
		gardenBird.gameObject.SetActive (false);
		gardenButterfly.gameObject.SetActive (false);
		gardenStork.gameObject.SetActive (false);
		gardenBird.Stop ();
		gardenButterfly.Stop ();
		gardenStork.Stop ();
	}
}
