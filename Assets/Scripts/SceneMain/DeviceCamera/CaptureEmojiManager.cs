using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
#if UNITY_IOS
using System.Runtime.InteropServices;
#endif

public class CaptureEmojiManager : MonoBehaviour {
	[Header("CaptureEmojiManager Attributes")]
	public RoomController roomController;
	public PopupShareScreenshot popupShare;
	public Collider2D[] floorToIgnoreAtStart;

	public GameObject buttonHotkey;
	public GameObject cameraFrontPrefab;
	public GameObject cameraBackPrefab;
	public GameObject watermarkObj;
	public Transform canvasTransform;

	public Button buttonCapture;
	public Button buttonSwitchCamera;
	public Button buttonBack;

	[Header("Screenshot Attributes")]
	public string fileName = "EmojiCareScreenshot.png";
	byte[] dataToSave;

	[Header("Do Not Modify")]
	[SerializeField] GameObject cameraFront;
	[SerializeField] GameObject cameraBack;

	public Texture2D tex;
	public bool isProcessing = false;
	public bool isFocus;

	#region initialization
	public void Init()
	{	
		roomController.interactable = false;
		buttonHotkey.SetActive (false);
		ShowCameraButtons (true);

		PlayerData.Instance.PlayerEmoji.triggerFall.ClearColliderList ();

		foreach (Collider2D floorCollider in floorToIgnoreAtStart)
			Physics2D.IgnoreCollision (PlayerData.Instance.PlayerEmoji.body.thisCollider, floorCollider);

		
		PlayerData.Instance.PlayerEmoji.transform.localPosition = new Vector3 (100f, 100f, -1f);

		Camera.main.transform.position = new Vector3 (100f, 100f, Camera.main.transform.position.z);

		if (AdmobManager.Instance != null)
			AdmobManager.Instance.HideBanner ();

		// turn on camera
		StartBackCamera ();
	}
	#endregion

	#region mechanics
	public void StartBackCamera()
	{
		if (cameraFront != null) cameraFront.GetComponent<DeviceCamera> ().Stop ();
		cameraFront = null;
		cameraBack = (GameObject)Instantiate (cameraBackPrefab, canvasTransform);

	}

	public void StartFrontCamera()
	{
		if (cameraBack != null)	cameraBack.GetComponent<DeviceCamera> ().Stop ();
		cameraBack = null;
		cameraFront = (GameObject)Instantiate (cameraFrontPrefab, canvasTransform);
	}

	public void BackToGame()
	{
		if (cameraFront != null) cameraFront.GetComponent<DeviceCamera> ().Stop ();
		if (cameraBack != null)	cameraBack.GetComponent<DeviceCamera> ().Stop ();

		PlayerData.Instance.PlayerEmoji.triggerFall.ClearColliderList ();
		PlayerData.Instance.PlayerEmoji.transform.localPosition = new Vector3( 0f, 0f,-1f);

		Camera.main.transform.position = new Vector3 (0f, 0f, Camera.main.transform.position.z);

		ShowWatermark (false);
		ShowCameraButtons (false);
		buttonHotkey.SetActive (true);
		roomController.interactable = true;

		PlayerData.Instance.flagDeviceCamera = false;
	}

	public void CaptureScreenshot()
	{
		StartCoroutine (CapturingScreenShot ());
	}

	IEnumerator CapturingScreenShot()
	{
		ShowWatermark (true);
		ShowCameraButtons (false);

		yield return new WaitForEndOfFrame ();

		tex = new Texture2D (Screen.width, Screen.height, TextureFormat.RGB24, false);

		tex.ReadPixels (new Rect (0, 0, Screen.width, Screen.height), 0, 0);
		tex.Apply ();
		popupShare.ShowUI (popupShare.gameObject);
	}

	public void ShareScreenShot()
	{
		if(!isProcessing){
			StartCoroutine (SharingScreenshot ());
		}
	}

