using UnityEngine;
using System.Collections;

public class AnimationEventReceiver : MonoBehaviour {
	public AudioSource footStepsSoundSource;
	
	public AudioClip[] footStepsClips;
	
	void FootStep() {
		footStepsSoundSource.PlayOneShot(footStepsClips[Random.Range(0, footStepsClips.Length)]);
	}
}
