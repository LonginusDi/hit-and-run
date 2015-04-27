using UnityEngine;
using System.Collections;

public class FBManager : MonoBehaviour {

	public GameObject UIFBIsLoggedIn;
	public GameObject UIFBnotLoggedIn;

	void Awake(){
		FB.Init (SetInit, OnHideUnity);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void SetInit(){
		Debug.Log ("FB init done.");
		if (FB.IsLoggedIn) {
			Debug.Log ("FB logged in.");
			UIFBIsLoggedIn.SetActive(true);
			UIFBnotLoggedIn.SetActive(false);
		} else {
			Debug.Log("FB not logged in.");
			//			FBLogin();
		}
	}
	private void OnHideUnity(bool isGameShown){
		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}
	
	public void FBLogin(){
		FB.Login ("public_profile", AuthCallBack);
	}
	
	void AuthCallBack(FBResult result){
		if (FB.IsLoggedIn) {
			Debug.Log("FB log in worked.");
			DealWithFBMenus(true);
		} else {
			Debug.Log("FB log in failed.");
			DealWithFBMenus(false);
		}
	}

	void DealWithFBMenus(bool isLoggedIn){
		if (isLoggedIn) {
			UIFBIsLoggedIn.SetActive(true);
			UIFBnotLoggedIn.SetActive(false);
		} else {
			UIFBIsLoggedIn.SetActive(false);
			UIFBnotLoggedIn.SetActive(true);

		}
	}

	public void ShareWithFriends(){
		FB.Feed (
			linkCaption: "Hit and Run is an amazing game!",
			picture:"https://newevolutiondesigns.com/images/freebies/space-wallpaper-29.jpg",
			linkName:"Check out this game!",
			link: "www.google.com"
//			link: "http://apps.facebook.com/" + FB.AppID + "/?challenge_brag" + (FB.IsLoggedIn ? FB.UserID : "guest")
		);
	}
	
	//
	//	public void Share(){
	////		if (!SoomlaProfile.IsLoggedIn (Soomla.Profile.Provider.FACEBOOK)) {
	////			SoomlaProfile.Login( Soomla.Profile.Provider.FACEBOOK, "email,publish_actions", null);
	////		}
	////		else if (SoomlaProfile.IsLoggedIn (Soomla.Profile.Provider.FACEBOOK)) {
	////			FB.Feed (
	////				linkCaption: "Great",
	////				linkName: "yo",
	////				actionLink: "http://www.google.com"
	////			);
	////		} 
	//
	//		if (!FB.IsLoggedIn) {
	////			FB.Login ("email,publish_actions", LoginCallback);
	//		} else if (FB.IsLoggedIn) {
	//			FB.Feed(
	//				linkCaption:"Got it!",
	//				linkName:"oh yeah",
	//				link:"http://www.google.com"
	//				);
	//		}
	//	}
}
