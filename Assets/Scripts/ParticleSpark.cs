using UnityEngine;
using System.Collections;

public class ParticleSpark : MonoBehaviour {
	

	public void EmitParticleSystem (bool emit){
		gameObject.GetComponent<ParticleSystem> ().enableEmission = emit;
	}
	

	void OnParticleCollision(GameObject other) {
		Rigidbody body = other.GetComponent<Rigidbody>();
		if (body) {
			if(other.GetComponent<PlayerControllerMain>())
				other.GetComponent<PlayerControllerMain>().takingDamage();
			Vector3 direction = other.transform.position - transform.position;
			direction = direction.normalized;
			body.AddForce (direction * 5000, ForceMode.Force);
		} 
		
	}
}
