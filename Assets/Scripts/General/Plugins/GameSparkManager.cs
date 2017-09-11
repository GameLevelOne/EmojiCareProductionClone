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
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void DoFacebookLogin(string facebookAccessToken)
	{
		new FacebookConnectRequest().SetAccessToken(facebookAccessToken).Send((response)=>{
			if(!response.HasErrors){
				//do code here
			}
		});
	}

	public void DoLogout()
	{
		GS.Reset();
		FacebookManager.Instance.DoLogout();
	}

	public void GetDownloadable()
	{
		new GameSparks.Api.Requests.GetDownloadableRequest().SetShortCode("mybundle").Send((response)=>{
			if(!response.HasErrors){
				
			}else{
				Debug.Log("Error: "+response.Errors.JSON);
			}	
		});
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}
