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
		FaceExpression expression = FaceExpression.Default;
		const float statsTresholdMax = 0.95f;
		const float statsTresholdHigh = 0.7f;
		const float statsTresholdMed = 0.4f;
		const float statsTresholdLow = 0.2f;

			if(hunger 	 >= statsTresholdHigh 
			&& hygiene 	 >= statsTresholdHigh 
			&& happiness >= statsTresholdHigh 
			&& stamina 	 >= statsTresholdHigh 
			&& health 	 >= statsTresholdHigh ) expression = FaceExpression.Smile;

		else if(hunger 		>= statsTresholdMed 
			 && hygiene 	>= statsTresholdMed 
			 && happiness 	>= statsTresholdMed 
			 && stamina 	>= statsTresholdMed 
			 && health	 	>= statsTresholdMed ) expression = FaceExpression.Default;
		
		else{
				 if(health <= statsTresholdLow) expression = FaceExpression.Sick;
			else if (health < statsTresholdMed) expression = FaceExpression.Fidget;
			else{
					 if(hunger <= statsTresholdLow) expression = FaceExpression.Starving;
				else if(hunger < statsTresholdMed) expression = FaceExpression.Hungry;
				else if(hunger >= statsTresholdMax) expression = FaceExpression.Yummy;

				else if(hygiene <= statsTresholdLow) expression = FaceExpression.Worried;
				else if(hygiene < statsTresholdMed) expression = FaceExpression.Embarrassed;

				else if(happiness <= statsTresholdLow) expression = FaceExpression.Cry;
				else if(happiness < statsTresholdMed) expression = FaceExpression.Weary;
				else if(happiness >= statsTresholdMax) expression = FaceExpression.Excited;

				else if(stamina <= statsTresholdLow) expression = FaceExpression.Exhausted;
				else if(stamina < statsTresholdMed) expression = FaceExpression.Upset;
				else if(stamina >= statsTresholdMax) expression = FaceExpression.Energized;
			}
		}
		SetEmojiExpression(expression);
	}
		
	public void SetEmojiExpression(FaceExpression expression)
	{
		PlayerData.Instance.PlayerEmoji.emojiExpressions.SetExpression(expression, getExpressionDuration(expression));
	}

	float getExpressionDuration(FaceExpression expression)
	{
		float duration = 2f;
		bool temp = false;
		switch(expression){
		case FaceExpression.Smile: duration = 0f;break;
		case FaceExpression.Default: duration = 0f;break;
		case FaceExpression.Sick: duration = 0f;break;
		case FaceExpression.Fidget: duration = 0f;break;
		case FaceExpression.Starving: duration = 0f;break;
		case FaceExpression.Hungry: duration = 0f;break;
		case FaceExpression.Yummy: duration = 0f;break;
		case FaceExpression.Worried: duration = 0f;break;
		case FaceExpression.Embarrassed: duration = 0f;break;
		case FaceExpression.Cry: duration = 0f;break;
		case FaceExpression.Weary: duration = 0f;break;
		case FaceExpression.Excited: duration = 0f;break;
		case FaceExpression.Exhausted: duration = 0f; break;
		case FaceExpression.Upset: duration = 0f; break;
		case FaceExpression.Energized: duration = 0f; break;
		default: break;
		}

		return duration;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
