using UnityEngine;
using System.Collections;

public class TPV_WeaponController : MonoBehaviour {
	public ParticleEmitter[] emittOnAttack;
	public Light lightOnShot;
	
	public void Attack(){
		if(emittOnAttack.Length > 0){
			foreach(ParticleEmitter pe in emittOnAttack)
				pe.Emit();
		}
		if(lightOnShot)
			StartCoroutine("EnableLight");
	}
	
	IEnumerator EnableLight(){
		lightOnShot.enabled = true;
		yield return new WaitForSeconds(0.05f);
		lightOnShot.enabled = false;
	}
}
