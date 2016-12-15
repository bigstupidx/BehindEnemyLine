using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {

	public Rigidbody grenade;
	public float throwForce = 10f;
	public GameObject startPoint;

	// Use this for initialization
	void Start () {
	

	}

	void throwGrenade (){

			GameObject clone;
			clone = Instantiate(grenade, startPoint.transform.position, transform.rotation) as GameObject;

			clone.rigidbody.velocity = clone.transform.TransformDirection(Vector3.forward * 50);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonUp (0)) {
			
			throwGrenade ();

		}
	}
}
