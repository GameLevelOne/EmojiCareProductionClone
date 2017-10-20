using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTutorialManager : MonoBehaviour {

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
