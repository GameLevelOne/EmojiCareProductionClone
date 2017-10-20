using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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