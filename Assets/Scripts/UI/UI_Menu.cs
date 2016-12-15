using UnityEngine;
using System.Collections;

public class UI_Menu : MonoBehaviour {
	public MenuState menuState;
	public NetworkController _nc;
	
	public enum MenuState{
		MainMenu,
		SetName,
		CreateGame,
		Settings,
		CreateCharacter,
	}
	
	void Start(){
		_nc = FindObjectOfType(typeof(NetworkController)) as NetworkController;
		menuState = MenuState.MainMenu;
	}
	
	void OnGUI () {
		GUI.depth = 1;
		switch (menuState){
			case MenuState.MainMenu : GUI_MainMenu();
				break;
			case MenuState.CreateGame : GUI_CreateGame();
				break;
		/*	case MenuState.CreateCharacter: GUI_CreateCharacter();
				break;*/
			case MenuState.SetName : GUI_SetName();
				break;
		}
	}
	
	void GUI_MainMenu(){
		GUI.BeginGroup(new Rect(50, Screen.height - 250, 300, 200));
	    GUI.Box(new Rect(0, 0, 300, 200), "Main Menu");
		
		if(GUI.Button(new Rect(50, 30, 200, 30), "Start Game"))
			menuState = MenuState.CreateGame;
		
		/*if(GUI.Button(new Rect(50, 65, 200, 30), "Create Character"))
			menuState = MenuState.CreateCharacter;*/
		
		if(GUI.Button(new Rect(50, 100, 200, 30), "Set Name"))
			menuState = MenuState.SetName;
		
		if(GUI.Button(new Rect(50, 135, 200, 30), "Quit"))
				Application.Quit();
		
		
		GUI.EndGroup();

	}
	/*
	void GUI_CreateCharacter(){
		GUI.BeginGroup(new Rect(Screen.width / 2 - 250, Screen.height/2 - 115, 500, 230));
	  	GUI.Box(new Rect(0, 0, 500, 230), "Menu");
		GUI.Label(new Rect(10, 45, 50, 20), "Level: " + _nc.playerStats.level);
		GUI.Label(new Rect(10, 70, 50, 20), "Score: " + _nc.playerStats.score);
		GUI.Label(new Rect(10, 95, 50, 20), "Exp: " + _nc.playerStats.exp);
		GUI.Label(new Rect(10, 20, 50, 20), "Name: ");
		_nc.username = GUI.TextField(new Rect(70, 20, 180, 20), _nc.username);
		
		if(!Utils.IsStringEmpty(_nc.username)){
			if(GUI.Button(new Rect(200, 180, 50, 30), "OK")){
				menuState = MenuState.MainMenu;
			}
		}
		else{
			GUI.Label(new Rect(200, 180, 180, 30), "Enter correct name!");
		}
		
		if(GUI.Button(new Rect(30, 180, 150, 30), "Back To Menu"))
			menuState = MenuState.MainMenu;
		GUI.EndGroup();
	}
	*/
	void GUI_CreateGame(){
		GUI.BeginGroup(new Rect(Screen.width / 2 - 250, Screen.height/2 - 115, 500, 230));
	  	GUI.Box(new Rect(0, 0, 500, 230), "Menu");
		GUI.Label(new Rect(10, 20, 150, 20), "Connection Settings");
		GUI.Label(new Rect(10, 45, 50, 20), "IP:");
		GUI.Label(new Rect(10, 70, 50, 20), "PORT:");
			
		_nc.connectIP = GUI.TextField(new Rect(65, 45, 150, 20), _nc.connectIP);
		_nc.connectPORT = int.Parse(GUI.TextField(new Rect(65, 70, 70, 20), _nc.connectPORT.ToString()));
			
		if(GUI.Button(new Rect(230, 45, 150, 30), "Connect"))
			_nc.Connect();
			
		GUI.Label(new Rect(10, 100, 300, 400), "Server Settings");
			
		GUI.Label(new Rect(10, 120, 50, 20), "PORT:");
		GUI.Label(new Rect(10, 145, 50, 20), "Max Pl:");
			
		_nc.serverPORT = int.Parse(GUI.TextField(new Rect(65, 120, 70, 20), _nc.serverPORT.ToString()));
		_nc.maxPlayers = int.Parse(GUI.TextField(new Rect(65, 145, 70, 20), _nc.maxPlayers.ToString()));
			
		if(GUI.Button(new Rect(230, 120, 150, 30), "Start Server"))
			_nc.StartServer();
		
		if(GUI.Button(new Rect(30, 180, 150, 30), "Back To Menu"))
			menuState = MenuState.MainMenu;
		
		GUI.EndGroup();

	}
	
	void GUI_SetName(){
		GUI.BeginGroup(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 60, 200, 120));
	    GUI.Box(new Rect(0, 0, 200, 120), "Enter Name");
		_nc.username = GUI.TextField(new Rect(10, 20, 180, 20), _nc.username);
		if(!Utils.IsStringEmpty(_nc.username)){
			if(GUI.Button(new Rect(10, 45, 50, 30), "OK")){
				PlayerPrefs.SetString("USERNAME",_nc.username);
				menuState = MenuState.MainMenu;
			}
		}
		else{
			GUI.Label(new Rect(10, 45, 180, 30), "Enter correct name!");
		}
		GUI.EndGroup();	
	}
}
