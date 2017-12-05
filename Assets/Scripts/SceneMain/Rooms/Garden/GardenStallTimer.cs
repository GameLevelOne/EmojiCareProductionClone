using System;
using UnityEngine;
using UnityEngine.UI;

public class GardenStallTimer : MonoBehaviour {
	#region attributes
	public ScreenPopup screenPopup;
	public GardenStall stall;
	public Text textTimerSeed, textTimerItem;

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init()
	{
		stall.OnStallSeedTick += OnStallSeedTick;
		stall.OnStallItemTick += OnStallItemTick;
	}

	void OnDestroy()
	{
		stall.OnStallSeedTick -= OnStallSeedTick;
		stall.OnStallItemTick -= OnStallItemTick;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void OnStallSeedTick (TimeSpan duration)
	{
		textTimerSeed.text = (int)duration.TotalMinutes+":"+duration.Seconds;
	}

	void OnStallItemTick (TimeSpan duration)
	{
		textTimerItem.text = (int)duration.TotalMinutes+":"+duration.Seconds;
	}

	//button module
	public void ShowPopupRefillStall (int eventID)
	{
		if (eventID == 1) {
			screenPopup.ShowPopup (PopupType.AdsOrGems, PopupEventType.RestockSeeds);
		} else {
			screenPopup.ShowPopup (PopupType.AdsOrGems, PopupEventType.RestockStall);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
