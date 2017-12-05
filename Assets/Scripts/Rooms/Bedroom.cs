using System.Collections;
using UnityEngine;

public class Bedroom : BaseRoom {
	[Header("Bedroom Attributes")]
	public RandomBedroomObjectController randomBedroomObjectController;
	public SpriteRenderer darkLight;
	public float fadeSpeed = 3f;

	bool hasRegisterDarkLightEvent = false;

	Color brightColor = new Color(0,0,0,0);
	Color dimmedColor = new Color(0,0,0,0.5f);

	public void Init()
	{
		RegisterDarkLightEvent();
		randomBedroomObjectController.Init();
	}

	void RegisterDarkLightEvent()
	{
		if(!hasRegisterDarkLightEvent){
			hasRegisterDarkLightEvent = true;
			PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent += OnEmojiSleepEvent;
			PlayerData.Instance.PlayerEmoji.playerInput.OnEmojiWake += OnEmojiWake;

		}
	}

	void OnDestroy()
	{
		PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent -= OnEmojiSleepEvent;
	}

	void OnEmojiSleepEvent (bool sleeping)
	{
		if(sleeping){
			StartCoroutine(fadeLight(brightColor,dimmedColor));
		}
	}

	public void OnEmojiWake(float a,float b)
	{
		StartCoroutine(fadeLight(dimmedColor,brightColor));
	}

	IEnumerator fadeLight(Color currentColor, Color targetColor)
	{
		float t = 0;
		while(t < 1){
			darkLight.color = Color.Lerp(currentColor,targetColor,t);
			t+= Time.deltaTime * fadeSpeed;
			yield return null;
		}
		darkLight.color = targetColor;
	}

}
