using UnityEngine;
using System.Collections;

public class MusicManager : GP8SingletonBase<MusicManager> {

	private GameObject		BackgroundMusic;
	private AudioSource		audioSourceForBackgroundMusic;
	private AudioClip[]		gamePlayBackgroundMusic = new AudioClip[3];
	private string[]        gameSceneMusicFiles = new string[3];
	private float 			initialBackgroundMusicVolume = 0.6f;



	public MusicManager(){
		//explicily defining default constructor
	}

	// Use this for initialization
	void Awake () {
	
		this.BackgroundMusic = Instantiate (GP8ResourceUtility.GetBackgroundMusicPrefab ()) as GameObject;
		this.BackgroundMusic.transform.parent = this.transform;
		audioSourceForBackgroundMusic = this.BackgroundMusic.GetComponent<AudioSource> ();
		gameSceneMusicFiles [0] = "the_revelation";
		gameSceneMusicFiles [1] = "cod";
		gameSceneMusicFiles [2] = "commies";
		gamePlayBackgroundMusic [0] = (AudioClip)Resources.Load (GP8Strings.RESOURCE_PATH_FOR_AUDIO_FILES + gameSceneMusicFiles [0]);
		gamePlayBackgroundMusic [1] = (AudioClip)Resources.Load (GP8Strings.RESOURCE_PATH_FOR_AUDIO_FILES + gameSceneMusicFiles [1]);
		gamePlayBackgroundMusic [2] = (AudioClip)Resources.Load (GP8Strings.RESOURCE_PATH_FOR_AUDIO_FILES + gameSceneMusicFiles [2]);
		audioSourceForBackgroundMusic.volume = initialBackgroundMusicVolume;
	}

	void Start() {
		playBackgroundMusic (0);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void playBackgroundMusic (int index){

		audioSourceForBackgroundMusic.clip = gamePlayBackgroundMusic[index];
		audioSourceForBackgroundMusic.Play ();
	}


	public void crossFadeMusic(){

	}


	private IEnumerator DecreaseBackgroundMusic (AudioSource _audioSource)
	{
		while (_audioSource.volume > 0.01f) {
			
			_audioSource.volume = _audioSource.volume - 0.01f;
			
			yield return new WaitForSeconds (0.15f);
			
		}
		_audioSource.volume = 0;
		_audioSource.Stop ();
		
	}
	
	private IEnumerator IncreaseBackgroundMusic (AudioSource _audioSource)
	{
		_audioSource.Play ();
		while (_audioSource.volume < 0.2f) {
			
			_audioSource.volume = _audioSource.volume + 0.01f;
			
			yield return new WaitForSeconds (0.15f);
			
		}
		_audioSource.volume = 0.2f;
		
	}
	
	public void GraduallyDecreaseBackgroundMusic (int playOne)
	{
		StartCoroutine (DecreaseBackgroundMusic (audioSourceForBackgroundMusic));
	}
	
	public void GraduallyIncreaseBackgroundMusic (int playOne)
	{
		StartCoroutine (IncreaseBackgroundMusic (audioSourceForBackgroundMusic));
	}

}
