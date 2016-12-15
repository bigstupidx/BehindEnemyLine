using UnityEngine;
using System.Collections;

public class SentryGunScript : EnemyStrategy {

	private GameObject Player;

	void Awake() {
		this.locomoteBehaviour = new NoLocomotion ();
		this.firingBehaviour = new ElectricShockFireMode (gameObject);
	}

	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		gameObject.GetComponentInChildren<ParticleSpark>().EmitParticleSystem (false);

	}

	public override void killThisEntity (){
		ParticleSystem particle = new ParticleSystem ();
		this.gameObject.AddComponent<ParticleSystem> ();
		particle.Emit (transform.position, new Vector3 (1, 5, 1), 5.5f, 2.0f, Color.grey);
		particle.startSpeed = 3.5f;
		particle.maxParticles = 100;
		StartCoroutine("hideAfterDestroy");
	}

	IEnumerator hideAfterDestroy(){
		yield return new WaitForSeconds (5.0f);
		gameObject.GetComponent<ParticleSystem> ().Stop ();
		gameObject.SetActive (false);

	}

	void Update() {
		if (Vector3.Distance (gameObject.transform.localPosition, Player.transform.localPosition) < 250) {
			this.EnemyFire();
		} 
		else{
			GetComponentInChildren<ParticleSpark> ().EmitParticleSystem (false);
		}
	}

}
