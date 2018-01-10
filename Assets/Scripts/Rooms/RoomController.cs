using System;
using System.Collections;
using UnityEngine;

public class RoomController : MonoBehaviour {
	#region attributes
	//constants
	const float roomWidth = 8f;
	const float roomHeight = 12.8f;
	float snapSpeed = 6f;

	public BaseRoom[] rooms;

	BoxCollider2D thisCollider;
	public RoomType currentRoom = RoomType.LivingRoom;
	public GameObject danceMat; //SEMENTARA
	public GameObject Album;
	public GardenMiscItemsManager gardenMiscItemsManager;
	public RandomPassingToyManager randomPassingToyManager;
	public Pan pan;
	public GardenStall stall;
	public Soil soil;

	public ScreenTutorial screenTutorial;

	//TEMP
	public GuidedTutorialStork guidedTutorial;

	int roomTotal = 0;
	float distance = 0;
	float xOnBeginDrag;
	float beginXTouch;
	bool snapping = false;
	bool flagTouchRoom = false;
	bool flagDragging = false;

	[Header("Do Not Modify")]
	public bool interactable = true;

	bool hasInit = false;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initializations
	public void Init()
	{
		if(!hasInit){
			hasInit = true;

			thisCollider = GetComponent<BoxCollider2D>();

			transform.position = new Vector3(-16f,0f,0f);
			currentRoom = RoomType.LivingRoom;

//			PlayerData.Instance.PlayerEmoji.body.currentRoom = (int)currentRoom;
//			PlayerData.Instance.PlayerEmoji.body.previousRoom = (int)currentRoom;

			foreach(BaseRoom r in rooms) if(r != null){
					r.InitRoom();
					r.OnRoomChanged(currentRoom);
				}
			print("ALBUM DATA = "+PlayerData.Instance.EmojiAlbumData.Count);
			if(PlayerData.Instance.EmojiAlbumData.Count <= 0) Album.SetActive(false);

			AdjustTouchAreaSize();
			pan.OnCookingDone += OnCookingDone;
			stall.Init();
			soil.Init();
		}
	}

	public void RegisterEmojiEvents()
	{
		PlayerData.Instance.PlayerEmoji.body.OnEmojiBouncingToCurrentRoom += OnEmojiBouncingToCurrentRoom;
		PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent += OnEmojiSleepEvent;
		PlayerData.Instance.PlayerEmoji.playerInput.OnEmojiPouting += OnEmojiPouting;
		PlayerData.Instance.PlayerEmoji.body.OnEmojiEatEvent += OnEmojiEatEvent;

	}

	public void UnregisterEmojiEvents()
	{
		PlayerData.Instance.PlayerEmoji.body.OnEmojiBouncingToCurrentRoom -= OnEmojiBouncingToCurrentRoom;
		PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent -= OnEmojiSleepEvent;
		PlayerData.Instance.PlayerEmoji.playerInput.OnEmojiPouting -= OnEmojiPouting;
		PlayerData.Instance.PlayerEmoji.body.OnEmojiEatEvent -= OnEmojiEatEvent;
	}


		
	public void OnDestroy()
	{
		if(PlayerData.Instance.PlayerEmoji) UnregisterEmojiEvents();
		pan.OnCookingDone -= OnCookingDone;
	}

	void OnEmojiSleepEvent (bool sleeping)
	{
		interactable = !sleeping;
	}

	void OnEmojiEatEvent (float lockDuration)
	{
		LockInteraction(lockDuration);
	}

	void OnEmojiPouting ()
	{
		LockInteraction(10f);
	}

	void OnEmojiBouncingToCurrentRoom ()
	{
		LockInteraction(1f);
	}
		
