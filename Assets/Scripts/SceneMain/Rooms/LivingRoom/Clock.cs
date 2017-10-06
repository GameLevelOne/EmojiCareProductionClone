using System.Collections;
using UnityEngine;
using System;

public class Clock : BaseFurniture {
	[Header("Clock Attributes")]
	public Transform hourHand;
	public Transform minuteHand;

	const float hoursToDegrees = 360f/12f;
	const float minutesToDegrees = 360f/60f;

	IEnumerator Start()
	{
		while(true){
			TimeSpan timeSpan = DateTime.Now.TimeOfDay;
			hourHand.localRotation = Quaternion.Euler(0f,0f,(float) timeSpan.TotalHours * -hoursToDegrees);
			minuteHand.localRotation = Quaternion.Euler(0f,0f,(float) timeSpan.TotalMinutes * -minutesToDegrees);
			yield return new WaitForSeconds(1f);
		}
	}
	
}
