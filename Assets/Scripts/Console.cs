using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Console : MonoBehaviour {
	public FPS_Calculator fpsCalculator;
	public KeyCode toggleKey = KeyCode.BackQuote;
 	public GUIStyle textStyle;
	public Color backGroundColor;
	
	List<ConsoleMessage> messages = new List<ConsoleMessage>();
	bool showUI_Console;
	bool showUI_Stats;
	
	Vector2 scrollPosition = Vector2.zero;
	Texture2D backGroundTexture;
	
	public struct ConsoleMessage{
		public string message;
		public string stackTrace;
		public LogType type;
		
		public ConsoleMessage (string message, string stackTrace, LogType type){
			this.message = message;
			this.stackTrace	= stackTrace;
			this.type = type;
		}
	}

	
	void OnEnable () { Application.RegisterLogCallback(HandleLog); }
	void OnDisable () { Application.RegisterLogCallback(null); }
	
	
	void Awake(){
		backGroundTexture = Utils.CreateTexture1x1(backGroundColor);
	}
	
	void Start(){
		Utils.CLog("", "Debug", "green");
		foreach (GameObject go in FindObjectsOfType(typeof (GameObject)))
				go.SendMessage("OnConsoleStart", SendMessageOptions.DontRequireReceiver);
		
		fpsCalculator.FPS_Start();
	}
	
	void Update (){
		fpsCalculator.FPS_Update();
		if (Input.GetKeyDown(toggleKey))
			showUI_Console = !showUI_Console;
	}
	
	void OnGUI(){
		if(showUI_Console)
			UI_Console();
		UI_Stats();
	}
	
	void UI_Console(){
		GUI.depth = 0;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height * 0.75f), backGroundTexture);
		scrollPosition = GUI.BeginScrollView(new Rect(10, 10, Screen.width - 10, Screen.height * 0.75f - 10), 
			scrollPosition, new Rect(10, 10, Screen.width - 10, 10 + messages.Count * 12), false, false);
		for(int i = 0; i < messages.Count; i++)
			GUI.Label(new Rect(10, 10 + i*12, Screen.width - 10, 12), messages[i].message, textStyle);
		
		GUI.EndScrollView();
	}
	
	void UI_Stats(){
		GUI.Label(new Rect(10, Screen.height - 65, 300, 20), fpsCalculator.output, textStyle);
	}
	
	
	void HandleLog (string message, string stackTrace, LogType type){
		ConsoleMessage newMessage = new ConsoleMessage(message, stackTrace, type);
		messages.Add(newMessage);
	}
}

[System.Serializable]
public class FPS_Calculator{
	public float updateInterval = 0.5F;
	public string output;
	
	float accum   = 0;
	int frames  = 0;
	float timeleft; 
	float fps; 
	
	public void FPS_Start(){
	   timeleft = updateInterval;  
	}
	 
	public void FPS_Update(){
	    timeleft -= Time.deltaTime;
	    accum += Time.timeScale/Time.deltaTime;
	    ++frames;
	 
	    if(timeleft <= 0.0){
			fps = accum/frames;
			output = "FPS: " + "<color=" + SelectColor() + ">" + fps.ToString("f1") + "</color>";
			timeleft = updateInterval;
	        accum = 0.0F;
	        frames = 0;
		}
	}
	
	string SelectColor(){
		if(fps < 30)
			return "red";
		if(fps >= 30 && fps < 60)
			return "yellow";
		if(fps >= 60)
			return "lime";
		return "wight";
	}
}		