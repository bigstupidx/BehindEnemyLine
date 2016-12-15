using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

	public bool firstLoading;
	// Use this for initialization
	void Start () {
	 	
		Invoke ("loadScene", 5.0f);

	}

	public void loadScene (){

		if (firstLoading) {
			Application.LoadLevel ("MenuScreen");
		} else {
			Application.LoadLevel("stage1");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	 
}
