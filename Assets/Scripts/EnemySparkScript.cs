using UnityEngine;
using System.Collections;

public class EnemySparkScript : ParticleSpark {

	public GameObject enemyGun;
	Vector3 gunPos;
	Vector3 gunRot;
	EnergyBar playerHealth;

	void Start () {
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControllerMain> ().playerHealth;
	}
	
	// Update is called once per frame
	void Update () {
			gameObject.transform.localPosition = enemyGun.transform.localPosition + new Vector3 (0, -2f, 0);
			gameObject.transform.localEulerAngles = enemyGun.transform.forward;
			gunRot = enemyGun.transform.eulerAngles;
			gameObject.transform.eulerAngles = gunRot + new Vector3 (90, 0, 0);
	}

}
