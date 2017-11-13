using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationNewExpression : MonoBehaviour {
	public Transform parentFrame;
	Image expressionImage;
	Image progressBar;
	Image progressBarFill;
	Text expressionNameText;
	Text unlockDetailsText;
	Text progressText;
	Button continueButton;
	ParticlePlayer particles;
	bool isNewExpression = false;

	List<GameObject> notificationObj = new List<GameObject>();

	string triggerOpenNotif = "OpenNotif";
	string triggerCloseNotif = "CloseNotif";

	public void ShowUI (int expression, ExpressionIcons expressionIcons, ParticlePlayer particlePlayer, bool isNewExpression)
	{
		unlockDetailsText.gameObject.SetActive (false);
		EmojiType currentEmoji = PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType;
		EmojiExpression emojiExpression = PlayerData.Instance.PlayerEmoji.emojiExpressions;
		EmojiExpressionData currentData = emojiExpression.expressionDataInstances [expression];
		int expressionCurrentProgress = currentData.GetCurrentProgress ();
		int expressionTotalProgress = currentData.GetTotalProgress ();
		particles = particlePlayer;
		this.isNewExpression = isNewExpression;

		SetNotificationProgressReferences ();

		expressionImage.sprite = expressionIcons.GetExpressionIcon (currentEmoji, expression);
		expressionNameText.text = expressionIcons.GetExpressionName (currentEmoji, expression);

		if (isNewExpression) {
			unlockDetailsText.text = expressionIcons.GetExpressionUnlockCondition (currentEmoji, expression);
		} else{
			progressText.text = expressionCurrentProgress.ToString () + "/" + expressionTotalProgress.ToString ();
		}

		if (isNewExpression) {
			emojiExpression.expressionProgress = ((float)emojiExpression.unlockedExpressions.Count / (float)emojiExpression.totalExpression) * 100;
		} 

		ShowNotification (expressionCurrentProgress,expressionTotalProgress);
	}

	public void OnClickContinue(){
		particles.StopParticles();
		StopCoroutine("AutoCloseNotif");
		StartCoroutine(WaitForAnim());
	}

	public void AddNotifToList(GameObject obj){
		//notificationObj.Add (obj);
		notificationObj.Insert (0, obj);
	}

	void SetNotificationNewExpressionReferences(){
		expressionImage = parentFrame.GetChild(1).GetComponent<Image>();
		expressionNameText = parentFrame.GetChild(2).GetComponent<Text>();
		unlockDetailsText = parentFrame.GetChild(3).GetComponent<Text>();
		continueButton = parentFrame.GetChild(4).GetComponent<Button>();
		continueButton.onClick.AddListener(OnClickContinue);
	}

	void SetNotificationProgressReferences(){
		expressionImage = parentFrame.GetChild(1).GetComponent<Image>();
		expressionNameText = parentFrame.GetChild(2).GetComponent<Text>();
		continueButton = parentFrame.GetChild(3).GetComponent<Button>();
		progressBar = parentFrame.GetChild (4).GetComponent<Image> ();
		progressBarFill = parentFrame.GetChild (5).GetComponent<Image> ();
		progressText = parentFrame.GetChild (6).GetComponent<Text> ();
		unlockDetailsText = parentFrame.GetChild (7).GetComponent<Text> ();
		continueButton.onClick.AddListener(OnClickContinue);
	}

	void ShowNotification (float currentProgress, float totalProgress)
	{
		gameObject.SetActive (true);
		GetComponent<Animator> ().SetTrigger (triggerOpenNotif);
		float time = 2 + (notificationObj.Count - 1);

		progressBarFill = parentFrame.GetChild (5).GetComponent<Image> ();
		progressBarFill.fillAmount = (currentProgress - 1) / totalProgress;
		StartCoroutine (AnimateProgressBar (currentProgress, totalProgress));

		if (isNewExpression) {
			particles.ShowParticles ();
		} else {
			StartCoroutine (AutoCloseNotif (time));
		}
	}

	IEnumerator WaitForAnim(){
		GetComponent<Animator>().SetTrigger(triggerCloseNotif);
		particles.StopParticles();
		yield return new WaitForSeconds(0.16f);
		Destroy(gameObject);
	}

	IEnumerator AutoCloseNotif(float time=2){
		yield return new WaitForSeconds(time);
		particles.StopParticles();
		GetComponent<Animator>().SetTrigger(triggerCloseNotif);
		yield return new WaitForSeconds(0.16f);
		Destroy(this.gameObject);
	}

	IEnumerator AnimateProgressBar(float currentProgress,float totalProgress){
		yield return new WaitForSeconds (1f);
		float time = 0;
		float startValue = (currentProgress - 1) / totalProgress;
		float endValue = currentProgress / totalProgress;
		while(progressBarFill.fillAmount < endValue){
			progressBarFill.fillAmount = Mathf.Lerp (startValue, endValue, time);
			time += Time.deltaTime * 2;
			yield return null;
		}
		time = 0;
		if(isNewExpression){
			StartCoroutine (NewExpressionSequence ());
		}
	}

	IEnumerator NewExpressionSequence(){
		float time = 0;
		while(time<1){
			progressBar.color = Color.Lerp (Color.white, Color.clear, time);
			progressBarFill.color = Color.Lerp (Color.white, Color.clear, time);
			time += Time.deltaTime * 2;
			yield return null;
		}
		unlockDetailsText.gameObject.SetActive (true);
		StartCoroutine (AutoCloseNotif ());
	}
}
