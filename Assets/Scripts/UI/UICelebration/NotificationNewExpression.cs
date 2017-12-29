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

	string boolShowNotif = "ShowNotif";

	public void ShowUI (int expression, ExpressionIcons expressionIcons, ParticlePlayer particlePlayer, bool isNewExpression)
	{
		
		EmojiType currentEmoji = PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType;
		EmojiExpression emojiExpression = PlayerData.Instance.PlayerEmoji.emojiExpressions;
		EmojiExpressionData currentData = emojiExpression.expressionDataInstances [expression];

		particles = particlePlayer;
		this.isNewExpression = isNewExpression;

		SetNotificationProgressReferences ();

		expressionImage.sprite = expressionIcons.GetExpressionIcon (currentEmoji, expression);
		expressionNameText.text = expressionIcons.GetExpressionName (currentEmoji, expression);
//		progressText.text = expressionCurrentProgress.ToString () + "/" + expressionTotalProgress.ToString ();

		if(isNewExpression)
			ShowNotification ();
	}

	public void OnClickContinue(){
		particles.StopParticles();
		//StopCoroutine("AutoCloseNotif");
		StartCoroutine(WaitForAnim());
	}

	public void AddNotifToList(GameObject obj){
		//notificationObj.Add (obj);
		notificationObj.Insert (0, obj);
	}

	void SetNotificationProgressReferences(){
		expressionImage = parentFrame.GetChild(1).GetComponent<Image>();
		expressionNameText = parentFrame.GetChild(2).GetComponent<Text>();
		continueButton = parentFrame.GetChild(3).GetComponent<Button>();
//		progressBar = parentFrame.GetChild (4).GetComponent<Image> ();
//		progressBarFill = parentFrame.GetChild (5).GetComponent<Image> ();
//		progressText = parentFrame.GetChild (6).GetComponent<Text> ();
//		continueButton.onClick.AddListener( () => OnClickContinue());
	}

	void ShowNotification ()
	{
		gameObject.SetActive (true);
		GetComponent<Animator> ().SetBool (boolShowNotif,true);
		float time = 2 + (notificationObj.Count - 1);

//		progressBarFill = parentFrame.GetChild (5).GetComponent<Image> ();
//		progressBarFill.fillAmount = (currentProgress - 1) / totalProgress;
//		StartCoroutine (AnimateProgressBar (currentProgress, totalProgress));

		particles.ShowParticleConfetti();

		StartCoroutine (AutoCloseNotif ());
	}

	IEnumerator WaitForAnim(){
		GetComponent<Animator>().SetBool(boolShowNotif,false);
		particles.StopParticles();
		yield return new WaitForSeconds(40f/60f);

		Destroy(gameObject);
	}

	IEnumerator AutoCloseNotif(float time=2){
		yield return new WaitForSeconds(time);
//		particles.StopParticles();
		particles.StopParticleConfettiAndStarBoom();
		GetComponent<Animator>().SetBool(boolShowNotif,false);
		yield return new WaitForSeconds(40f/60f);

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
			particles.ShowParticleStarBoom();
			StartCoroutine (NewExpressionSequence ());
			yield return new WaitForSeconds(3f);
		}

		StartCoroutine (AutoCloseNotif ());
	}

	IEnumerator NewExpressionSequence(){
		float time = 0;
		while(time<1){
			progressBar.color = Color.Lerp (Color.white, Color.clear, time);
			progressBarFill.color = Color.Lerp (Color.white, Color.clear, time);
			time += Time.deltaTime * 2;
			yield return null;
		}
		//unlockDetailsText.gameObject.SetActive (true);
		StartCoroutine (AutoCloseNotif ());
	}
}
