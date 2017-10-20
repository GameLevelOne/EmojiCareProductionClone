using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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


public class SceneEmojiEditManager : MonoBehaviour {
	#region attributes
	public Animator bodyAnim;
	public Animator faceAnim;
	public Animator effectAnim;

	public Text currentExpLabel;
	public Transform buttonParent;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Start()
	{
		currentExpLabel.text = "00 DEFAULT";

		for (int i = 0; i < buttonParent.childCount; i++) {

			Button b = buttonParent.GetChild (i).GetComponent<Button> ();
			Text btext = b.transform.GetChild (0).GetComponent<Text> ();

			int bstate = i;
			EmojiExpressionState estate = (EmojiExpressionState)i;
			string stateName = estate.ToString ();
			if (stateName.Length > 9)
				stateName = stateName.Remove (9);
			btext.text = bstate.ToString ("00") + " - " + stateName;

			b.onClick.AddListener (() => {
				ChangeExpression(bstate);
			});
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void ChangeExpression(int state)
	{
		bodyAnim.SetInteger("ExpState",state);
		faceAnim.SetInteger("ExpState",state);
		effectAnim.SetInteger("ExpState",state);

		EmojiExpressionState estate = (EmojiExpressionState)state;
		currentExpLabel.text = state.ToString ("00") + " - " + estate.ToString();
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}