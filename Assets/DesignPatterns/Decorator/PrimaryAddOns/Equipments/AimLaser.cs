using UnityEngine;
using System.Collections;


public class AimLaser : selectedAddOnsPrimaryWeapon {
	
	selectedPrimaryWeapon primaryWeapon;
	
	public AimLaser(selectedPrimaryWeapon primaryWeapon){
		this.primaryWeapon = primaryWeapon;
	}
	
	public override float getPrecision(){
		return this.primaryWeapon.getPrecision () + 0.2f;
	}
	
	public override string getDescription() {
		return this.primaryWeapon.getDescription() + " Armed with Lasor";
	}
	
	public override float getPrice() {
		return this.primaryWeapon.getPrice () + 40.0f;
	}
	
	public override GameObject mount3DModel (){
		GameObject path_to_model = (GameObject)Resources.Load ("Prefabs/Weapons/FPV/v_" + this.name);
		return GameObject.Instantiate (path_to_model, Player.transform.position, Player.transform.rotation) as GameObject;
	}
	
}

