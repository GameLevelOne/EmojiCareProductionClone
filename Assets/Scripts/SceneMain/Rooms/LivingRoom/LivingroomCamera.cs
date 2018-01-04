using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingroomCamera : ActionableFurniture {
	public CaptureEmojiManager captureEmojiManager;

	public override void PointerClick ()
	{
		GoToCaptureScreen ();
	}


	void GoToCaptureScreen()
	{
		PlayerData.Instance.flagDeviceCamera = true;
		captureEmojiManager.Init ();
	}

}
