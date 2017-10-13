using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICookBar : MonoBehaviour {
	public Image barImage;
	public float duration;

	public void UpdateBar(float current)
	{
		barImage.fillAmount = current/duration;
	}
}
