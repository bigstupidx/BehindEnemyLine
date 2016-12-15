using UnityEngine;
using System.Collections;

public class SplashScript : MonoBehaviour {

	GP8SingletonBase<MusicManager> manager;

	// Use this for initialization
	void Start () {
		manager = MusicManager.Instance;
		AdMobManager.ShowInterstitial ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
