using UnityEngine;
using System.Collections;

public class EnemyMovement : EnemyStrategy {


	private Animator myAnimator;
	private GameObject Player;
	public GameObject playerRoot;
	public Vector3 normalOffset_FPS;
	public GameObject bulletTracerOrigin;
	public GameObject bulletTracers;

	void Awake () {
		this.locomoteBehaviour = new SoldierLocomotion(gameObject);
		this.firingBehaviour = new gunFireMode (gameObject);
	}

	void Start () {
	
		myAnimator = gameObject.GetComponent<Animator> ();
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	void FixedUpdate(){


		float distance = Vector3.Distance (Player.transform.localPosition, transform.localPosition)*10;

		gameObject.transform.LookAt (Player.transform.localPosition);

		if(distance < 1000 && distance > 500){
			gameObject.transform.localEulerAngles += new Vector3 (0, 48, 0);
			this.EnemyFire();
			if (myAnimator.GetFloat ("shouldFireIdle") == 0) {
				updateAnimatorFloats(0,0,1,0,0);
			} 
		}

		if(distance < 500 && distance > 400){
			GetComponentInChildren<ParticleSpark> ().EmitParticleSystem(false);

			if (myAnimator.GetFloat ("shouldWalk") == 0) {
				updateAnimatorFloats(0,1,0,0,0);
			}
			else{
				this.EnemyLocomotion();
			}
		}

		if(distance < 400 && distance > 300){
			if (myAnimator.GetFloat ("ShouldAim") == 0) {
				updateAnimatorFloats(1,0,0,0,0);
			} 
		}

		if(distance < 300){
			gameObject.transform.localEulerAngles += new Vector3 (0, 45, 0);
			this.firingBehaviour.firingMode();
			if (myAnimator.GetFloat ("shouldWalkAgain") == 0) {
				updateAnimatorFloats(0,0,0,1,0);
			} 
			else{
				this.EnemyLocomotion();
			}

		}

		if (distance > 1000) {
			GetComponentInChildren<ParticleSpark> ().EmitParticleSystem(false);
			if (myAnimator.GetFloat ("shouldIdleAgain") == 0) {
				updateAnimatorFloats(0,0,0,0,1);
			} 
		}
	}


	public override void killThisEntity (){
		GetComponentInChildren<ParticleSpark> ().EmitParticleSystem(false);
		gameObject.GetComponent<AudioSource> ().mute = true;
		myAnimator.SetFloat("ShouldDie" , 1);
	}

 	IEnumerator idleMovement(){
	
		while (true) {
			yield return new WaitForSeconds (25.0f);
			myAnimator.SetFloat ("shouldIdleReload", 1);
			yield return new WaitForSeconds(10.0f);
			myAnimator.SetFloat("shouldIdleReload" , 0);

		}
	
		}

	
	void updateAnimatorFloats(float shouldAim, float shouldWalk, float shouldFireIdle, float shouldWalkAgain, float shouldIdleAgain) {

		myAnimator.SetFloat ("shouldFireIdle", shouldFireIdle);
		myAnimator.SetFloat ("shouldWalk", shouldWalk);
		myAnimator.SetFloat ("ShouldAim", shouldAim);
		myAnimator.SetFloat ("shouldWalkAgain", shouldWalkAgain);
		myAnimator.SetFloat ("shouldIdleAgain", shouldIdleAgain);

	}
}
