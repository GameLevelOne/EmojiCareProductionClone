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
	public delegate void NewExpression(int expressionStateIndex,bool isNewExpression);
	public static event NewExpression OnNewExpression;

	public delegate void ChangeExpression();
	public event ChangeExpression OnChangeExpression;
	#endregion

	#region attributes
	[Header("Expressions")]
	public Animator bodyAnim;
	public Animator faceAnim;
	public Animator effectAnim;
	public int totalExpressionAvailable = 60;
	public int totalExpressionForSendOff = 48;
	public float sendOffProgressThreshold = 0.8f;
	public bool isExpressing = false;
	public EmojiExpressionData[] expressionDataInstances;
	[Header("DON'T MODIFY THIS")]
	public float currentDuration = 0f;
	public EmojiExpressionState currentExpression = EmojiExpressionState.DEFAULT;
	public List<EmojiExpressionState> unlockedExpressions = new List<EmojiExpressionState>();

	const string RESOURCE_DATA = "EmojiUnlockedExpressions";
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init()
	{
		LoadEmojiExpression();
	}

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

	public float GetTotalExpressionProgress()
	{
		int counter = 0;
		foreach(EmojiExpressionData data in expressionDataInstances){
			if(data.GetProgressRatio(PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType) == 1){
				counter++;
			}
		}

		Debug.Log((float) System.Math.Round((double)counter / totalExpressionForSendOff,4));
		return (float) System.Math.Round((double)counter / totalExpressionForSendOff,4);
	}


	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void SaveEmojiExpression()
	{
		//save to json
		string data = "{\""+RESOURCE_DATA+"\":[";

		for(int i = 0;i<unlockedExpressions.Count;i++){
			data += ((int)unlockedExpressions[i]).ToString();
			if(i != unlockedExpressions.Count-1) data += ",";
		}
		data += "]}";
		PlayerPrefs.SetString(PlayerPrefKeys.Emoji.UNLOCKED_EXPRESSIONS+PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType.ToString(),data);
	}

	public void LoadEmojiExpression()
	{
		EmojiType emojiType = PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType;
		//load from json	
		if(PlayerPrefs.HasKey(PlayerPrefKeys.Emoji.UNLOCKED_EXPRESSIONS+emojiType.ToString())){
			string data = PlayerPrefs.GetString(PlayerPrefKeys.Emoji.UNLOCKED_EXPRESSIONS+emojiType.ToString());
			JSONNode node = JSON.Parse(data);
			for(int i = 0;i< node[RESOURCE_DATA].Count;i++){
				unlockedExpressions.Add((EmojiExpressionState)node[RESOURCE_DATA][i].AsInt);
			}
		}

		expressionDataInstances = new EmojiExpressionData[totalExpressionAvailable];

		for(int i=0;i<expressionDataInstances.Length;i++){
			expressionDataInstances [i] = new EmojiExpressionData (i,emojiType,
			PlayerData.Instance.PlayerEmoji.emojiBaseData.expressionNewProgress[i]); //unlock conditions
		}
	}

	/// <summary>
	/// <para>Duration:</para>
	/// <para>-1 = override other expressions, first priority</para>
	/// <para>0 = static expressions</para>
	/// <para>>0 = has duration, return to static expressions if duration reach 0</para>
	/// </summary>
	public void SetExpression (EmojiExpressionState expression, float duration)
	{
//		Debug.Log(expression+", "+duration+", "+currentDuration);
		//check for unlocked expression
//		Debug.Log("Expression =  "+expression+", duration = "+duration+", current = "+currentDuration);
//		Debug.Log(">CURRENT EXPRESSION = "+currentExpression);
//		Debug.Log("EXPRESSION = "+expression);
//		Debug.Log("CURRENT DURATION = "+currentDuration);
//		Debug.Log(">>DURATION = "+duration);
			
		if(duration == -1f){ //sleep, bath, override other expressions
//			Debug.Log("A");
			if(!PlayerData.Instance.flagDeviceCamera) UpdateExpressionProgress(expression);

			SetEmojiAnim((int)expression);
			currentExpression = expression;
			currentDuration = duration;

		}else if(currentDuration != -1f && duration > 0){ //non-static expression, have certain duration
//			Debug.Log("B");
			if(!PlayerData.Instance.flagDeviceCamera) UpdateExpressionProgress(expression);

			SetEmojiAnim((int)expression);
			currentExpression = expression;
			currentDuration = duration;
			if(OnChangeExpression != null) OnChangeExpression();

		}else if(currentDuration == 0 && duration == 0){//static expression, like stats expression
//			Debug.Log("C");
			if(!PlayerData.Instance.flagDeviceCamera) UpdateExpressionProgress(expression);

			SetEmojiAnim((int)expression);
			currentExpression = expression;
			currentDuration = duration;
		}
//		Debug.Log(">>>CURRENT DURATION = "+currentDuration);

	}

	void UpdateExpressionProgress(EmojiExpressionState expression)
	{
		if(currentExpression != expression){
			if (IsNewExpression (expression) && expression != EmojiExpressionState.DEFAULT) {
				EmojiExpressionData currentData = expressionDataInstances [(int)expression];
				currentData.AddToCurrentProgress (1);

				if(currentData.GetProgressRatio(PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType) >= 1f){
					//new expression
					unlockedExpressions.Add (expression);
					PlayerPrefs.SetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_STATUS +
					PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType.ToString () +
					expression.ToString (), (int)ExpressionStatus.Unlocked);
					SaveEmojiExpression ();

					if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == ShortCode.SCENE_GUIDED_TUTORIAL){
						if(expression == EmojiExpressionState.BLISS){
							if (OnNewExpression != null) {
								OnNewExpression ((int)expression,true);
							}
						}
					} else{
						if (OnNewExpression != null) {
							OnNewExpression ((int)expression,true);
						}
						PlayerData.Instance.PlayerEmoji.emojiGrowth.UpdateGrowth(GetTotalExpressionProgress());
					}

				}
				else{
					//notif expression progress
//					if (OnNewExpression != null) {
//						OnNewExpression ((int)expression,false);
//					}
				}
			}
		}
	}

	/// <summary>
	/// Reset overrided animation to static animation
	/// </summary>
	public void ResetExpressionDuration()
	{
		Debug.Log("ResetDuration");
		currentDuration = 0f;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	void SetEmojiAnim(int index)
	{
		bodyAnim.SetInteger(AnimatorParameters.Ints.EMOJI_ANIM_STATE,index);
		faceAnim.SetInteger(AnimatorParameters.Ints.EMOJI_ANIM_STATE,index);
		effectAnim.SetInteger(AnimatorParameters.Ints.EMOJI_ANIM_STATE,index);
	}
}
