using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class GP8SingletonSocialNetworksManager : SingletonBase<GP8SingletonSocialNetworksManager>
{

		private GP8SingletonSocialNetworksManager ()
		{
		} // guarantee this will be always a singleton only - can't use the constructor!

		private void Awake ()
		{
				FB.Init (OnInitComplete, OnHideUnity);
		}

		public void SocialTakeScreenshot ()
		{
				StartCoroutine ("TakeScreenshot");
		}

		public void SocialUserRegister (string id, string friendlist, string friendNames)
		{
				StartCoroutine (RegisterUser (id, friendlist, friendNames));
		}

	#region FACEBOOK_CALLBACKS
		private void OnInitComplete ()
		{
				Debug.Log ("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
		}
	
		private void OnHideUnity (bool isGameShown)
		{
				Debug.Log ("Is game showing? " + isGameShown);
		}

		private IEnumerator TakeScreenshot ()
		{
				yield return new WaitForEndOfFrame ();
		
				var width = Screen.width;
				var height = Screen.height;
				var tex = new Texture2D (width, height, TextureFormat.RGB24, false);
				// Read screen contents into the texture
				tex.ReadPixels (new Rect (0, 0, width, height), 0, 0);
				tex.Apply ();
				byte[] screenshot = tex.EncodeToPNG ();
		
				var wwwForm = new WWWForm ();
				wwwForm.AddBinaryData ("image", screenshot, "Screenshot.png");
				wwwForm.AddField ("message", "herp derp.  I did a thing!  Did I do this right?");
		
				FB.API ("me/photos", Facebook.HttpMethod.POST, GP8UtilitySocialNetworks.FacebookCallback, wwwForm);
		
				StopCoroutine ("TakeScreenshot");
		}

		private IEnumerator RegisterUser (string id, string friendlist, string friendNames)
		{
				string url = GP8Constants.FACEBOOK_BASE_URL + "postFBFriends?player=" 
						+ id
						+ "&playerName="
						+ PlayerPrefs.GetString (GP8Constants.KEY_FOR_FACEBOOK_NAME)
						+ "&friendsWith="
						+ friendlist
						+ "&friendsName="
						+ friendNames;
		
				Debug.Log ("Final URL: " + url);
				WWW request = new WWW (url);
				yield return request;

				if (!string.IsNullOrEmpty (request.error))
						Debug.Log ("User registration failed with error: " + request.error);
				else {
						Debug.Log ("User registration state: " + request.text);
						var data = Json.Deserialize (request.text) as Dictionary<string, object>;
						bool isPlayerAlreadyExist = (bool)(data ["isExist"]);
						Debug.Log ("Player exist?: " + isPlayerAlreadyExist.ToString ());
						if (!isPlayerAlreadyExist) {
								// Reward for facebook login
				
				
						}
				}
		}
	#endregion
}
