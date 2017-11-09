using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIPlantProgress : MonoBehaviour {
	public delegate void FinishGrowingEvent();
	public event FinishGrowingEvent OnFinishGrowing;
	#region attributes
	public Image imagePlantIcon;
	public Image imageProgressBar;
	public Text textTimeLeft;
	int totalDuration;
	DateTime finishTime;
	bool ticking = false;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init(Sprite icon, DateTime finishTime)
	{
		imagePlantIcon.sprite = icon;
		this.finishTime = finishTime;
		totalDuration = (int) finishTime.Subtract(DateTime.Now).TotalSeconds;
		ticking = true;
		Show();
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	int getDuration()
	{
		TimeSpan duration = finishTime.Subtract(DateTime.Now);
		int hour = duration.Hours;
		int minute = duration.Minutes;
		int second = duration.Seconds;

		int totalSec = hour * 60 * 60 + minute * 60 + second;
		return totalSec;
	}

	string ConvertTimeToString()
	{
		TimeSpan duration = finishTime.Subtract(DateTime.Now);

		return 	duration.Hours.ToString()+"h "+
				duration.Minutes.ToString()+"m "+
				duration.Seconds.ToString()+"s";
	}

	float GetDuration()
	{
		TimeSpan deltaTime = finishTime.Subtract(DateTime.Now);
		return (float)deltaTime.TotalSeconds / (float)totalDuration;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void UpdateDuration()
	{
		if(DateTime.Now.CompareTo(finishTime) >= 0){
			ticking = false;
			if(OnFinishGrowing != null) OnFinishGrowing();
			Hide();
		}else{
			textTimeLeft.text = ConvertTimeToString();
			imageProgressBar.fillAmount = GetDuration();
		}

	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	void Update()
	{
		if(ticking) UpdateDuration();
	}
}
