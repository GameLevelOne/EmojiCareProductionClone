using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using SimpleJSON;

public enum FaceExpression{
	Default = 1,//01
	Smile,		//02
	Yummy,		//03
	Hungry,		//04
	Starving,	//05
	Blushed,	//06
	Embarrassed,//07
	Worried,	//08
	Excited,	//09
	Upset,		//10
	Cry,		//11
	Lively,		//12
	Fidget,		//13
	Sick,		//14
	Energized,	//15
	Weary,		//16
	Exhausted,	//17
	Eat,		//18
	Oh,			//19
	Content,	//20
	Eyeroll,	//21
	Whistle,	//22
	Amused,		//23
	Blessed,	//24
	Sleep,		//25
	Nerd,		//26
	Cool,		//27
	Happy,		//28
	Calm,		//29
	Hearty,		//30
	Mouthzip,	//31
	Terrified,	//32
	Kisswink,	//33
	Lick,		//34
	Overjoyed,	//35
	Glee,		//36
	Angry,		//37
	Drool,		//38
	Dizzy,		//39
	Surprised	//40
}

[System.Serializable]
public class EmojiExpression {
	#region event delegates
	public delegate void NewExpression(int newExpression);
	public static event NewExpression OnNewExpression;

	public delegate void ChangeExpression(bool expressionStay);
	public event ChangeExpression OnChangeExpression;

	public bool isExpressing = false;
	#endregion

	#region attributes
	[Header("Expressions")]
	public Animator bodyAnim;
	public Animator faceAnim;
	[Header("DON'T MODIFY THIS")]
	public FaceExpression currentExpression = FaceExpression.Default;
	public FaceExpression staticExpression;
	public List<FaceExpression> unlockedExpressions = new List<FaceExpression>();
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	bool IsNewExpression(FaceExpression expression)
	{
		if(unlockedExpressions.Count <= 0) return true;
		else{
			foreach(FaceExpression exp in unlockedExpressions){
				if(exp == expression) return false;
			}
			return true;
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void SaveEmojiExpression()
	{
		//save to json

	}

	public void LoadEmojiExpression()
	{
		//load from json	

	}

	public void SetExpression(FaceExpression expression, bool expressionStay)
	{
		//check for unlocked expression
		if(IsNewExpression(expression)){
			unlockedExpressions.Add(expression);
			SaveEmojiExpression();

			if(OnNewExpression != null) OnNewExpression((int)expression);
		}


		if(expressionStay) staticExpression = expression;
		else{ 
			isExpressing = true;
			faceAnim.SetInteger(AnimatorParameters.Ints.FACE_STATE,(int)expression);
			currentExpression = expression;
		}

		if(expression != currentExpression){
			if(!isExpressing){
				faceAnim.SetInteger(AnimatorParameters.Ints.FACE_STATE,(int)expression);
				currentExpression = expression;
			}

		}

		if(expression != staticExpression){
			if(OnChangeExpression != null) OnChangeExpression(expressionStay);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	

}