	void OnCookingDone()
	{
		LockInteraction(1f + (40f/60f));
	}
	/// <summary>
	///<para>Automatically adjust the room collider size for touch function.</para> 
	/// <para> </para>
	/// <para>- collider size (x,y) = (roomWidth * totalRoom,roomHeight)</para>
	/// <para>we want the collider position to be at 0,0 in world space, so:</para>
	/// <para>- collider offset (x,y) = ( (colliderSize.x / 2)-(roomWidth/2), 0)</para>
	/// </summary>
	void AdjustTouchAreaSize()
	{
		roomTotal = Enum.GetNames(typeof(RoomType)).Length;
//		print("Total Room = "+roomTotal);
		thisCollider.size = new Vector2((roomWidth*roomTotal),roomHeight);
		thisCollider.offset = new Vector2((thisCollider.size.x/2f)-(roomWidth/2f),0f);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void PointerDown()
	{
		if(interactable){
			PlayerData.Instance.PlayerEmoji.body.CancelBouncing();
			flagTouchRoom = true;
		}
	}

	public void PointerUp()
	{
		if(interactable){
			flagTouchRoom = false;
			flagDragging = false;
		}
	}

	//event triggers
	public void BeginDrag()
	{
		if(interactable){
			if(!snapping){
				beginXTouch = getWorldPositionFromTouchInput().x;
				xOnBeginDrag = transform.localPosition.x;
				float x = getWorldPositionFromTouchInput().x;
				distance = transform.localPosition.x - x;

				if(currentRoom == RoomType.Playroom){
					randomPassingToyManager.Stop();
				}
			}
		}
	}
		
	public void Drag()
	{
		if(interactable){
			if(!snapping){
				
				if(flagTouchRoom){
					float currentXTouch = getWorldPositionFromTouchInput().x;
					if(Mathf.Abs(currentXTouch - beginXTouch) > 0.3f){
						flagTouchRoom = false;
						flagDragging = true;
					}
				}else if(flagDragging){
					transform.position = new Vector3(getWorldPositionFromTouchInput().x + distance,0f,0f);
				}
			}
		}
	}

	public void EndDrag()
	{
		if(interactable){
			if(!snapping){
				Vector3 startPos = transform.position;
				Vector3 endpos = new Vector3(getXEndPosition(startPos.x),0f,0f);
//				print("StartPos = "+startPos);
//				print("EndPos = "+endpos);
				StartCoroutine(SmoothSnap(startPos,endpos));
			}
		}
	}

	//proccessors
	Vector3 getWorldPositionFromTouchInput()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,10f);
		return Camera.main.ScreenToWorldPoint(tempMousePosition);
	}

	float getXEndPosition(float xPosOnEndDrag)
	{
//		print(xPosOnEndDrag);
//		print(-1 * ((roomTotal-1) * roomWidth));
		if(xPosOnEndDrag >= 3.6f){ //most left of rooms = nothing
			
			return 0;
		}else if(xPosOnEndDrag <= -1 * ((roomTotal-1) * roomWidth)){ //most right of rooms = nothing
			
			return (-1 * ((roomTotal-1) * roomWidth));
		}else{
			float ratio = Mathf.Abs(xPosOnEndDrag) / roomWidth;
			float tenths = ratio - Mathf.Floor(ratio);
			int index = Mathf.FloorToInt(ratio);

			if(xOnBeginDrag < xPosOnEndDrag){
				if(tenths > 0.9f) index++;
			}else{
				if(tenths > 0.1f) index++;
			}
			return -1f * (index * roomWidth);
		}
	}

	/// <summary>
	/// I don't know what happens, but when I use (int) casting instead, the system misscalculate. 21.6 / 7.2 is 3 in float, but 2 in int. thus I use Mathf.CeilToInt
	/// </summary>
	RoomType GetCurrentRoom(float xPos)
	{
		int roomIndex = Mathf.CeilToInt(Mathf.Abs(xPos / roomWidth));
		return (RoomType)roomIndex;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void OnRoomEditMode(bool flagEditRoom)
	{
		if(flagEditRoom){
			interactable = false;
			PlayerData.Instance.PlayerEmoji.HideEmojiWhenEditMode ();
		}else{
			interactable = true;
			PlayerData.Instance.PlayerEmoji.ReturnEmojiFromEditMode();

		}
	}

	public void LockInteraction(float duration)
	{
		StartCoroutine(_lockRoomChanging,duration);
	}

	public void SetEditMode(bool editMode)
	{
		thisCollider.enabled = editMode;
	}

	public void GoToRoom (RoomType destination)
	{		
		if (destination == currentRoom)
			return;

		Vector3 startPos = transform.position;
		Vector3 endpos = new Vector3 ((roomWidth * (int)destination * -1f), 0f, 0f);

//		if (PlayerData.Instance.TutorialIdleLivingRoom == 0) {
//			screenTutorial.TriggerRoomChange ();
//		}

		StartCoroutine(SmoothSnap(startPos,endpos));
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region coroutines
	IEnumerator SmoothSnap (Vector3 startPos, Vector3 endPos)
	{
		snapping = true;
		float t = 0;
		RoomType temp = GetCurrentRoom (endPos.x);

		if (currentRoom != temp) {
			currentRoom = GetCurrentRoom (endPos.x);
			foreach (BaseRoom r in rooms)
				if (r != null)
					r.OnRoomChanged (currentRoom);
		}

		switch (currentRoom) {
		case RoomType.Garden: 		
			rooms [(int)currentRoom].GetComponent<Garden> ().Init ();

			break;
		case RoomType.Playroom:
			rooms [(int)currentRoom].GetComponent<Playroom> ().Init ();
			break;
		case RoomType.LivingRoom:
			rooms [(int)currentRoom].GetComponent<LivingRoom> ().Init ();
			break;
		case RoomType.Kitchen:
			rooms [(int)currentRoom].GetComponent<Kitchen> ().Init ();
			break;
		case RoomType.Bedroom:
			rooms [(int)currentRoom].GetComponent<Bedroom> ().Init ();
			break;
		case RoomType.Bathroom:
			rooms [(int)currentRoom].GetComponent<Bathroom> ().Init ();
			break;
		}

		PlayerData.Instance.PlayerEmoji.body.CheckRoomForBubbleMechanic (currentRoom);

		//SEMENTARA
//		if(currentRoom != RoomType.Playroom) danceMat.SetActive(false);
//		else danceMat.SetActive(true);

		if (currentRoom == RoomType.Garden) {
			gardenMiscItemsManager.Init ();
			CropHolder.Instance.ShowCrops ();
		} else {
			gardenMiscItemsManager.Hide ();
			CropHolder.Instance.HideCrops ();
		}


		//SNAPPING
		while (t <= 1) {
			t += Time.fixedDeltaTime * snapSpeed;
			transform.position = Vector3.Lerp (startPos, endPos, Mathf.SmoothStep (0, 1, t));
			yield return null;
		}

		transform.position = endPos;

		snapping = false;

		PlayerData.Instance.PlayerEmoji.transform.parent = rooms [(int)currentRoom].transform;
		print("T E R P A N G G I L");
		PlayerData.Instance.PlayerEmoji.body.BounceToCurrentRoom ((int)currentRoom);

//		if(currentRoom != RoomType.LivingRoom)
//			screenTutorial.CheckRoomPlayerPrefs (currentRoom);

		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == ShortCode.SCENE_GUIDED_TUTORIAL) {
			if(currentRoom == RoomType.Kitchen){
				guidedTutorial.ShowFirstDialog ((int)GuidedTutorialIndex.Kitchen);
			}
		}

		yield return null;
	}

	const string _lockRoomChanging = "LockRoomChanging";
	IEnumerator LockRoomChanging(float duration)
	{
		interactable = false;
		yield return new WaitForSeconds(duration);
		interactable = true;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}