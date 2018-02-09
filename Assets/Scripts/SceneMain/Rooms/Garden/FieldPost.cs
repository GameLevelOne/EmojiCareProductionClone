using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FieldPost : MonoBehaviour {
	#region attributes
	public Text textTimer;
	public GardenField field;
	public ScreenPopup popup;
	public UICoin uiCoin;
	#endregion

	int instantHarvestCost = 20;
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void OnEnable()
	{
		field.OnTimerTick += OnTimerTick;
	}
	void OnDisable()
	{
		field.OnTimerTick -= OnTimerTick;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void OnTimerTick (TimeSpan duration)
	{
//		print(duration.TotalMinutes+":"+duration.Seconds);
		textTimer.text = ((int)duration.TotalMinutes).ToString("00")+":"+duration.Seconds.ToString("00");
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void Show()
	{
		gameObject.SetActive(true);
		textTimer.gameObject.SetActive(true);
	}
	public void Hide()
	{
		gameObject.SetActive(false);
		textTimer.gameObject.SetActive(false);
	}
	public void OnClickPost ()
	{
		if (field.isUnlocked()) {
			uiCoin.ShowUI (instantHarvestCost, false,true,false);
			popup.ShowPopup (PopupType.AdsOrGems, PopupEventType.SpeedUpPlant,false,null,null,null,instantHarvestCost);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	

}
