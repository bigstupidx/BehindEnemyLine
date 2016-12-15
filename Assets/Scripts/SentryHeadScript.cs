using UnityEngine;
using System.Collections;

public class SentryHeadScript : MonoBehaviour {

	public GameObject Player;
	// Use this for initialization
	void Start () {
		
		Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		//if (Vector3.Distance (gameObject.transform.position, Player.transform.position) > 300) {
			gameObject.transform.LookAt (Player.transform);
		//}
	}
}
