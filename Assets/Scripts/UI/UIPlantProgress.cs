using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIPlantProgress : MonoBehaviour {
	#region attributes
	public Image imagePlantIcon;
	public Image imageProgressBar;
	public Text textTimeLeft;
	int totalTime;
	DateTime finishTime;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init(Image icon, DateTime finishTime)
	{
		this.imagePlantIcon = icon;
		this.finishTime = finishTime;
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
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void UpdateData()
	{
		if(!gameObject.activeSelf) gameObject.SetActive(true);
		textTimeLeft.text = ConvertTimeToString();
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	#region coroutine
	const string _TickTime = "TickTime";
	IEnumerator TickTime()
	{
		while(DateTime.Now.CompareTo(finishTime) < 0){
			textTimeLeft.text = ConvertTimeToString();
			yield return new WaitForSeconds(1f);
		}

		Hide();
	}
	#endregion
}
