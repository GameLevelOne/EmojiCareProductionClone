using System;
using UnityEngine;
using UnityEngine.UI;

public class GardenStallTimer : MonoBehaviour {
	#region attributes
	public ScreenPopup screenPopup;
	public GardenStall stall;
	public Text textTimerSeed, textTimerItem;
	public UICoin uiCoin;

	public delegate void RefillStallWithGems(PopupEventType eventName);
	public static event RefillStallWithGems OnRefillStallWithGems;

	PopupEventType currentEvent;

	int stallRestockCost = 10;

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
		uiCoin.ShowUI (stallRestockCost, false,true,false);
		if (eventID == 1) {
			currentEvent = PopupEventType.RestockSeeds;
			screenPopup.ShowPopup (PopupType.Gems,PopupEventType.RestockSeeds,false,null,null,null,stallRestockCost);
		} else {
			currentEvent = PopupEventType.RestockStall;
			screenPopup.ShowPopup (PopupType.Gems,PopupEventType.RestockStall,false,null,null,null,stallRestockCost);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
//	public void RefillButtonWithAds ()
//	{
//		if (currentEvent == AdEvents.RestockSeeds) {
//			AdmobManager.Instance.ShowRewardedVideo (AdEvents.RestockSeeds);
//		} else if(currentEvent == AdEvents.RestockStall){
//			AdmobManager.Instance.ShowRewardedVideo (AdEvents.RestockStall);
//		}
//		screenPopup.ClosePopup (screenPopup.gameObject);
//		uiCoin.CloseUI (false);
//	}

	public void RefillButtonWithGems(){
		uiCoin.CloseUI (false);
		if(PlayerData.Instance.PlayerGem >= stallRestockCost){
			PlayerData.Instance.PlayerGem -= stallRestockCost;
			if (currentEvent == PopupEventType.RestockSeeds) {
				if (OnRefillStallWithGems != null)
					OnRefillStallWithGems (PopupEventType.RestockSeeds);
			} else if(currentEvent == PopupEventType.RestockStall){
				if (OnRefillStallWithGems != null)
					OnRefillStallWithGems (PopupEventType.RestockStall);
			}
			screenPopup.ClosePopup (screenPopup.gameObject);
		} else{
			screenPopup.ShowPopup (PopupType.Warning, PopupEventType.NotAbleToRestock);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
