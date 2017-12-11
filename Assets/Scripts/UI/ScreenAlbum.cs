using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenAlbum : BaseUI {
	public ScreenPopup screenPopup;
	public GameObject emojiBoxPrefab;
	public RectTransform emojiContentBox;
	public Scrollbar scrollbar;
	public Image emojiBigIcon;
	public Text emojiEntryTime;
	public Text emojiCompletionRate;

	public EmojiIcons emojiIcons;
	public Sprite lockedEmoji;

	List<EmojiType> emojiData = new List<EmojiType>();

	int tileCount = 6; //initial display
	int tileWidth = 1;
	int tileHeight = 1;
	int lastTileWidth = 1;
	int currentRecordCount = 0;

	float boxSize = 150;
	float contentBoxMarginX = 50;

	void OnEnable(){
		AlbumTile.OnSelectEmoji += OnSelectEmoji;
		ScreenPopup.OnSendOffEmoji += OnSendOffEmoji;
	}

	void OnDisable(){
		AlbumTile.OnSelectEmoji -= OnSelectEmoji;
		ScreenPopup.OnSendOffEmoji -= OnSendOffEmoji;
	}

	void OnSelectEmoji (Sprite sprite,string time,float completionRate)
	{
		emojiBigIcon.sprite = sprite;
		emojiEntryTime.text = "Sent off at "+time;
		emojiCompletionRate.text = "Completion rate: "+completionRate.ToString()+"%";
	}

	void OnSendOffEmoji(Sprite sprite,string name){
		AddEmojiRecord();
	}

	//TODO: create event
	void OnEmojiTransfer(){
		//AddEmojiRecord();
	}

	public override void InitUI ()
	{
		Debug.Log ("album");

		tileHeight = Mathf.CeilToInt (tileCount / tileWidth);

		int tempIdx = 0;
		int counter = 0;
		emojiContentBox.sizeDelta = new Vector2 (0, ((float)tileHeight * boxSize) + contentBoxMarginX);

		currentRecordCount = PlayerPrefs.GetInt (PlayerPrefKeys.Player.EMOJI_RECORD_COUNT, 0);

		if(currentRecordCount >= 0){
			counter = currentRecordCount;
			while(counter>0){
				EmojiType emojiType = PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType;
				string emojiEntryTime = System.DateTime.Now.ToString ();
				float completionRate = PlayerData.Instance.PlayerEmoji.emojiExpressions.GetTotalExpressionProgress ();

				PlayerData.Instance.EmojiAlbumData.Add(emojiType);
				PlayerData.Instance.EmojiAlbumEntryTime.Add(emojiEntryTime);
				PlayerData.Instance.EmojiCompletionRate.Add(completionRate);
				counter--;
			}
		}


		if(currentRecordCount <= 3){
			tileHeight=1;
			tileWidth=currentRecordCount;
		} else{
			if(currentRecordCount%3 !=0){
				tileHeight = currentRecordCount/3 + 1;
			} else{
				tileHeight = currentRecordCount/3;
			}
			tileWidth = currentRecordCount%3;
		}

		for (int i = 0; i < tileHeight; i++) {
			for (int j = 0; j < tileWidth; j++) {
				GameObject obj = Instantiate (emojiBoxPrefab, emojiContentBox, false) as GameObject;
				obj.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (90 + j * 140, -85 - i * 150);

				if (PlayerData.Instance.EmojiAlbumData.Count != 0 && (currentRecordCount-1) >=tempIdx) {
					if (PlayerData.Instance.EmojiAlbumData [tempIdx] != null) {
						Debug.Log ("asd");
						Sprite sprite = emojiIcons.GetEmojiIcon(PlayerData.Instance.EmojiAlbumData[tempIdx]);
						string entryTime = PlayerData.Instance.EmojiAlbumEntryTime[tempIdx];
						float completionRate = PlayerData.Instance.EmojiCompletionRate[tempIdx];
						obj.GetComponent<AlbumTile>().InitTile(sprite,entryTime,completionRate);
					} else {
						obj.GetComponent<Button> ().interactable = false;
					}

					tempIdx++;
				}
			}
		}
		scrollbar.size = 0.1f;
	}

	public void AddEmojiRecord(){
		Debug.Log("add emoji record");
		currentRecordCount++;
		Debug.Log ("record count:" + currentRecordCount);
		if(currentRecordCount > tileCount){
			tileCount = currentRecordCount;
		}

		EmojiType emojiType = PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType;
		string emojiEntryTime = System.DateTime.Now.ToString ();
		float completionRate = PlayerData.Instance.PlayerEmoji.emojiExpressions.GetTotalExpressionProgress ();

		PlayerData.Instance.EmojiAlbumData.Add(emojiType);
		PlayerData.Instance.EmojiAlbumEntryTime.Add(emojiEntryTime);
		PlayerData.Instance.EmojiCompletionRate.Add(completionRate);

		PlayerPrefs.SetInt(PlayerPrefKeys.Player.EMOJI_RECORD_COUNT,currentRecordCount);
		PlayerPrefs.SetInt (PlayerPrefKeys.Album.EMOJI_TYPE+currentRecordCount.ToString(), (int)emojiType);
		PlayerPrefs.SetString (PlayerPrefKeys.Album.ENTRY_TIME+currentRecordCount.ToString(),emojiEntryTime);
		PlayerPrefs.SetFloat (PlayerPrefKeys.Album.COMPLETION_RATE+currentRecordCount.ToString(),completionRate);

		//set emoji status
	}

	public void ShowAlbum(){
		if(currentRecordCount >= 1){
			ShowPanelInHotkey(this.gameObject);
		} else{
			screenPopup.ShowPopup(PopupType.Warning,PopupEventType.AlbumLocked,false,false);
		}
	}
}
