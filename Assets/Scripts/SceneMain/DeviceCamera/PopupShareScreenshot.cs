using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupShareScreenshot : BaseUI {

	public CaptureEmojiManager captureEmojiManager;
	public RawImage image;

	public override void InitUI ()
	{
		image.texture = captureEmojiManager.tex;
	}

	public void OnClickButtonShare(){
		base.ClosePopup (this.gameObject);
		captureEmojiManager.ShareScreenShot ();
		captureEmojiManager.ShowWatermark (false);
		captureEmojiManager.ShowCameraButtons (true);
	}

	public void OnClickButtonCancel(){
		base.ClosePopup (this.gameObject);
		captureEmojiManager.ShowWatermark (false);
		captureEmojiManager.ShowCameraButtons (true);
	}
}
