using UnityEngine;
using System.Collections;

public class GP8SingletonAnalyticsManager : SingletonBase<GP8SingletonAnalyticsManager>
{

		private GP8SingletonAnalyticsManager ()
		{
		} // guarantee this will be always a singleton only - can't use the constructor!
	
		private void Awake ()
		{
				#if UNITY_IPHONE
				FlurryAnalytics.startSession (GP8UtilityAnalytics.ANALYTICS_ID);
				#elif UNITY_ANDROID
				FlurryAndroid.onStartSession(GP8UtilityAnalytics.ANALYTICS_ID, false, true);
				#endif
		}

		#if UNITY_ANDROID
		public void OnApplicationPause (bool paused)
		{
				if (paused) {
					// Game is paused, remember the time
					FlurryAndroid.onEndSession();
				} else {				
					// Game is unpaused, calculate the time passed since the game was paused and use this time to calculate build times of your buildings or how much money the player has gained in the meantime. 
					FlurryAndroid.onStartSession(GP8UtilityAnalytics.ANALYTICS_ID, false, false);				
				}			
		}
		#endif

}
