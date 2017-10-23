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
	int tileWidth = 3;
	int tileHeight = 2;
	int currentRecordCount = 1;

	float boxSize = 150;
	float contentBoxMarginX = 50;

	void OnEnable(){
		//PlayerData.Instance.PlayerEmoji.OnEmojiDead += OnEmojiDead;
		AlbumTile.OnSelectEmoji += OnSelectEmoji;
		ScreenPopup.OnSendOffEmoji += OnSendOffEmoji;
	}

	void OnDisable(){
		//PlayerData.Instance.PlayerEmoji.OnEmojiDead -= OnEmojiDead;
		AlbumTile.OnSelectEmoji -= OnSelectEmoji;
		ScreenPopup.OnSendOffEmoji -= OnSendOffEmoji;
	}

//	void OnEmojiDead ()
//	{
//		AddEmojiRecord();
//	}

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
		AddEmojiRecord();
	}

	public override void InitUI ()
	{
		Debug.Log ("album");

		tileHeight = Mathf.CeilToInt (tileCount / tileWidth);

		int tempIdx = 0;
		emojiContentBox.sizeDelta = new Vector2 (0, ((float)tileHeight * boxSize) + contentBoxMarginX);

		//emojiData.Add(EmojiType.Emoji);

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
		if(currentRecordCount > tileCount){
			tileCount = currentRecordCount;
		}

		PlayerData.Instance.EmojiAlbumData.Add(PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType);
		PlayerData.Instance.EmojiAlbumEntryTime.Add(System.DateTime.Now.ToString());
		PlayerData.Instance.EmojiCompletionRate.Add(PlayerData.Instance.PlayerEmoji.emojiExpressions.expressionProgress);
		PlayerPrefs.SetInt(PlayerPrefKeys.Player.EMOJI_RECORD_COUNT,currentRecordCount);

		//set emoji status
	}

	public void ShowAlbum(){
		if(currentRecordCount > 1){
			ShowPanelInHotkey(this.gameObject);
		} else{
			screenPopup.ShowPopup(PopupType.Warning,PopupEventType.AlbumLocked,false,false);
		}
	}
}
