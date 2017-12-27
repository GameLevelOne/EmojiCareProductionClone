using System;
using UnityEngine;
using UnityEngine.UI;

public class GardenStallTimer : MonoBehaviour {
	#region attributes
	public ScreenPopup screenPopup;
	public GardenStall stall;
	public Text textTimerSeed, textTimerItem;
	public UICoin uiCoin;

	public delegate void RefillStallWithGems(AdEvents eventName);
	public static event RefillStallWithGems OnRefillStallWithGems;

	AdEvents currentEvent;

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
		textTimerSeed.text = ((int)duration.TotalMinutes).ToString("00")+":"+duration.Seconds.ToString("00");
	}

	void OnStallItemTick (TimeSpan duration)
	{
		textTimerItem.text = ((int)duration.TotalMinutes).ToString("00")+":"+duration.Seconds.ToString("00");
	}

	//button module
	public void ShowPopupRefillStall (int eventID)
	{
		uiCoin.ShowUI (100, false,true,false);
		if (eventID == 1) {
			currentEvent = AdEvents.RestockSeeds;
			screenPopup.ShowPopup (PopupType.AdsOrGems,PopupEventType.RestockSeeds);
		} else {
			currentEvent = AdEvents.RestockStall;
			screenPopup.ShowPopup (PopupType.AdsOrGems,PopupEventType.RestockStall);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void RefillButtonWithAds ()
	{
		if (currentEvent == AdEvents.RestockSeeds) {
			AdmobManager.Instance.ShowRewardedVideo (AdEvents.RestockSeeds);
		} else if(currentEvent == AdEvents.RestockStall){
			AdmobManager.Instance.ShowRewardedVideo (AdEvents.RestockStall);
		}
		screenPopup.ClosePopup (screenPopup.gameObject);
	}

	public void RefillButtonWithGems(){
		uiCoin.CloseUI (false);
		screenPopup.ClosePopup (screenPopup.gameObject);
		if(PlayerData.Instance.PlayerGem >= 100){
			PlayerData.Instance.PlayerGem -= 100;
			if (currentEvent == AdEvents.RestockSeeds) {
				if (OnRefillStallWithGems != null)
					OnRefillStallWithGems (AdEvents.RestockSeeds);
			} else if(currentEvent == AdEvents.RestockStall){
				if (OnRefillStallWithGems != null)
					OnRefillStallWithGems (AdEvents.RestockStall);
			}
		} else{
			screenPopup.ShowPopup (PopupType.Warning, PopupEventType.NotAbleToRestock);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
