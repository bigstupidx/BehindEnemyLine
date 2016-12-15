using UnityEngine;
using System.Collections;

public interface LocomotionBehaviour {
	void locomote();
}

public class SoldierLocomotion : LocomotionBehaviour {
	
	GameObject selfObject;
	GameObject Player;
	public float moveSpeed = 1.0f;
	public float rotationSpeed = 1.0f;
	
	public SoldierLocomotion (GameObject self){
		this.selfObject = self;
		Player = GameObject.FindGameObjectWithTag("Player");
		
	}
	
	public void locomote() {
		Vector3 dir = Player.transform.position - this.selfObject.transform.position;
		// Only needed if objects don't share 'z' value.
		dir.z = 0.0f;
		if (dir != Vector3.zero) 
			this.selfObject.transform.rotation = Quaternion.Slerp ( this.selfObject.transform.rotation, 
			                                                       Quaternion.FromToRotation (Vector3.right, dir), 
			                                                       rotationSpeed * Time.deltaTime);
		
		//Move Towards Target
		this.selfObject.transform.position += (Player.transform.position - this.selfObject.transform.position).normalized 
			* moveSpeed * Time.deltaTime;	}
}



public class NoLocomotion : LocomotionBehaviour {
	public void locomote() {
		return;
	}
}
