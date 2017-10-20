using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using SimpleJSON;

public enum EmojiExpressionState {
	DEFAULT,
	SLEEP,
	CARESSED,
	HOLD,
	WORRIED,
	AFRAID,
	DIZZY,
	HOLD_BARF,
	HUMMING,
	FALL,
	CHANGE_ROOM,
	POKED,
	ANNOYED,
	POUTING,
	AWAKE_LAZILY,
	AWAKE_NORMALLY,
	AWAKE_ENERGETICALLY,
	EATING,
	REJECT,
	BATHING,
	PLAYING_GUITAR,
	BORED,
	HAPPY,
	ANGERED,
	EATING_SADLY,
	HURT,
	CURIOUS,
	WHISTLE,
	LIKE,
	NERD,
	LANDING,
	SAD_SMILE,
	CRY,
	HEARTY,
	SOBBING,
	ANGELIC,
	SIGH,
	DEVILISH,
	MOUTHZIP,
	SURPRISED,
	SCARED,
	COOL,
	SHAME,
	STARVING,
	HUNGRY,
	FULL,
	POLLUTED,
	DIRTY,
	SPOTLESS,
	GRIEVING,
	MAD,
	SAD,
	ANGRY,
	BLISS,
	EXHAUSTED,
	TIRED,
	HYPED,
	SUFFERING,
	SICK,
	FIT
}

[System.Serializable]
public class EmojiExpression {
	#region event delegates
	public delegate void NewExpression(int newExpression);
	public static event NewExpression OnNewExpression;

	public delegate void ChangeExpression();
	public event ChangeExpression OnChangeExpression;
	#endregion

	#region attributes
	[Header("Expressions")]
	public Animator bodyAnim;
	public Animator faceAnim;
	public Animator effectAnim;
	public float expressionProgress = 0f;
	public int totalExpression = 40;
	public bool isExpressing = false;
	[Header("DON'T MODIFY THIS")]
	public float currentDuration = 0f;
	public EmojiExpressionState currentExpression = EmojiExpressionState.DEFAULT;
	public List<EmojiExpressionState> unlockedExpressions = new List<EmojiExpressionState>();
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	bool IsNewExpression(EmojiExpressionState expression)
	{
		if(unlockedExpressions.Count <= 0) return true;
		else{
			foreach(EmojiExpressionState exp in unlockedExpressions){
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

	/// <summary>
	/// <para>Duration:</para>
	/// <para>-1 = override other expressions, first priority</para>
	/// <para>0 = static expressions</para>
	/// <para>>0 = has duration, return to static expressions if duration reach 0</para>
	/// </summary>
	public void SetExpression(EmojiExpressionState expression, float duration)
	{
//		Debug.Log(expression+", "+duration+", "+currentDuration);

		//check for unlocked expression
		if(IsNewExpression(expression)){
			unlockedExpressions.Add(expression);
			SaveEmojiExpression();

			if(OnNewExpression != null) OnNewExpression((int)expression);
		}

		if(duration == -1f){ //sleep, bath, override other expressions
			Debug.Log("A");
			SetEmojiAnim((int)expression);
			currentExpression = expression;
			currentDuration = duration;

		}else if(currentDuration != -1f && duration > 0){ //non-static expression, have certain duration
			Debug.Log("B");
			SetEmojiAnim((int)expression);
			currentExpression = expression;
			currentDuration = duration;
			if(OnChangeExpression != null) OnChangeExpression();

		}else if(currentDuration == 0 && duration == 0){//static expression, like stats expression
			Debug.Log("C");
			SetEmojiAnim((int)expression);
			currentExpression = expression;
			currentDuration = duration;

		}
	}

	void SetEmojiAnim(int index)
	{
		bodyAnim.SetInteger(AnimatorParameters.Ints.EMOJI_ANIM_STATE,index);
		faceAnim.SetInteger(AnimatorParameters.Ints.EMOJI_ANIM_STATE,index);
		effectAnim.SetInteger(AnimatorParameters.Ints.EMOJI_ANIM_STATE,index);
	}


	/// <summary>
	/// Reset overrided animation to static animation
	/// </summary>
	public void ResetExpressionDuration()
	{
		currentDuration = 0f;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	

}
