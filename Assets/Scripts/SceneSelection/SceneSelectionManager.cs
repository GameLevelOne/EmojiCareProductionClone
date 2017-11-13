using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSelectionManager : MonoBehaviour {
	public ScreenPopup screenPopup;
	public EmojiIcons emojiIcons;
	public EmojiSO[] emojiSO;
	public Transform parentObj;
	public EmojiSelectionData emojiObj;

	List<int> unlockedEmoji = new List<int>();
	List<int> lockedEmoji = new List<int>();

	Vector3[] selectionPos = new Vector3[]{new Vector3(-200,200,0),new Vector3(200,200,0),new Vector3(0,-100,0)};

	void Start ()
	{
		if (PlayerData.Instance.PlayerEmoji.emojiDead) {
			GenerateSelectionForDeadEmoji ();
		} else {
			GenerateSelectionPool ();
		}
	}

	void OnEnable(){
		EmojiSelectionData.OnEmojiClicked += OnEmojiClicked;
	}

	void OnDisable(){
		EmojiSelectionData.OnEmojiClicked -= OnEmojiClicked;
	}

	void OnEmojiClicked (bool needToBuy,Sprite sprite,EmojiSO emojiData)
	{
		ConfirmEmoji(needToBuy,sprite,emojiData);
	}

	public void GenerateSelectionPool(){
		for(int i=0;i<emojiSO.Length;i++){
			if(emojiSO[i].isUnlocked){
				unlockedEmoji.Add(i);
				Debug.Log("unlocked "+i);
			} else{
				lockedEmoji.Add(i);
				Debug.Log("locked "+i);
			}
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
			obj.GetComponent<EmojiSelectionData>().InitEmoji(emojiSO[currIdx],emojiIcons.GetEmojiIcon((EmojiType)currIdx));
		}
	}

	public void GenerateSelectionForDeadEmoji(){
		int currentEmojiType = PlayerData.Instance.PlayerEmojiType;
		GameObject obj = Instantiate(emojiObj.gameObject,parentObj,false);
		obj.transform.localPosition = selectionPos[2];
		obj.GetComponent<EmojiSelectionData> ().InitEmoji (emojiSO [currentEmojiType], emojiIcons.GetEmojiIcon ((EmojiType)currentEmojiType));
	}

	void ConfirmEmoji(bool needToBuy,Sprite sprite,EmojiSO emojiData){
		PlayerData.Instance.SelectedEmoji = emojiData.emojiType;
		PlayerData.Instance.PlayerEmojiType = (int)emojiData.emojiType;
		if(needToBuy){
			screenPopup.ShowPopup(PopupType.Confirmation,PopupEventType.BuyEmoji,needToBuy,false,sprite,emojiData.emojiType.ToString());
		}
		else{
			screenPopup.ShowPopup(PopupType.Confirmation,PopupEventType.SelectEmoji,needToBuy,false,sprite,emojiData.emojiType.ToString());
		}
	}

}
