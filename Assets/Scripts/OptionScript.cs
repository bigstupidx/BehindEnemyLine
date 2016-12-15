using UnityEngine;
using System.Collections;

public class OptionScript : MonoBehaviour {
	public UISprite onOffSprite;
	public UISprite onOffSprite1;
	
	public bool isSoundOn = true;
	public bool isMusicOn = true;

	//public UISprite onOffSprite;
	//bool isSoundOn = true;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		}

		public void onBackButtonClick1(){
		Application.LoadLevel ("MenuScreen");
		}
	public void onSpriteClick (){
		if (isSoundOn) {
			isSoundOn = false;
			onOffSprite.spriteName = "off";
		} else {
			isSoundOn = true;
			onOffSprite.spriteName = "on";	
			
		}
	}
	
	public void onSpriteClick1 (){
		if (isMusicOn) {
			isMusicOn = false;
			onOffSprite1.spriteName = "off";
		} else {
			isMusicOn = true;
			onOffSprite1.spriteName = "on";	
			
		}
	}}

	//public void onSpriteClick (){
			//	if (isSoundOn) {

					//	onOffSprite.spriteName = "off";


