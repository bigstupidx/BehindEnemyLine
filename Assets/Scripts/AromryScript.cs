using UnityEngine;
using System.Collections;

public class AromryScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp ("Jump")) {
			create ();
		}

	
	}

	public void create(){

		selectedPrimaryWeapon primary = new AK47 ();
		Debug.Log (primary.getPrice ());
		primary = new AimLaser (primary);
		Debug.Log (primary.getPrice ());

	}

	public void onBackButtonClick(){
		Application.LoadLevel ("MenuScreen");
	}

}
