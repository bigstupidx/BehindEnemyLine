using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_ANDROID
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
#endif

public class GP8SingletonLeaderboardsAndAchievements : SingletonBase<GP8SingletonLeaderboardsAndAchievements>
{

		private	GP8SingletonLeaderboardsAndAchievements ()
		{
		} // guarantee this will be always a singleton only - can't use the constructor!

		private  GameObject		leaderboardsAndAchievementsEventListener;
		private  GameObject		leaderboardsAndAchievementsManager;
	#if UNITY_IPHONE
		[HideInInspector]
		public List<GameCenterLeaderboard>
				_leaderboards;
		private List<GameCenterAchievement> _achievementMetadata;
	#endif

		public void Awake ()
		{
				if (GP8Constants.GAME_CENTER_IS_READY_TO_LAUNCH) {
						#if UNITY_IPHONE
						GameCenterManager.categoriesLoadedEvent += delegate(List<GameCenterLeaderboard> leaderboards) {
								this._leaderboards = leaderboards;
						};

						GameCenterManager.achievementsLoadedEvent += delegate( List<GameCenterAchievement> achievementMetadata ) {
								this._achievementMetadata = achievementMetadata;
						};
						#elif UNITY_ANDROID
						// recommended for debugging
						PlayGamesPlatform.DebugLogEnabled = true;
			
						// Activate the Google Play Games platform
						PlayGamesPlatform.Activate ();
						#endif

						GP8UtilityLeaderboardsAndAchievements.AuthenticatePlayerForLeaderboardsAndAchievements ();
						GP8Constants.GAME_CENTER_IS_READY_TO_LAUNCH = false;
				}
		}
	
}