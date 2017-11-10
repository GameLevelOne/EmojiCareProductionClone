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
	public GameObject cookBar; //SEMENTARA
	public Pan pan;

	public UIPlantProgress[] plantProgress;

	public ScreenTutorial screenTutorial;

	int roomTotal = 0;
	float distance = 0;
	float xOnBeginDrag;
	bool snapping = false;
	public bool interactable = true;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initializations
	public void Init()
	{
		thisCollider = GetComponent<BoxCollider2D>();
		currentRoom = GetCurrentRoom(transform.localPosition.x);

		PlayerData.Instance.PlayerEmoji.body.currentRoom = (int)currentRoom;
		PlayerData.Instance.PlayerEmoji.body.previousRoom = (int)currentRoom;

		foreach(BaseRoom r in rooms) if(r != null){
				r.InitRoom();
				r.OnRoomChanged(currentRoom);
			}
		AdjustTouchAreaSize();
		RegisterLockRoomEvent();
	}

	void RegisterLockRoomEvent()
	{
		PlayerData.Instance.PlayerEmoji.body.OnEmojiBouncingToCurrentRoom += OnEmojiBouncingToCurrentRoom;
		PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent += OnEmojiSleepEvent;
		PlayerData.Instance.PlayerEmoji.playerInput.OnEmojiPouting += OnEmojiPouting;
		PlayerData.Instance.PlayerEmoji.body.OnEmojiEatEvent += OnEmojiEatEvent;
	}


	void OnEmojiSleepEvent (bool sleeping)
	{
		interactable = !sleeping;
	}
		
	public void OnDestroy()
	{
		PlayerData.Instance.PlayerEmoji.body.OnEmojiBouncingToCurrentRoom -= OnEmojiBouncingToCurrentRoom;
		PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent -= OnEmojiSleepEvent;
		PlayerData.Instance.PlayerEmoji.playerInput.OnEmojiPouting -= OnEmojiPouting;
		PlayerData.Instance.PlayerEmoji.body.OnEmojiEatEvent -= OnEmojiEatEvent;
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
		PlayerData.Instance.PlayerEmoji.body.CancelBouncing();
	}

	//event triggers
	public void BeginDrag()
	{
		if(interactable){
			if(!snapping){
				xOnBeginDrag = transform.localPosition.x;
				float x = getWorldPositionFromTouchInput().x;
				distance = transform.localPosition.x - x;

				foreach(UIPlantProgress pp in plantProgress) pp.Hide();
				//			Emoji.Instance.emojiObject.GetComponent<EmojiObject>().OnRoomChangingStart();
			}
		}

	}
		
	public void Drag()
	{
		if(interactable){
			if(!snapping) transform.position = new Vector3(getWorldPositionFromTouchInput().x + distance,0f,0f);
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
			PlayerData.Instance.PlayerEmoji.gameObject.SetActive(false);
		}else{
			interactable = true;
			PlayerData.Instance.PlayerEmoji.gameObject.SetActive(true);
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

	public void GoToRoom(RoomType destination)
	{		
		if(destination == currentRoom) return;

		Vector3 startPos = transform.position;
		Vector3 endpos = new Vector3((roomWidth*(int)destination*-1f),0f,0f);

		if(PlayerData.Instance.TutorialIdleLivingRoom == 0)
			screenTutorial.TriggerRoomChange ();

		StartCoroutine(SmoothSnap(startPos,endpos));
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region coroutines
	IEnumerator SmoothSnap(Vector3 startPos, Vector3 endPos)
	{
		snapping = true;
		float t = 0;
		RoomType temp = GetCurrentRoom(endPos.x);

		if(currentRoom != temp){
			currentRoom = GetCurrentRoom(endPos.x);
			foreach(BaseRoom r in rooms) if(r != null) r.OnRoomChanged(currentRoom);
		}

		switch(currentRoom)
		{
		case RoomType.Garden: 		
			rooms[(int)currentRoom].GetComponent<Garden>().Init(); 
			break;
		case RoomType.Playroom: 	rooms[(int)currentRoom].GetComponent<Playroom>().Init(); break;
		case RoomType.LivingRoom: 	rooms[(int)currentRoom].GetComponent<LivingRoom>().Init(); break;
		case RoomType.Kitchen: 		rooms[(int)currentRoom].GetComponent<Kitchen>().Init(); break;
		case RoomType.Bedroom: 		rooms[(int)currentRoom].GetComponent<Bedroom>().Init(); break;
		case RoomType.Bathroom: 	rooms[(int)currentRoom].GetComponent<Bathroom>().Init(); break;
		}


		//SEMENTARA
//		if(currentRoom != RoomType.Playroom) danceMat.SetActive(false);
//		else danceMat.SetActive(true);

		//SEMENTARA
		if(currentRoom != RoomType.Kitchen) cookBar.SetActive(false);
		else{
			if(pan.isCooking) cookBar.SetActive(true);
		} 

		while(t <= 1){
			t += Time.fixedDeltaTime * snapSpeed;
			transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0, 1, t));
			yield return null;
		}

		transform.position = endPos;
		snapping = false;

		PlayerData.Instance.PlayerEmoji.transform.parent = rooms[(int)currentRoom].transform;
		PlayerData.Instance.PlayerEmoji.body.BounceToCurrentRoom((int)currentRoom);

		if(currentRoom != RoomType.LivingRoom)
			screenTutorial.CheckRoomPlayerPrefs (currentRoom);

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