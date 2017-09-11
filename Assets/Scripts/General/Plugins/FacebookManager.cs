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
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Init()
	{
		if(!FB.IsInitialized) FB.Init(InitCallBack,OnHideUnity);
		else
			#if UNITY_ANDROID
			FB.ActivateApp();
			#endif
	}

	void InitCallBack(){
		if(FB.IsInitialized)
			#if UNITY_ANDROID
			FB.ActivateApp();
			#endif
		else Debug.Log("Facebook Init Failed.");
	}
	void OnHideUnity(bool isGameShown){}

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void DoLogin(){
		if(FB.IsInitialized) FB.LogInWithPublishPermissions(new List<string>{"publish_actions"},LoginResponse);
		else Init();
	}
	public void DoShare(){
//		if(FB.IsInitialized) 
//		else Init();
	}
	public void DoInviteFriends(){}

	public void DoLogout(){
		FB.LogOut();
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region Responses
	void LoginResponse(ILoginResult result)
	{
		if(!string.IsNullOrEmpty(result.Error) || result.Cancelled){
			if(result.Cancelled) Debug.Log("Error: Login canceled");
			else Debug.Log("Error: Facebook Login Failed. "+result.Error.ToString());
		}else{
			Debug.Log("Facebook Login Successful. Directing to Gamesparks...");

			Dictionary<string,object> resultObj = Json.Deserialize(result.RawResult) as Dictionary<string,object>;
			string accessToken = resultObj["access_token"].ToString();
			Debug.Log("AccessToken = "+accessToken);
			GameSparkManager.Instance.DoFacebookLogin(accessToken);
		}
	}

	void ShareResponse(IShareResult result)
	{
		
	}

	void InviteResponse(IAppInviteResult result)
	{
		
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}