	public IEnumerator SharingScreenshot()
	{
		isProcessing = true;
//		Debug.Log ("processing share");
		dataToSave = tex.EncodeToPNG ();
		string destination = Application.persistentDataPath + "/" + fileName;
		File.WriteAllBytes (destination, dataToSave);

		#if UNITY_ANDROID

//			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
//			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","content://"+destination);
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject unityContext = currentActivity.Call<AndroidJavaObject>("getApplicationContext");

			//string packageName = unityContext.Call<string>("getPackageName");
	        string authority = "com.gamelevelone.emojicare.provider";

			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction",intentClass.GetStatic<string>("ACTION_SEND"));

			int FLAG_GRANT_READ_URI_PERMISSION = intentObject.GetStatic<int>("FLAG_GRANT_READ_URI_PERMISSION");

			AndroidJavaObject fileObj = new AndroidJavaObject("java.io.File", destination);
			AndroidJavaClass fileProvider = new AndroidJavaClass("android.support.v4.content.FileProvider");

			AndroidJavaObject uri = fileProvider.CallStatic<AndroidJavaObject>("getUriForFile", unityContext, authority, fileObj);

			intentObject.Call<AndroidJavaObject>("addFlags", FLAG_GRANT_READ_URI_PERMISSION);
			intentObject.Call<AndroidJavaObject>("putExtra",intentClass.GetStatic<string>("EXTRA_STREAM"),uri);
			intentObject.Call<AndroidJavaObject>("putExtra",intentClass.GetStatic<string>("EXTRA_TEXT"),"Play EmojiCare Now!");
			intentObject.Call<AndroidJavaObject>("setType","image/png");

			currentActivity.Call("startActivity",intentObject);

//		//instantiate the class Intent
//			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
//
//			//instantiate the object Intent
//			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
//
//			//call setAction setting ACTION_SEND as parameter
//			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
//
//			//instantiate the class Uri
//			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
//
//			//instantiate the object Uri with the parse of the url's file
//			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","file://"+ destination);
//
//			//call putExtra with the uri object of the file
//			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
//
//			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "Play EmojiCare Now!");
//
//			//set the type of file
//			intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
//
//			//instantiate the class UnityPlayer
//			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//
//			//instantiate the object currentActivity
//			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
//
//			//call the activity with our Intent
//			currentActivity.Call("startActivity", intentObject);
		#elif UNITY_IOS

		ShareIOS("Play EmojiCare Now!","","",destination);

		#endif

		yield return new WaitUntil (() => isFocus);
		isProcessing = false;
	}
	#endregion


	void OnApplicationFocus(bool focus)
	{
		isFocus = focus;
	}

	#region button functions
	public void buttonBackClick()
	{
		BackToGame ();
		if (AdmobManager.Instance != null)
			AdmobManager.Instance.ShowBanner ();
	}
	public void buttonSwitchCameraClick()
	{
		if (cameraFront == null)
			StartFrontCamera ();
		else
			StartBackCamera ();
	}
	public void buttonCaptureClick()
	{
		CaptureScreenshot ();
	}
	public void ShowWatermark(bool show){
		watermarkObj.SetActive (show);
	}
	public void ShowCameraButtons(bool show){
		buttonBack.gameObject.SetActive (show);
		buttonCapture.gameObject.SetActive (show);
		buttonSwitchCamera.gameObject.SetActive (show);
	}
	public void OpenCaptureRoomFromHotkey(){
		Init ();
		buttonHotkey.transform.parent.GetComponent<HotkeysAnimation> ().CloseHotkeys ();
	}
	#endregion

	#if UNITY_IOS
	public struct ConfigStruct
	{
		public string title;
		public string message;
	}

	[DllImport ("__Internal")] private static extern void showAlertMessage(ref ConfigStruct conf);

	public struct SocialSharingStruct
	{
		public string text;
		public string subject;
		public string filePaths;
	}

	[DllImport ("__Internal")] private static extern void showSocialSharing(ref SocialSharingStruct conf);

	public static void ShareIOS(string title, string message)
	{
		ConfigStruct conf = new ConfigStruct();
		conf.title  = title;
		conf.message = message;
		showAlertMessage(ref conf);
	}

	public static void ShareIOS(string body, string subject, string url, string[] filePaths)
	{
		SocialSharingStruct conf = new SocialSharingStruct();
		conf.text = body;
		string paths = string.Join(";", filePaths);
		if (string.IsNullOrEmpty(paths))
			paths = url;
		else if (!string.IsNullOrEmpty(url))
			paths += ";" + url;
		conf.filePaths = paths;
		conf.subject = subject;

		showSocialSharing(ref conf);
	}
	#endif

}
