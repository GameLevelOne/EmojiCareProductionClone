using System.Collections;
using UnityEngine;
using System;

public class Clock : ImmovableFurniture {
	public Transform hourHand, minuteHand;

	const float hoursToDegrees = 360f/12f;
	const float minutesToDegrees = 60f/12f;

//	IEnumerator Start()
//	{
//		while(true){
//			yield return new WaitForSeconds(1f);
//			DateTime time = DateTime.Now;
//			hourHand.localRotation = Quaternion.Euler(0f,0f,time.Hour * -hoursToDegrees);
//			minuteHand.localRotation = Quaternion.Euler(0f,0f,time.Minute * -minutesToDegrees);
//		}
//	}
	
}
