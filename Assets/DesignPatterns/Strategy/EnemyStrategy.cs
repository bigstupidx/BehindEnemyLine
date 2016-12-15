using UnityEngine;
using System.Collections;

public abstract class EnemyStrategy : MonoBehaviour {
	
	public LocomotionBehaviour locomoteBehaviour;
	public FiringModeBehaviour firingBehaviour;
	
	public void EnemyLocomotion(){
		locomoteBehaviour.locomote ();
	}
	
	public void EnemyFire (){
		firingBehaviour.firingMode ();
	}
	
	public abstract void killThisEntity();
	
}

