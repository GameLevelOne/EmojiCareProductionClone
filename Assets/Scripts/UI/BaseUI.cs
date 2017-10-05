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

public enum PanelCelebrationType{
	NewEmoji = 0,
	NewExpression,
	EmojiSentOff,
	EmojiDead,
	EmojiTransferred
}

public class BaseUI : MonoBehaviour {

	public GameObject[] UIPanels;
	public GameObject[] UICelebrationPanels = new GameObject[5];
	string triggerShowUI = "Show";
	string triggerCloseUI = "Close";

	public virtual void InitUI(){
		
	}

	public void ShowUI (GameObject obj)
	{
		if (SceneManager.GetActiveScene().name == "SceneMain") {
			AdmobManager.Instance.HideBanner ();
		}
		obj.SetActive(true);
		obj.GetComponent<Animator>().SetTrigger(triggerShowUI);	
		InitUI();
	}

	public void CloseUI(GameObject obj){
		obj.GetComponent<Animator>().SetTrigger(triggerCloseUI);
		StartCoroutine(WaitForAnim(obj));
	}

	IEnumerator WaitForAnim(GameObject obj){
		yield return new WaitForSeconds(0.16f);
		obj.SetActive(false);
		if (SceneManager.GetActiveScene().name == "SceneMain") {
			AdmobManager.Instance.ShowBanner ();
		}
	}
}
