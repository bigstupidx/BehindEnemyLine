using UnityEngine;
using System.Collections;

[System.Serializable]
public class ScreenResolution{
	public int width = 1024;
	public int height = 576;
}

[System.Serializable]
public class CustomInput{
	public GameObject KEY_BROWSE_WEAPONS;
	public KeyCode KEY_TOGGLE_RUN_WALK;
	public KeyCode KEY_RELOAD;
	public KeyCode KEY_TRY_ATTACK;
	public KeyCode KEY_TOGGLE_VIEW;
}

public class Settings : MonoBehaviour {
	public CustomInput customInput;
	public ScreenResolution[] resolutions;
	public int startIndex;
	public float volume = 0.3f;
	
	[HideInInspector]public ScreenResolution currentResolution;
	
	void Start () {
		SetResolution(startIndex, false);
		volume = Mathf.Clamp(volume, 0.0f, 1.0f);
		AudioListener.volume = volume;
	}
	
	public void SetResolution(int index, bool fs){
		Screen.SetResolution(resolutions[index].width, resolutions[index].height, fs);
		currentResolution = resolutions[index];
	}
	
	public string GetResolutionInString(ScreenResolution input){
		return (input.width.ToString() + "x" + input.height.ToString());
	}
}
