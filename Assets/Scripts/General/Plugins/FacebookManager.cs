using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FacebookManager : MonoBehaviour {
	#region singleton
	private static FacebookManager instance = null;
	public static FacebookManager Instance{ get{return instance;} }

	void Awake()
	{
		if(instance != null && instance != this) Destroy(gameObject);
		else instance = this;

		Init();
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Init()
	{
		if(!FB.IsInitialized) FB.Init();
		else FB.ActivateApp();
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void DoLogin(){
		if(FB.IsInitialized)FB.LogInWithPublishPermissions(new List<string>{"publish_actions"},LoginResponse);
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
