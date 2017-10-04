using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanelType{
	Hotkey,
	Stats,
	Progress,
	Album,
	EditRoom,
	Locations,
	Shops,
	Settings
}

public class BaseUI : MonoBehaviour {

	public GameObject[] UIPanels;
	string triggerShowUI = "Show";
	string triggerCloseUI = "Close";

	public virtual void InitUI(){
		
	}

	public void ShowUI(GameObject obj){
		obj.SetActive(true);
		obj.GetComponent<Animator>().SetTrigger(triggerShowUI);	
		InitUI();
	}

	public void CloseUI(GameObject obj){
		obj.GetComponent<Animator>().SetTrigger(triggerCloseUI);
	}
}
