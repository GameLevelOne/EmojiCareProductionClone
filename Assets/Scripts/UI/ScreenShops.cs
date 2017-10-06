using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShops : BaseUI {

	public ScreenPopup screenPopup;

	public void ConfirmToBuyItem(){
		screenPopup.ShowPopup(PopupType.Confirmation,PopupEventType.BuyItem,false);
	}
}
