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
		PlayerData.Instance.PlayerEmoji.emojiExpressions.SetExpression(expression, isExpressionStay(expression));
	}

	bool isExpressionStay(FaceExpression expression)
	{
		bool temp = false;
		switch(expression){

		case FaceExpression.Default		: temp = true; break;  	//01
		case FaceExpression.Smile		: temp = true; break;	//02
		case FaceExpression.Hungry		: temp = true; break;	//04
		case FaceExpression.Starving	: temp = true; break;	//05
		case FaceExpression.Embarrassed	: temp = true; break;	//07
		case FaceExpression.Worried		: temp = true; break;	//08
		case FaceExpression.Upset		: temp = true; break;	//10
			
		case FaceExpression.Cry			: temp = true; break;	//11
		case FaceExpression.Fidget		: temp = true; break;	//13
		case FaceExpression.Sick		: temp = true; break;	//14
		case FaceExpression.Weary		: temp = true; break;	//16
		case FaceExpression.Exhausted	: temp = true; break;	//17
		case FaceExpression.Oh			: temp = true; break;	//19
		case FaceExpression.Content		: temp = true; break;	//20
			
		case FaceExpression.Happy		: temp = true; break;	//28
		case FaceExpression.Calm		: temp = true; break;	//29
			
		case FaceExpression.Angry		: temp = true; break;	//37

		default: break;
		}

		return temp;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
