//using UnityEngine;
//using System.Collections;
//
//public class GP8SingletonMusicManager : GP8SingletonBase<GP8SingletonMusicManager>
//{
//
//		private GP8SingletonMusicManager ()
//		{
//		} // guarantee this will be always a singleton only - can't use the constructor!
//
//		private GameObject		BackgroundMusic
//		private GameObject		SoundEffects;
//		private AudioSource		audioSourceForSoundEffects;
//		private AudioSource[]		audioSourceForBackgroundMusic = new AudioSource[3];
//		private AudioClip[]		gamePlayBackgroundMusic = new AudioClip[3];
//		private string[]        gameSceneMusicFiles = new string[3];
//		private float 			initialBackgroundMusicVolume = 0.25f;
//
//		void Awake ()
//		{
//				this.BackgroundMusic = Instantiate (GP8ResourceUtility.GetBackgroundMusicPrefab ()) as GameObject;
//				this.BackgroundMusic.transform.parent = this.transform;
//	
//				this.SoundEffects = Instantiate (GP8ResourceUtility.GetSoundEffectsPrefab ()) as GameObject;
//				this.SoundEffects.transform.parent = this.transform;
//	
//				audioSourceForBackgroundMusic [0] = this.BackgroundMusic.GetComponents<AudioSource> () [0];
//				audioSourceForBackgroundMusic [1] = this.BackgroundMusic.GetComponents<AudioSource> () [1];
//				audioSourceForBackgroundMusic [2] = this.BackgroundMusic.GetComponents<AudioSource> () [2];
//				audioSourceForSoundEffects = this.SoundEffects.GetComponent<AudioSource> ();
//				gameSceneMusicFiles [0] = "Hellion";
//				gameSceneMusicFiles [1] = "water_industrial_bubble_loop";
//				gameSceneMusicFiles [2] = "F15Airflow";
//
//				gamePlayBackgroundMusic [0] = (AudioClip)Resources.Load (GP8Strings.RESOURCE_PATH_FOR_AUDIO_FILES + gameSceneMusicFiles [0]);
//				gamePlayBackgroundMusic [1] = (AudioClip)Resources.Load (GP8Strings.RESOURCE_PATH_FOR_AUDIO_FILES + gameSceneMusicFiles [1]);
//				gamePlayBackgroundMusic [2] = (AudioClip)Resources.Load (GP8Strings.RESOURCE_PATH_FOR_AUDIO_FILES + gameSceneMusicFiles [2]);
//
//				audioSourceForSoundEffects.volume = initialBackgroundMusicVolume;
//		}
//
//		void Start ()
//		{
//				//UpdateBackgroundMusic ();
//				PlayBackGroundMusic (0);
//		}
//
//
////	#region BACKGROUND MUSIC
////	
//		public void UpdateBackgroundMusic ()
//		{
//				this.PlayRaceSceneBackgroundMusic ();
//		if (GP8PlayerPrefsUtility.GetSoundPlayStatus () == GP8SoundPlayStatus.PlaySound) 
//		{
//			if (! audioSourceForBackgroundMusic.audio.isPlaying)
//					audioSourceForBackgroundMusic.audio.Play ();
//
//			if (Application.loadedLevelName == GP8Strings.RESOURCE_LEVEL_NAME_FOR_SPLASH_SCENE)
//			{
//				Invoke("PlayMainSceneBackgroundMusic", 5F);
//			}
//			
//			else if (Application.loadedLevelName == GP8Strings.RESOURCE_LEVEL_NAME_FOR_LOADING_SCENE)
//			{
//				if(constants.playRaceSceneMusic)
//				{
//					this.PlayRaceSceneBackgroundMusic ();
//
//				}
//				else
//				{
//					this.PlayMainSceneBackgroundMusic ();
//				}
//			}
//
//			if (Application.loadedLevelName == GP8Strings.RESOURCE_LEVEL_NAME_FOR_MAIN_SCENE)
//			{
//				audioSourceForBackgroundMusic.volume = initialBackgroundMusicVolume;
//				this.PlayMainSceneBackgroundMusic ();
//			}
//			else if (Application.loadedLevelName == GP8Strings.RESOURCE_LEVEL_NAME_FOR_RACE_SCENE)
//			{
//				this.PlayRaceSceneBackgroundMusic ();
//			}
//
//		} else
//			audioSourceForBackgroundMusic.audio.Stop ();
//		}
//	
//	
//	private void PlayMainSceneBackgroundMusic(){
//		
//		if(audioSourceForBackgroundMusic.clip		!= mainSceneBackgroundMusic){
//			this.StopAllCoroutines();
//			this.StartCoroutine(this.SwitchGraduallyToAudioClip(mainSceneBackgroundMusic) );			
//		}
//	}
//	
//	private void PlayRaceSceneBackgroundMusic(){		
//		
//		if(audioSourceForBackgroundMusic.clip		!= gamePlayBackgroundMusic){
//			this.StopAllCoroutines();
//			this.StartCoroutine(this.SwitchGraduallyToAudioClip(gamePlayBackgroundMusic[0]) );
//		}	
//	}
//	
//	public void PlayRaceFinishBackgroundMusic ()
//	{
//		
//		if (audioSourceForBackgroundMusic.clip != raceFinishBackgroundMusic) {
//			this.StopAllCoroutines ();
//			this.StartCoroutine (this.SwitchGraduallyToAudioClip (raceFinishBackgroundMusic));
//			
//		}	
//	}
//	
//	private void PlayRaceStartCounterBackgroundMusic ()
//	{
//		
//		if (audioSourceForBackgroundMusic.clip != raceStartCouunterBackgroundMusic) {
//			this.StopAllCoroutines ();
//			audioSourceForBackgroundMusic.PlayOneShot(raceStartCouunterBackgroundMusic, raceStartCouunterBackgroundMusic.length);
//		}	
//	}
//
//		private IEnumerator SwitchGraduallyToAudioClip (AudioClip _audioClip)
//		{
//				yield return new WaitForSeconds (0.1f);
//		
////		while (audioSourceForBackgroundMusic.volume > 0.01f) {
////			
////			audioSourceForBackgroundMusic.volume = audioSourceForBackgroundMusic.volume - 0.05f;
////			
////			yield return new WaitForSeconds (0.1f);
////			
////		}
//		
//				audioSourceForBackgroundMusic [0].clip = _audioClip;
//				audioSourceForBackgroundMusic [0].Play ();
//		
////		while (audioSourceForBackgroundMusic.volume < initialBackgroundMusicVolume) {
////			
////			audioSourceForBackgroundMusic.volume = audioSourceForBackgroundMusic.volume + 0.05f;
////			
////			yield return new WaitForSeconds (0.1f);
////			
////		}
//		}
//
//		private IEnumerator DecreaseBackgroundMusic (AudioSource _audioSource)
//		{
//				while (_audioSource.volume > 0.01f) {
//			
//						_audioSource.volume = _audioSource.volume - 0.01f;
//			
//						yield return new WaitForSeconds (0.15f);
//			
//				}
//				_audioSource.volume = 0;
//				_audioSource.Stop ();
//
//		}
//
//		private IEnumerator IncreaseBackgroundMusic (AudioSource _audioSource)
//		{
//				_audioSource.Play ();
//				while (_audioSource.volume < 0.2f) {
//			
//						_audioSource.volume = _audioSource.volume + 0.01f;
//			
//						yield return new WaitForSeconds (0.15f);
//			
//				}
//				_audioSource.volume = 0.2f;
//		
//		}
//
//		public void GraduallyDecreaseBackgroundMusic (int playOne)
//		{
//				StartCoroutine (DecreaseBackgroundMusic (audioSourceForBackgroundMusic [playOne]));
//		}
//
//		public void GraduallyIncreaseBackgroundMusic (int playOne)
//		{
//				StartCoroutine (IncreaseBackgroundMusic (audioSourceForBackgroundMusic [playOne]));
//		}
//
//
//		public void PlayBackGroundMusic (int playOne)
//		{
//				audioSourceForBackgroundMusic [playOne].volume = 0.2f;
//				audioSourceForBackgroundMusic [playOne].clip = gamePlayBackgroundMusic [playOne];
//				audioSourceForBackgroundMusic [playOne].Play ();
//		}
//
//		public void ResumeBackgroundMusic ()
//		{
////		if(audioSourceForBackgroundMusic.clip == null)
////		{
////			if (Application.loadedLevelName == GP8Strings.RESOURCE_LEVEL_NAME_FOR_MAIN_SCENE)
////			{
////				audioSourceForBackgroundMusic.volume = initialBackgroundMusicVolume;
////				this.PlayMainSceneBackgroundMusic ();
////			}
////			else if (Application.loadedLevelName == GP8Strings.RESOURCE_LEVEL_NAME_FOR_RACE_SCENE)
////			{
////				this.PlayRaceSceneBackgroundMusic ();
////			}
////		}
////		else 
////		{
////			audioSourceForBackgroundMusic.Play();
////		}
//				this.UpdateBackgroundMusic ();
//		}
//
//		public void StopBackgroundMusic (int playOne)
//		{
//				audioSourceForBackgroundMusic [playOne].Stop ();
//		}
////	
//		public void PauseBackgroundMusic ()
//		{
//				audioSourceForBackgroundMusic [0].Pause ();
//		}
////	
////	#endregion
////	
////	
////	#region SOUND EFFECTS
////	
////	
//		public void PlaySoundEffect (string _soundFileName)
//		{
////		if (GP8PlayerPrefsUtility.GetSoundPlayStatus () == GP8SoundPlayStatus.PlaySound)
////		{
//				//audioSourceForSoundEffects = this.SoundEffects.GetComponent<AudioSource> ();
//				audioSourceForSoundEffects.clip = (AudioClip)Resources.Load (GP8Strings.RESOURCE_PATH_FOR_SOUND_EFFECTS_FILES + _soundFileName);
//				audioSourceForSoundEffects.audio.Play ();
////		}
//		}
//
//		public void StopSoundEffect ()
//		{
//				//		if (GP8PlayerPrefsUtility.GetSoundPlayStatus () == GP8SoundPlayStatus.PlaySound)
//				//		{
//				//audioSourceForSoundEffects = this.SoundEffects.GetComponent<AudioSource> ();
//				//audioSourceForSoundEffects.clip = (AudioClip)Resources.Load (GP8Strings.RESOURCE_PATH_FOR_SOUND_EFFECTS_FILES + _soundFileName);
//				audioSourceForSoundEffects.audio.Stop ();
//				//		}
//		}
//
////		
////	}
////	
////	public void PlayTapSound ()
////	{
////		this.PlaySoundEffect ("PUT FILE NAME HERE");
////	}
////	
////	public void PlayUnlockSound ()
////	{
////		this.PlaySoundEffect ("PUT FILE NAME HERE");
////	}
////	
////	public void PlayCarBodySpraySound ()
////	{
////		this.PlaySoundEffect ("PUT FILE NAME HERE");
////	}
////	
////	public void PlayCarPartUpgradeSound ()
////	{
////		this.PlaySoundEffect ("C2SoundCarUpgrade");
////	}
////	
////	public void PlayPopupDropSound ()
////	{
////		this.PlaySoundEffect ("PUT FILE NAME HERE");
////	}
////	
////	public void PlayHomeScreenGoRaceSound ()
////	{
////		this.PlaySoundEffect ("PUT FILE NAME HERE");
////	}
////	
////	public void PlayCashFlowSound ()
////	{
////		this.PlaySoundEffect ("PUT FILE NAME HERE");
////	}
////	
////	public void PlayNitroSound ()
////	{
////		this.PlaySoundEffect ("PUT FILE NAME HERE");
////	}
////	
////	public void PlayRaceCounterSound ()
////	{
////		this.PlaySoundEffect ("PUT FILE NAME HERE");
////	}
////	
////	public void PlayRaceCounterGOSound ()
////	{
////		this.PlaySoundEffect ("PUT FILE NAME HERE");
////	}
////	
////	#endregion
////	
////	#region Sound Control
////	
////	public void StopAllAudioSourcesForSoundEffects ()
////	{
////		AudioSource[] allAudioSources = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];
////		
////		foreach (AudioSource source in allAudioSources) {
////			if (C2PlayerPrefsUtility.GetSoundPlayStatus () == C2SoundPlayStatus.PlaySound) {
////				if (source.clip != audioSourceForBackgroundMusic.clip)
////					source.Stop();
////			} else
////				source.Stop();
////		}
////		
////	}
////	
////	public void PauseAllAudioSources ()
////	{
////		AudioSource[] allAudioSources = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];
////		
////		foreach (AudioSource source in allAudioSources) {
////			source.Pause ();
////		}
////		
////		C2PlayerPrefsUtility.SetSoundsPlayStatusForPauseMenu (C2SoundsPlayStatusForPauseMenu.PauseSound);
////		
////	}
////	
////	public void ResumeAllAudioSources ()
////	{
////		AudioSource[] allAudioSources = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];
////		
////		foreach (AudioSource source in allAudioSources) {
////			if (C2PlayerPrefsUtility.GetSoundPlayStatus () == C2SoundPlayStatus.PlaySound)
////				if(source == audioSourceForBackgroundMusic)
////					if (Application.loadedLevelName == C2Strings.RESOURCE_LEVEL_NAME_FOR_MAIN_SCENE
////					    || Application.loadedLevelName == C2Strings.RESOURCE_LEVEL_NAME_FOR_GARAGE)
////						UpdateBackgroundMusicForHomeScene();
////			else
////				UpdateBackgroundMusicForGameScene();
////			//source.Play();
////			
////			if(C2PlayerPrefsUtility.GetSoundEffectsPlayStatus () == C2SoundEffectsPlayStatus.PlaySoundEffects)
////				if (source != audioSourceForSoundEffects && source != audioSourceForBackgroundMusic)
////					source.Play ();
////		}
////		
////		C2PlayerPrefsUtility.SetSoundsPlayStatusForPauseMenu (C2SoundsPlayStatusForPauseMenu.PlaySound);
////		
////	}
////
////	#endregion
//}
