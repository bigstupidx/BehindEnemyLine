using UnityEngine;
using System.Collections;

public class LevelScene : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}
	public void onAssaultButtonClick (){
		Application.LoadLevel ("stage1");
	}
	public void onDefendButtonClick (){
		Application.LoadLevel ("stage3");
	}
	public void onBackButtonClick(){
		Application.LoadLevel ("MenuScreen");
	}
	public void onSnipeEmOutClick () {
		Application.LoadLevel("stage6");
	}
}