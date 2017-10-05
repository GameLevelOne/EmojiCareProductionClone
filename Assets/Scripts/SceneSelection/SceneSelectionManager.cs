using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSelectionManager : MonoBehaviour {
	public ScreenPopup screenPopup;
	public EmojiSO[] emojiSO;
	public Transform parentObj;
	public EmojiSelectionData emojiObj;

	List<int> unlockedEmoji = new List<int>();
	List<int> lockedEmoji = new List<int>();

	Vector3[] selectionPos = new Vector3[]{new Vector3(-200,80,0),new Vector3(200,80,0),new Vector3(0,-250,0)};

	void Start(){
		//GenerateSelectionPool();
	}

	void OnEnable(){
		EmojiSelectionData.OnEmojiClicked += OnEmojiClicked;
	}

	void OnDisable(){
		EmojiSelectionData.OnEmojiClicked -= OnEmojiClicked;
	}

	void OnEmojiClicked (bool needToBuy)
	{
		if(needToBuy){
			ConfirmBuyEmoji();
		} else{
			ConfirmSelectEmoji();
		}
	}

	public void GenerateSelectionPool(){
		for(int i=0;i<emojiSO.Length;i++){
//			if(emojiSO[i].isUnlocked){
				unlockedEmoji.Add(i);
				Debug.Log("unlocked "+i);
//			} else{
				lockedEmoji.Add(i);
				Debug.Log("locked "+i);
//			}
		}

		int rand = 0;
		int currIdx = 0;

		for(int i=0;i<3;i++){
			
			if(i == 2){
				rand = Random.Range(0,lockedEmoji.Count);
				currIdx = lockedEmoji[rand];
				lockedEmoji.RemoveAt(rand);
			} else{
				rand = Random.Range(0,unlockedEmoji.Count);
				currIdx = unlockedEmoji[rand];
				unlockedEmoji.RemoveAt(rand);
			}

			GameObject obj = Instantiate(emojiObj.gameObject,parentObj,false);
			obj.transform.localPosition = selectionPos[i];
			obj.GetComponent<EmojiSelectionData>().InitEmoji(emojiSO[currIdx]);
		}
	}

	void ConfirmSelectEmoji(){
		screenPopup.ShowUI(PopupType.Confirmation,PopupEventType.SelectEmoji,false);
	}

	void ConfirmBuyEmoji(){
		screenPopup.ShowUI(PopupType.Confirmation,PopupEventType.BuyEmoji,true);
	}

}
