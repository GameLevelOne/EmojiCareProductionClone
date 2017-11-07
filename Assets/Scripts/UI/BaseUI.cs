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
	string boolShowUI = "Show";
	//string boolCloseUI = "Close";

	public virtual void InitUI(){
		
	}

	public void ShowUI (GameObject obj)
	{
		SoundManager.Instance.PlaySFXOneShot (SFXList.OpenThings);
		if(AdmobManager.Instance) AdmobManager.Instance.HideBanner();
		obj.SetActive(true);
		obj.GetComponent<Animator>().SetBool(boolShowUI,true);	
		InitUI();
	}

	public void CloseUI(GameObject obj){
		if(AdmobManager.Instance) AdmobManager.Instance.ShowBanner();
		obj.GetComponent<Animator>().SetBool(boolShowUI,false);
		StartCoroutine(WaitForAnim(obj));
	}

	public void ClosePanel(PanelType panel){
		GameObject obj = UIPanels[(int)panel];
		CloseUI(obj);
	}

	public void ShowPanelInHotkey(GameObject obj){
		if(AdmobManager.Instance) AdmobManager.Instance.HideBanner();
		hotkeyAnim.CloseHotkeys();
		ShowUI(obj);
	}

	public void ClosePanelInHotkey(GameObject obj){
		CloseUI(obj);
		hotkeyAnim.ShowHotkeys();
	}

	IEnumerator WaitForAnim(GameObject obj){
		yield return new WaitForSeconds(0.16f);
		obj.SetActive(false);
	}
}
