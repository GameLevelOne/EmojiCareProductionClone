using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTutorialManager : MonoBehaviour {

	public List<GameObject> tutorialPanels = new List<GameObject>();
	int panelCount = 0;

	public void ShowPanel(){
		tutorialPanels[panelCount].SetActive(true);
		panelCount++;
	}
}
