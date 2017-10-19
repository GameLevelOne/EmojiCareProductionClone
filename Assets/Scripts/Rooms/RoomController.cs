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

	int roomTotal = 0;
	float distance = 0;
	float xOnBeginDrag;
	bool snapping = false;
	bool interactable = true;
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
	}

	public void RegisterLockRoomEvent()
	{
		PlayerData.Instance.PlayerEmoji.body.OnEmojiBouncingToCurrentRoom += OnEmojiBouncingToCurrentRoom;
	}

	void OnEmojiBouncingToCurrentRoom ()
	{
		StartCoroutine(_lockRoomChanging);	
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
		print("Total Room = "+roomTotal);
		thisCollider.size = new Vector2((roomWidth*roomTotal),roomHeight);
		thisCollider.offset = new Vector2((thisCollider.size.x/2f)-(roomWidth/2f),0f);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics

	//event triggers
	public void BeginDrag()
	{
		if(interactable){
			if(!snapping){
				xOnBeginDrag = transform.localPosition.x;
				float x = getWorldPositionFromTouchInput().x;
				distance = transform.localPosition.x - x;

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
	public void SetEditMode(bool editMode)
	{
		thisCollider.enabled = editMode;
	}

	public void GoToRoom(RoomType destination)
	{
		if(destination == currentRoom) return;

		Vector3 startPos = transform.position;
		Vector3 endpos = new Vector3((roomWidth*(int)destination*-1f),0f,0f);
		StartCoroutine(SmoothSnap(startPos,endpos));
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region coroutines
	IEnumerator SmoothSnap(Vector3 startPos, Vector3 endPos)
	{
		snapping = true;
		float t = 0;

		currentRoom = GetCurrentRoom(endPos.x);

		foreach(BaseRoom r in rooms) if(r != null) r.OnRoomChanged(currentRoom);

		while(t <= 1){
			t += Time.fixedDeltaTime * snapSpeed;
			transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0, 1, t));
			yield return new WaitForSeconds(Time.deltaTime);
		}

		transform.position = endPos;
		snapping = false;

		PlayerData.Instance.PlayerEmoji.emojiExpressions.ResetExpressionDuration();
		PlayerData.Instance.PlayerEmoji.body.BounceToCurrentRoom((int)currentRoom);

		yield return null;
	}

	const string _lockRoomChanging = "LockRoomChanging";
	IEnumerator LockRoomChanging()
	{
		interactable = false;
		yield return new WaitForSeconds(1f);
		interactable = true;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}