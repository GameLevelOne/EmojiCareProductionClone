using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSelectionManager : MonoBehaviour {
	public ScreenPopup screenPopup;
	public EmojiSO[] emojiSO;
	public Sprite[] emojiIcons;
	public Transform parentObj;
	public EmojiSelectionData emojiObj;

	List<int> unlockedEmoji = new List<int>();
	List<int> lockedEmoji = new List<int>();

	Vector3[] selectionPos = new Vector3[]{new Vector3(-200,200,0),new Vector3(200,200,0),new Vector3(0,-100,0)};

	void Start(){
		GenerateSelectionPool();
	}

	void OnEnable(){
		EmojiSelectionData.OnEmojiClicked += OnEmojiClicked;
	}

	void OnDisable(){
		EmojiSelectionData.OnEmojiClicked -= OnEmojiClicked;
	}

	void OnEmojiClicked (bool needToBuy,Sprite sprite,string emojiName)
	{
		ConfirmEmoji(needToBuy,sprite,emojiName);
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
			obj.GetComponent<EmojiSelectionData>().InitEmoji(emojiSO[currIdx],emojiIcons[currIdx]);
		}
	}

	void ConfirmEmoji(bool needToBuy,Sprite sprite,string emojiName){
		if(needToBuy){
			screenPopup.ShowPopup(PopupType.Confirmation,PopupEventType.BuyEmoji,needToBuy,sprite,emojiName);
		}
		else{
			screenPopup.ShowPopup(PopupType.Confirmation,PopupEventType.SelectEmoji,needToBuy,sprite,emojiName);
		}
	}

}
