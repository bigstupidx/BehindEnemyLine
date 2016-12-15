using UnityEngine;
using System.Collections;

public class MenuScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onOptionButtonClick (){
		Application.LoadLevel ("OptionScreen");
	}

	public void onQuitButtonClick (){
		Application.LoadLevel ("stage2");
	}

	public void onCreditButtonClick (){
		Application.LoadLevel ("CreditScene");
	}

	public void onArmoryButtonClick (){
		Application.LoadLevel ("ArmoryScene");
		}

	public void onPlayButtonClick (){

		Application.LoadLevel ("LoadingScreenSecond");
	}

	public void onLevelButtonClick () {

		Application.LoadLevel("Level Scene");

	}


}
