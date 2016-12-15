using UnityEngine;
using System;
using System.Collections;
	
public class Utils{
	public static string CreateRandomString(int _length){
	  	string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
	  	char[] chars = new char[_length];
		for (int i = 0; i < _length; i++) 
		 	 chars[i] = allowedChars[UnityEngine.Random.Range(0, allowedChars.Length)];
		return new string(chars);
	}
	
	public static bool IsStringEmpty(string str){
		for (int i = 0; i < str.Length; i++){
		  if (str[i] != ' ')
		  	 return false;
		}
		return true;
	}
	
	public static float ClampAngle(float angle, float min, float max){
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
	
	public static void StopAnimationsInLayer(Animation anim, int layer){
		foreach(AnimationState state in anim){
			if(state.layer == 3)
				anim.Stop(state.clip.name);
		}
	}
	
	public static Texture2D CreateTexture1x1(Color color){
		Texture2D TEMP_texture = new Texture2D(1, 1);
		TEMP_texture.SetPixel(0, 0, color);
		TEMP_texture.Apply();
		return TEMP_texture;
	}
	
	public static CustomInput LoadInput(){
		Settings settings = MonoBehaviour.FindObjectOfType(typeof(Settings)) as Settings;
		return settings.customInput;
	}
	
	public static void CLog(string tag, string msg, string color){
		string TEMP_string = tag + msg;
		string[] TEMP_strings = TEMP_string.Split(new Char [] {'$'});
		foreach (string str in TEMP_strings)
			Debug.Log("<color=" + color + ">" + str + "</color>");
	}
}
