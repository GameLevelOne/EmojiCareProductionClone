using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PanelType{
	Hotkey,
	Stats,
	Progress,
	Album,
	EditRoom,
	Locations,
	Shops,
	Settings,
	Popup,
}

public class BaseUI : MonoBehaviour {

	public GameObject[] UIPanels = new GameObject[9];
	public HotkeysAnimation hotkeyAnim;
	string triggerShowUI = "Show";
	string triggerCloseUI = "Close";

	public virtual void InitUI(){
		
	}

	public void ShowUI (GameObject obj)
	{
		obj.SetActive(true);
		obj.GetComponent<Animator>().SetTrigger(triggerShowUI);	
		InitUI();
	}

	public void CloseUI(GameObject obj){
		obj.GetComponent<Animator>().SetTrigger(triggerCloseUI);
		StartCoroutine(WaitForAnim(obj));
	}

	public void ClosePanel(PanelType panel){
		GameObject obj = UIPanels[(int)panel];
		CloseUI(obj);
	}

	public void ShowPanelInHotkey(GameObject obj){
		hotkeyAnim.CloseHotkeys();
		ShowUI(obj);
	}

	public void ClosePanelInHotkey(GameObject obj){
		hotkeyAnim.ShowHotkeys();
		CloseUI(obj);
	}

	IEnumerator WaitForAnim(GameObject obj){
		yield return new WaitForSeconds(0.16f);
		obj.SetActive(false);
	}
}
