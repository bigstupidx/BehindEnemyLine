using UnityEngine;
using System.Collections;


public class AK47 : selectedPrimaryWeapon {
	
	public AK47(){
		name = "AK47";
		FiringMode = "burst";
		description = "oldest but finest klashinikov";
		price = 200.0f;
		precision = 0.04f;
	}
	
	public override GameObject mount3DModel (){
		GameObject path_to_model = (GameObject)Resources.Load ("Prefabs/Weapons/FPV/v_" + this.name);
		return GameObject.Instantiate (path_to_model, Player.transform.position, Player.transform.rotation) as GameObject;
	}
	
	public GameObject associatedSoundResource ()
	{
		return (GameObject)Resources.Load("Sounds/Weapons/" + name + "/singleshot1.wav") ;
	}
	
}
