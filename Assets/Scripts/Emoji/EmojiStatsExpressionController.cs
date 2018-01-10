using System.Collections;
using UnityEngine;

public struct ExpressionData{
	
}

public class EmojiStatsExpressionController : MonoBehaviour {
	#region attributes
	const float statsTresholdHigh = 0.9f;
	const float statsTresholdMed = 0.4f;
	const float statsTresholdLow = 0.2f;


	public EmojiExpressionState[] lowState;
	public EmojiExpressionState[] mediumState;
	public EmojiExpressionState[] highState;
	[SerializeField] Emoji emoji;

	bool hasInit = false;
	#endregion
	//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init()
	{
		emoji = PlayerData.Instance.PlayerEmoji;
	}

	public void RegisterEmojiEvents()
	{
		print("REGISTERED");
		emoji.OnUpdateStatsToExpression += OnEmojiUpdateStats;
	}

	public void UnregisterEmojiEvents()
	{
		emoji.OnUpdateStatsToExpression -= OnEmojiUpdateStats;
	}

	void OnDestroy()
	{
		if(PlayerData.Instance.PlayerEmoji) UnregisterEmojiEvents();
	}
	#endregion
	//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void OnEmojiUpdateStats (float hungerValue, float hygieneValue, float happinessValue, float staminaValue, float healthValue)
	{
		//print("EH");
		float[] stats = new float[]{hungerValue,hygieneValue,happinessValue,staminaValue,healthValue};
		float tempLowestValue = Mathf.Min(stats);
		if(tempLowestValue < statsTresholdMed){
			for(int i = 0;i<stats.Length;i++){
				if(tempLowestValue == stats[i]){
					SetEmojiExpression(i,tempLowestValue);
					return;
				}
			}
		}else{
			float tempHighestValue = Mathf.Max(stats);
			if(tempHighestValue >= statsTresholdHigh){
				for(int i = 0;i<stats.Length;i++){
					if(tempHighestValue == stats[i]){
						SetEmojiExpression(i,tempHighestValue);
						return;
					}
				}
			}else{
				if(emoji.emojiExpressions.currentExpression != EmojiExpressionState.DEFAULT){
					emoji.emojiExpressions.SetExpression(EmojiExpressionState.DEFAULT,0);
				}
			}

		}
	}

	void SetEmojiExpression(int index, float value)
	{
		if(value < statsTresholdLow){
			if(emoji.emojiExpressions.currentExpression != lowState[index]){
				emoji.emojiExpressions.SetExpression(lowState[index],0);
			}
		}else if(value >= statsTresholdLow && value < statsTresholdMed){
			if(emoji.emojiExpressions.currentExpression != mediumState[index]){
				emoji.emojiExpressions.SetExpression(mediumState[index],0);
			}
		}else if (value >= statsTresholdHigh){
			if(emoji.emojiExpressions.currentExpression != highState[index]){
				emoji.emojiExpressions.SetExpression(highState[index],0);
			}
		}
	}
	#endregion
}
