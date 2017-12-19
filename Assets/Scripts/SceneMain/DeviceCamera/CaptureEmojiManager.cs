using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

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
		Debug.Log ("back to game");
		if (cameraFront != null) cameraFront.GetComponent<DeviceCamera> ().Stop ();
		if (cameraBack != null)	cameraBack.GetComponent<DeviceCamera> ().Stop ();

		PlayerData.Instance.PlayerEmoji.triggerFall.ClearColliderList ();
		PlayerData.Instance.PlayerEmoji.transform.localPosition = new Vector3( 0f, 0f,-1f);

		Camera.main.transform.position = new Vector3 (0f, 0f, Camera.main.transform.position.z);

		ShowWatermark (false);
		ShowCameraButtons (false);
		buttonHotkey.SetActive (true);
		roomController.interactable = true;
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
		Debug.Log ("processing share");
		dataToSave = tex.EncodeToPNG ();
		string destination = Application.persistentDataPath + "/" + fileName;
		File.WriteAllBytes (destination, dataToSave);

		#if UNITY_ANDROID

//			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
//			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","content://"+destination);
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject unityContext = currentActivity.Call<AndroidJavaObject>("getApplicationContext");

			string packageName = unityContext.Call<string>("getPackageName");
	        string authority = packageName + ".provider";

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
}
