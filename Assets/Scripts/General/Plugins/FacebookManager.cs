using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Facebook.MiniJSON;

public class FacebookManager : MonoBehaviour {
	#region singleton
	private static FacebookManager instance = null;
	public static FacebookManager Instance{ get{return instance;} }

	void Awake()
	{
		if(instance != null && instance != this) Destroy(gameObject);
		else instance = this;
		DontDestroyOnLoad(this.gameObject);

		Init();
	}
	#endregion

	#region delegate events
	public delegate void PlayerLogin();
	public delegate void PlayerShare();
	public delegate void PlayerInvite();
	public event PlayerLogin OnPlayerLogin;
	public event PlayerShare OnPlayerShare;
	public event PlayerInvite OnPlayerInvite;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Init()
	{
		if(!FB.IsInitialized) FB.Init(InitCallBack,OnHideUnity);
		else FB.ActivateApp();
	}

	void InitCallBack(){
		if(FB.IsInitialized) FB.ActivateApp();
		else Debug.Log("Facebook Init Failed.");
	}

	void OnHideUnity(bool isGameShown){}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void DoLogin()
	{
		if(FB.IsInitialized) FB.LogInWithPublishPermissions(new List<string>{"publish_actions"},LoginResponse);
		else Init();
	}

	public void DoShare()
	{
//		if(FB.IsInitialized) 
//		else Init();
	}

	public void DoInviteFriends()
	{
		
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region Responses
	void LoginResponse(ILoginResult result)
	{
		if(!string.IsNullOrEmpty(result.Error) || result.Cancelled){
			Debug.Log("Error: "+result.Error.ToString());
		}else{
			Debug.Log("Facebook Login Success. Directing to Gamesparks...");

			Dictionary<string,object> resultObj = Json.Deserialize(result.RawResult) as Dictionary<string,object>;
			string accessToken = resultObj["access_token"].ToString();
			Debug.Log("AccessToken = "+accessToken);
			GameSparkManager.Instance.DoFacebookLogin(accessToken);
		}
	}

	void ShareResponse(IShareResult result)
	{
		if(!string.IsNullOrEmpty(result.Error) || result.Cancelled){
			Debug.Log("Error: "+result.Error.ToString());
		}else{
			Debug.Log("Facebook Share success");
			if(OnPlayerShare != null) OnPlayerShare();
		}
	}

	void InviteResponse(IAppInviteResult result)
	{
		if(!string.IsNullOrEmpty(result.Error) || result.Cancelled){
			Debug.Log("Error: "+result.Error.ToString());
		}else{
			Debug.Log("Invite Success");
			if(OnPlayerInvite != null) OnPlayerInvite();
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}