using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeviceCamera : MonoBehaviour {
	[Header("DeviceCamera Attributes")]
	public RawImage cameraRawImage;
	public AspectRatioFitter fit;

	public bool isFrontCamera;

	[Header("Do Not Modify")]
	public WebCamTexture cameraTexture;
	public bool camAvailable = false;



	void LateUpdate()
	{
		if (!camAvailable) return;

		float ratioFront = (float)cameraTexture.width / (float)cameraTexture.height;
		fit.aspectRatio = ratioFront;

		float scaleYFront = cameraTexture.videoVerticallyMirrored ? -1f : 1f;
		cameraRawImage.rectTransform.localScale = new Vector3 (1f, scaleYFront, 1f);

		int orientFront = -cameraTexture.videoRotationAngle;
		cameraRawImage.rectTransform.localEulerAngles = new Vector3 (0, 0, orientFront);
	}

	void Start()
	{
		WebCamDevice[] devices = WebCamTexture.devices;

		if(devices.Length == 0){
//			Debug.Log ("No Camera Detected");
			camAvailable = false;
			return;
		}

		for(int i = 0; i < devices.Length;i++){
			if(devices[i].isFrontFacing == isFrontCamera){
				cameraTexture = new WebCamTexture (devices [i].name, Screen.width, Screen.height);
			}
		}

		if(cameraTexture == null){
//			Debug.Log ("No Camera Found");
			return;
		}

		cameraTexture.Play ();
		cameraRawImage.texture = cameraTexture;
		camAvailable = true;
//		Debug.Log ("STATO");
	}

	public void Stop()
	{
		if(cameraTexture && cameraTexture.isPlaying) cameraTexture.Stop ();
		Resources.UnloadUnusedAssets ();
		Destroy (gameObject);
	}
}