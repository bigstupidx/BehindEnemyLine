using UnityEngine;
using System.Collections;


public class mp15 : selectedPrimaryWeapon {
	
	public mp15(){
		name = "mp15";
		FiringMode = "burst";
		description = "american origin most popular semiauto rifle";
		price = 400.0f;
		precision = 0.055f;
	}

	public override float getPrecision(){
		return  precision;
	}
	
	public override string getDescription() {
		return description;
	}

	public override GameObject mount3DModel (){
		GameObject path_to_model = (GameObject)Resources.Load ("Prefabs/Weapons/FPV/v_" + this.name);
		return GameObject.Instantiate (path_to_model, Player.transform.position, Player.transform.rotation) as GameObject;
	}
	
	public GameObject associatedSoundResource ()
	{
		return (GameObject)Resources.Load("Sounds/Weapons/" + name + "/singleshot1.wav");
	}
	
}

