using System.Collections;
using UnityEngine;

public class GachaAnimationEvent : MonoBehaviour {
	public GachaReward gachaParent;

	public void EnableTouchButton()
	{
		gachaParent.touchArea.interactable = true;
		gachaParent.buttonBack.SetActive(true);
	}
	public void DisableTouchButton()
	{
		gachaParent.touchArea.interactable = false;
		gachaParent.buttonBack.SetActive(false);
	}
	public void ShowShining()
	{
		gachaParent.shiningImage.SetActive(true);
	}
	public void HideShining()
	{
		gachaParent.shiningImage.SetActive(false);
	}
}
