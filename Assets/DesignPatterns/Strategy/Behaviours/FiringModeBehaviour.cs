using UnityEngine;
using System.Collections;


public interface FiringModeBehaviour {
	
	void firingMode();
}

public class gunFireMode : FiringModeBehaviour {
	GameObject selfObject;
	GameObject player;
	Rigidbody projectile;
	float speed = 120;
	
	public gunFireMode(GameObject self){
		this.selfObject = self;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	public void firingMode() {
		Rigidbody instantiatedProjectile =(Rigidbody) Rigidbody.Instantiate( projectile, selfObject.transform.position, selfObject.transform.rotation ); 
		instantiatedProjectile.velocity = selfObject.transform.TransformDirection(new Vector3( 0, 0, speed ) );
		Physics.IgnoreCollision( instantiatedProjectile. collider, selfObject.transform.root.collider );
	}
}


public class ElectricShockFireMode : FiringModeBehaviour {
	GameObject selfObject;
	GameObject player;
	Rigidbody projectile;
	float speed = 120;
	float laserWidth = 1.0f;
	float noise = 1.0f;
	float maxLength = 50.0f;
	Color color = Color.red;
	LineRenderer lineRenderer;
	int length;
	Vector3[] position;
	Transform myTransform;
	Transform endEffectTransform;
	ParticleSystem endEffect;
	Vector3 offset;
	
	public ElectricShockFireMode(GameObject self){
		this.selfObject = self;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	public void firingMode() {
		Initialization ();
		RenderLaser ();
	}
	
	void Initialization () {
		lineRenderer = new LineRenderer ();
		lineRenderer.SetWidth(laserWidth, laserWidth);
		offset = new Vector3(0,0,0);
		endEffect = new ParticleSystem ();
		myTransform = selfObject.transform;
		if(endEffect)
			endEffectTransform = endEffect.transform;
	}
	
	
	void RenderLaser(){
		
		//Shoot our laserbeam forwards!
		UpdateLength();
		
		lineRenderer.SetColors(color,color);
		for(int i = 0; i<length; i++){
			//Set the position here to the current location and project it in the forward direction of the object it is attached to
			offset.x =myTransform.position.x+i*myTransform.forward.x+Random.Range(-noise,noise);
			offset.z =i*myTransform.forward.z+Random.Range(-noise,noise)+myTransform.position.z;
			position[i] = offset;
			position[0] = myTransform.position;	
			lineRenderer.SetPosition(i, position[i]);
			
		}
	}
	
	void UpdateLength(){
		RaycastHit[] hit;
		hit = Physics.RaycastAll(myTransform.position, myTransform.forward, maxLength);
		int i = 0;
		while(i < hit.Length){
			if(!hit[i].collider.isTrigger)
			{
				length = (int)Mathf.Round(hit[i].distance)+2;
				position = new Vector3[length];
				if(endEffect){
					endEffectTransform.position = hit[i].point;
					if(!endEffect.isPlaying)
						endEffect.Play();
				}
				lineRenderer.SetVertexCount(length);
				return;
			}
			i++;
		}
		if(endEffect){
			if(endEffect.isPlaying)
				endEffect.Stop();
		}
		length = (int)maxLength;
		position = new Vector3[length];
		lineRenderer.SetVertexCount(length);
		
	}
}

