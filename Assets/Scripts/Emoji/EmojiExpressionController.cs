using System.Collections;
using UnityEngine;

public class EmojiExpressionController : MonoBehaviour {
	#region attributes
	[Header("Ref: Expression Trigger")]

	public BathroomAppliances[] bathroomTools = new BathroomAppliances[3];
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init()
	{
		PlayerData.Instance.PlayerEmoji.OnUpdateStatsToExpression += OnEmojiUpdateStats;

		foreach (BathroomAppliances b in bathroomTools) b.OnApplyEmoji += SetEmojiExpression;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void OnEmojiUpdateStats (float hunger, float hygiene, float happiness, float stamina, float health)
	{
//		EmojiExpressionState expression = EmojiExpressionState.DEFAULT;
//		const float statsTresholdMax = 0.95f;
//		const float statsTresholdHigh = 0.7f;
//		const float statsTresholdMed = 0.4f;
//		const float statsTresholdLow = 0.2f;
//
//			if(hunger 	 >= statsTresholdHigh 
//			&& hygiene 	 >= statsTresholdHigh 
//			&& happiness >= statsTresholdHigh 
//			&& stamina 	 >= statsTresholdHigh 
//			&& health 	 >= statsTresholdHigh ) expression = EmojiExpressionState.Smile;
//
//		else if(hunger 		>= statsTresholdMed 
//			 && hygiene 	>= statsTresholdMed 
//			 && happiness 	>= statsTresholdMed 
//			 && stamina 	>= statsTresholdMed 
//			 && health	 	>= statsTresholdMed ) expression = EmojiExpressionState.Default;
//		
//		else{
//				 if(health <= statsTresholdLow) expression = EmojiExpressionState.Sick;
//			else if (health < statsTresholdMed) expression = EmojiExpressionState.Fidget;
//			else{
//					 if(hunger <= statsTresholdLow) expression = EmojiExpressionState.Starving;
//				else if(hunger < statsTresholdMed) expression = EmojiExpressionState.Hungry;
//				else if(hunger >= statsTresholdMax) expression = EmojiExpressionState.Yummy;
//
//				else if(hygiene <= statsTresholdLow) expression = EmojiExpressionState.Worried;
//				else if(hygiene < statsTresholdMed) expression = EmojiExpressionState.Embarrassed;
//
//				else if(happiness <= statsTresholdLow) expression = EmojiExpressionState.Cry;
//				else if(happiness < statsTresholdMed) expression = EmojiExpressionState.Weary;
//				else if(happiness >= statsTresholdMax) expression = EmojiExpressionState.Excited;
//
//				else if(stamina <= statsTresholdLow) expression = EmojiExpressionState.Exhausted;
//				else if(stamina < statsTresholdMed) expression = EmojiExpressionState.Upset;
//				else if(stamina >= statsTresholdMax) expression = EmojiExpressionState.Energized;
//			}
//		}
//		SetEmojiExpression(expression);
	}
		
	public void SetEmojiExpression(EmojiExpressionState expression)
	{
//		PlayerData.Instance.PlayerEmoji.emojiExpressions.SetExpression(expression, getExpressionDuration(expression));
	}

	float getExpressionDuration(EmojiExpressionState expression)
	{
		float duration = 2f;
//		bool temp = false;
//		switch(expression){
//		case EmojiExpressionState.Smile: duration = 0f;break;
//		case EmojiExpressionState.DEFAULT: duration = 0f;break;
//		case EmojiExpressionState.SICK: duration = 0f;break;
//		case EmojiExpressionState.FIT: duration = 0f;break;
//		case EmojiExpressionState.STARVING: duration = 0f;break;
//		case EmojiExpressionState.HUNGRY: duration = 0f;break;
//		case EmojiExpressionState.Yummy: duration = 0f;break;
//		case EmojiExpressionState.WORRIED: duration = 0f;break;
//		case EmojiExpressionState.Embarrassed: duration = 0f;break;
//		case EmojiExpressionState.CRY: duration = 0f;break;
//		case EmojiExpressionState.Weary: duration = 0f;break;
//		case EmojiExpressionState.Excited: duration = 0f;break;
//		case EmojiExpressionState.EXHAUSTED: duration = 0f; break;
//		case EmojiExpressionState.Upset: duration = 0f; break;
//		case EmojiExpressionState.Energized: duration = 0f; break;
//		default: break;
//		}

		return duration;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
