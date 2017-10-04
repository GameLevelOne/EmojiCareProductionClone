using System.Collections;
using UnityEngine;
using GameSparks.Api;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;

public class GameSparkManager : MonoBehaviour {
	#region singleton
	private static GameSparkManager instance = null;
	public static GameSparkManager Instance{ get{return instance;} }

	void Awake()
	{
		if(instance != null && instance != this) Destroy(gameObject);
		else instance = this;
		DontDestroyOnLoad(this.gameObject);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region delegate events
	public delegate void LoginSuccessful();
	public event LoginSuccessful OnLoginSuccessful;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void DoFacebookLogin(string facebookAccessToken)
	{
		new FacebookConnectRequest().SetAccessToken(facebookAccessToken).Send((response)=>{
			if(!response.HasErrors){
				PlayerData.Instance.PlayerAuthToken = response.AuthToken;
				Debug.Log("Connected to GameSpark. Auth Token = "+PlayerData.Instance.PlayerAuthToken);
				if(OnLoginSuccessful != null) OnLoginSuccessful();
			}else{
				Debug.Log("GameSparks Login Error: "+response.Errors.JSON.ToString());
			}
		});
	}

	public void DoGuestLogin()
	{
		new GameSparks.Api.Requests.DeviceAuthenticationRequest().Send((response)=>{
			if(!response.HasErrors){
				print("success login device");
				
				if(OnLoginSuccessful != null) OnLoginSuccessful();
			}else{
				print("Error: "+response.Errors.JSON.ToString());
			}
		});
	}

	public void DoSaveData()
	{
		
	}

	public void DoLoadData()
	{
		
	}

	/// <summary>
	/// USE THIS for get emoji bundle.
	/// </summary>
	public void GetDownloadableURL(EmojiType emojiType)
	{
		new GameSparks.Api.Requests.GetDownloadableRequest().SetShortCode(ShortCode.EMOJIS[(int)emojiType]).Send((response)=>{
			if(!response.HasErrors){
				BundleLoader.Instance.DoLoadBundle(response.Url,emojiType);
			}else{
				Debug.Log("Error: "+response.Errors.JSON);
			}	
		});
	}


	public void DoSetDiary()
	{
//		if(Emoji.Instance.emojiStatus != EmojiStatus.Alive){
//			string emojiName = Emoji.Instance.emojiName;
//			string emojiType = Emoji.Instance.emojiType.ToString();
//			string emojiStatus = Emoji.Instance.emojiStatus.ToString();
//			string date = System.DateTime.Now.ToString();
//
//			new LogChallengeEventRequest_SET_DIARY().Set_EMOJI_NAME(emojiName).Set_EMOJI_TYPE(emojiType).Set_EMOJI_STATUS(emojiStatus).Set_DATE(date).Send((response) =>{
//				if(!response.HasErrors){
//					Debug.Log("data sent succesfully")	;
//				}else{
//					Debug.Log("Error: "+response.Errors.JSON.ToString());
//				} 
//			});
//		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}
