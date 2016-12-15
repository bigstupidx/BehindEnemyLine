using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onPlayButtonClicked (){
		Application.LoadLevel ("PracticeClass2");
		}

	public void onCreditsButtonClick (){
		Application.LoadLevel ("CreditsScreen");
	    }

}
