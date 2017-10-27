using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTutorial : MonoBehaviour {
	public string[] firstVisit;
	public string[] idleLivingRoom;
	public string[] firstBedroom;
	public string[] firstBathroom;
	public string[] firstKitchen;
	public string[] firstPlayroom;
	public string[] firstGarden;
	public string[] firstProgressUI;
	public string[] firstEditRoomsUI;

	public string[] triggerHungerRed;
	public string[] triggerHygieneRed;
	public string[] triggerHappinessRed;
	public string[] triggerStaminaRed;
	public string[] triggerHealthOrange;
	public string[] triggerHealthRed;
	public string[] triggerFirstExpressionFull;
	public string[] triggerFirstDead;

	public List<GameObject> firstTutorialPanels = new List<GameObject>();
	int panelCount = 0;

	public void ShowTutorialPanels ()
	{
		if (panelCount < firstTutorialPanels.Count) {
			firstTutorialPanels [panelCount].SetActive (true);
			panelCount++;
		}
		if(panelCount == firstTutorialPanels.Count){
			PlayerData.Instance.PlayerFirstPlay = 1;
		}

	}
}
