using UnityEngine;
using System.Collections;

public class PlayerController1 : MonoBehaviour {
	public Transform nameTransform;
	public string text;
	NetworkController _nc;
	
	void Start () {
		if(networkView.isMine){
			NetworkController _nc = FindObjectOfType(typeof(NetworkController)) as NetworkController;
			networkView.RPC("TellOurStats", RPCMode.OthersBuffered, _nc.username);
		}
	}
	
	[RPC]
	void TellOurStats(string name){
		text = name;
	}

	void OnGUI(){
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(nameTransform.position);
		Vector3 cameraRelative = Camera.main.transform.InverseTransformPoint(nameTransform.position);	
		
		float distanceToCamera = Vector3.Distance(transform.position, Camera.main.transform.position);
		if (cameraRelative.z > 0 && distanceToCamera < 100){
			Rect position = new Rect(screenPosition.x, Screen.height - screenPosition.y, 200, 200);
			GUI.Label(position, text);
		}
	}

	
}
