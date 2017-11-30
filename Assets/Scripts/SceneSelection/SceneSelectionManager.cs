using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSelectionManager : MonoBehaviour {
	public ScreenSuptixEmoji screenSuptixEmoji;
	public ScreenPopup screenPopup;
	public EmojiIcons emojiIcons;
	public GameObject[] emojiObjects; //3 free, 1 paid
	public EmojiSO[] emojiSO;
	public Transform parentObj;
	public EmojiSelectionData[] emojiObj;
	public Sprite[] tempSprites;

	List<int> freeEmoji = new List<int>();
	List<int> paidEmoji = new List<int>();

	//Vector3[] selectionPos = new Vector3[]{new Vector3(-200,200,0),new Vector3(200,200,0),new Vector3(0,-100,0)};

	void Start ()
	{
		Init ();
	}

	void OnEnable(){
		EmojiSelectionData.OnEmojiClicked += OnEmojiClicked;
		if(AdmobManager.Instance) AdmobManager.Instance.OnFinishWatchVideoAds += OnFinishWatchVideoAds;
	}

	void OnDisable(){
		EmojiSelectionData.OnEmojiClicked -= OnEmojiClicked;
		if(AdmobManager.Instance) AdmobManager.Instance.OnFinishWatchVideoAds -= OnFinishWatchVideoAds;
	}

	void OnEmojiClicked (bool needToBuy,Sprite sprite,EmojiSO emojiData)
	{
		ConfirmEmoji(needToBuy,sprite,emojiData);
	}

	void OnFinishWatchVideoAds(AdEvents eventName){
		if (eventName == AdEvents.ShuffleEmoji){
			GenerateFreeEmojiSelectionPool ();
			GeneratePaidEmojiSelection ();
		}
	}

	void Init(){
		for(int i=0;i<emojiSO.Length;i++){
			if(emojiSO[i].price == 0){
				freeEmoji.Add(i);
			} else{
				paidEmoji.Add(i);
			}
		}
		GenerateFreeEmojiSelectionPool ();
		GeneratePaidEmojiSelection ();
	}

	public void GenerateFreeEmojiSelectionPool ()
	{
		List<int> emojiIdx = new List<int> ();
		int randomIdx = 0;
		int selectedIdx = 0;

		if (emojiIdx.Count == 0) {
			for (int i = 0; i < freeEmoji.Count; i++) {
				emojiIdx.Add (i);
			}
		}

		for (int i = 0; i < 3; i++) {
			if (emojiIdx.Count > 0) {
				randomIdx = Random.Range (0, emojiIdx.Count);
				selectedIdx = freeEmoji [emojiIdx[randomIdx]];
				emojiIdx.RemoveAt (randomIdx);
				emojiObj [i].gameObject.SetActive (true);
				emojiObj [i].InitEmoji (emojiSO [selectedIdx], tempSprites [selectedIdx]);
			} else{
				emojiObj [i].gameObject.SetActive (false);
			}
		}
	}

	void GeneratePaidEmojiSelection(){
		int randomIdx = Random.Range (0, paidEmoji.Count);
		emojiObj [3].InitEmoji (emojiSO [paidEmoji[randomIdx]], tempSprites [paidEmoji[randomIdx]]);
	}

	public void ShuffleEmojiPool(){
		if (AdmobManager.Instance)
			AdmobManager.Instance.ShowRewardedVideo (AdEvents.ShuffleEmoji);
	}

	public void ChooseEmoji(){
		screenSuptixEmoji.ShowUI (screenSuptixEmoji.gameObject);
	}

	void ConfirmEmoji(bool needToBuy,Sprite sprite,EmojiSO emojiData){
		PlayerData.Instance.SelectedEmoji = emojiData.emojiType;
		PlayerData.Instance.PlayerEmojiType = (int)emojiData.emojiType;
		if(needToBuy){
			if(PlayerData.Instance.PlayerGem>= emojiData.price){
				screenPopup.ShowPopup(PopupType.Confirmation,PopupEventType.BuyEmoji,needToBuy,false,sprite,emojiData.emojiType.ToString());
			} else{
				screenPopup.ShowPopup (PopupType.Warning, PopupEventType.NotAbleToBuyEmoji);
			}
		}
		else{
			screenPopup.ShowPopup(PopupType.Confirmation,PopupEventType.SelectEmoji,needToBuy,false,sprite,emojiData.emojiType.ToString());
		}
	}

}
