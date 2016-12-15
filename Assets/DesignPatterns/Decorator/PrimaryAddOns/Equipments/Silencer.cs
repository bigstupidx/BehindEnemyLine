using UnityEngine;
using System.Collections;


public class Silencer : selectedAddOnsPrimaryWeapon {
	
	selectedPrimaryWeapon primaryWeapon;
	
	public Silencer(selectedPrimaryWeapon primaryWeapon){
		this.primaryWeapon = primaryWeapon;
	}
	
	public override float getPrecision(){
		return this.primaryWeapon.getPrecision () - 0.1f;
	}
	
	public override string getDescription() {
		return this.primaryWeapon.getDescription() + " Silencer causes less detection and important for sneak in infiltration";
	}
	
	public override float getPrice() {
		return this.primaryWeapon.getPrice () + 60.0f;
	}
	
	public override GameObject mount3DModel (){
		GameObject path_to_model = (GameObject)Resources.Load ("Prefabs/Weapons/FPV/v_" + this.name);
		return GameObject.Instantiate (path_to_model, Player.transform.position, Player.transform.rotation) as GameObject;
	}
	
}

