using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnimationEventData{
	public AnimationClip motionClip;
	public EventData[] events;
	
	[System.Serializable]
	public class EventData{
		public float eventTime;
		public string functionName;
	}
}

public class AnimationPrepare : MonoBehaviour {
	public AnimationEventData[] animationEvents;
	
	void Start () {
		foreach(AnimationEventData aEventData in animationEvents){
			foreach(AnimationEventData.EventData eventData in aEventData.events){
				AnimationEvent newAnimationEvent = new AnimationEvent();
				newAnimationEvent.time = eventData.eventTime;
				newAnimationEvent.functionName = eventData.functionName;
				aEventData.motionClip.AddEvent(newAnimationEvent);
			}
		}
	}
}
