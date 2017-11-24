using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FieldPost : MonoBehaviour {
	#region attributes
	public Text textTimer;
	public GardenField field;
	#endregion
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
		textTimer.text = (int)duration.TotalMinutes+":"+duration.Seconds;
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

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	

}
