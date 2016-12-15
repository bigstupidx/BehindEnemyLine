using UnityEngine;
using System.Collections;

[System.Serializable]
public class wSlot{
	public Transform wBone;
	public Transform bBone;
	
	[HideInInspector]	public bool enable;
	[HideInInspector]	public int ID;
	[HideInInspector]	public string wName;
	[HideInInspector]	public GameObject weaponGO_TPV;
	[HideInInspector]	public GameObject weaponGO_FPV;
	[HideInInspector]	public FPV_WeaponController FPV_WController;
	[HideInInspector]	public TPV_WeaponController TPV_WController;
	[HideInInspector]	public float attackRate;
	[HideInInspector]	public float rateTime;
	[HideInInspector]	public float reloadTime;
	public int clipSize;
 
	public AudioClip attackSound;
}

[System.Serializable]
public class UpperAnimations{
	[System.Serializable]
	public class AnimationData_Main{
		public AnimationClip motionClip;
		public PlayerControllerMain.MoveState moveState;
	}
	
	[System.Serializable]
	public class AnimationData_Blend{
		public BlendAnimStateType stateType;
		public AnimationClip motionClip;
		public AnimationBlendMode blendMode;
		public float weight = 1.0f;
		public Transform[] mixWith;
	}
	
	[System.Serializable]
	public class UpperBodyMain{
		public string elementName = "element";
		public int weaponID;
		public Weapons.wSlotsType weaponSlot;
		public AnimationData_Main[] motions;
	}
	
	[System.Serializable]
	public class UpperBodyBlend{
		public string elementName = "element";
		public int weaponID;
		public Weapons.wSlotsType weaponSlot;
		public AnimationData_Blend[] motions;
	}
		
	public enum BlendAnimStateType{
		ATTACK,
		RELOAD,
	}
	public UpperBodyMain[] upperBodyMains;
	public UpperBodyBlend[] upperBodyBlend;
	public Hashtable blendWeightsThresholds = new Hashtable();
}

[System.Serializable]
public class MotionData{
	public PlayerControllerMain.MotionDirection motionDirection;
	public PlayerControllerMain.MoveState moveState;
	public AnimationClip motionClip;
}

public class PlayerControllerMain : MonoBehaviour {
	public GameObject damageTexture;
	public GameObject healthBar;
	public EnergyBar playerHealth;
	public EnergyBar playerAmmo;
	public Joystick joyStickLeft;
	public Joystick joyStickRight;
	public Vector3 normalOffset_FPS;
	public Animation _anim;
	public UpperAnimations upperAnimations;
	public MotionData[] motionData;
	public Transform[] bodyLookSegments;
	public wSlot[] wSlots = new wSlot[3];
	public float mouseXSens = 1.0f, mouseYSens = 1.0f;
	public float lookAngle;
	public float lookAngleThreshold = 10;
	public Transform upperBody;
	public AudioSource weaponAudioSourse;
	public AudioClip emptyClip;
	public AudioClip resultAudio;
	public Camera pauseCamera;
	public UISprite reloadSprite;

	bool shouldRun = false;

	float ak_valueCurrent = 10;
	float pis_valueCurrent = 10;
	Vector3 offset;
	Vector3 targetVelocity;
	float weaponChangeTime, weaponReloadTime;
	CustomInput cInput;

	
	[HideInInspector]	public float h, v, mouseX, mouseY;
	[HideInInspector]	public MotionDirection motionState_current, motionState_last;
	[HideInInspector]	public MoveState moveState_current, moveState_last;
	[HideInInspector]	public Weapons.wSlotsType WType_current, WType_last;
	
	[HideInInspector]	public AnimationState UppreBodyState_current;
	[HideInInspector]	public AnimationState MainBodyState_current;
	
	public enum MotionDirection{
		IDLE,
		FWD,
		FWD_LEFT,
		FWD_RIGHT,
		LEFT,
		RIGHT,
		BACK,
		BACK_LEFT,
		BACK_RIGHT,
	}
	
	public enum MoveState{
		IDLE,
		WALK,
		RUN,
	}
	
	void Awake(){
		gameObject.name = "PlayerGO_LOCAL"; //networkView.isMine? "PlayerGO_LOCAL" : "PlayerGO_REMOTE";
		_anim.cullingType = AnimationCullingType.AlwaysAnimate; //networkView.isMine? AnimationCullingType.AlwaysAnimate : AnimationCullingType.BasedOnRenderers; 

		if(joyStickLeft ==null){
			GameObject objjoyStickLeft = GameObject.FindGameObjectWithTag("leftJoyStick") as GameObject;
			joyStickLeft = objjoyStickLeft.GetComponent<Joystick>();
		}
		
		if(joyStickRight==null){
			GameObject objjoyStickRight = GameObject.FindGameObjectWithTag("rightJoyStick") as GameObject;
			joyStickRight = objjoyStickRight.GetComponent<Joystick>();
		}

		foreach(MotionData mData in motionData){
			_anim.AddClip(mData.motionClip, mData.moveState.ToString() + "_" + mData.motionDirection.ToString());
			_anim[mData.moveState.ToString() + "_" + mData.motionDirection.ToString()].layer = 1;
			_anim[mData.moveState.ToString() + "_" + mData.motionDirection.ToString()].wrapMode = WrapMode.Loop;
		}

		foreach(UpperAnimations.UpperBodyMain uBodyMain in upperAnimations.upperBodyMains){
			foreach(UpperAnimations.AnimationData_Main aData in uBodyMain.motions){
				string motionName = aData.moveState.ToString()+ "_" + uBodyMain.weaponSlot.ToString() + "_" + uBodyMain.weaponID;
				_anim.AddClip(aData.motionClip, motionName);
				_anim[motionName].layer = 2;
				_anim[motionName].blendMode = AnimationBlendMode.Blend;
				_anim[motionName].wrapMode = WrapMode.Loop;
				_anim[motionName].AddMixingTransform(upperBody);
			}
		}
		
		foreach(UpperAnimations.UpperBodyBlend uBodyBlend in upperAnimations.upperBodyBlend){
			foreach(UpperAnimations.AnimationData_Blend aData in uBodyBlend.motions){
				string motionName = aData.stateType.ToString()+ "_" + uBodyBlend.weaponSlot.ToString() + "_" + uBodyBlend.weaponID;
				_anim.AddClip(aData.motionClip, motionName);
				_anim[motionName].layer = 3;
				_anim[motionName].blendMode = aData.blendMode;
				_anim[motionName].wrapMode = WrapMode.Once;
				upperAnimations.blendWeightsThresholds.Add(motionName, aData.weight);
				foreach(Transform mixTransform in aData.mixWith)
					_anim[motionName].AddMixingTransform(mixTransform);
			}
		}
		
		_anim.SyncLayer(1);
		_anim.SyncLayer(2);
		
		if(/*networkView.isMine*/ true){
			cInput = Utils.LoadInput();
			foreach (GameObject go in FindObjectsOfType(typeof (GameObject)))
				go.SendMessage("OnPlayerSpawn", gameObject, SendMessageOptions.DontRequireReceiver);

			AddWeapon1((int)Weapons.wSlotsType.M, 0);
			
			AddWeapon1((int)Weapons.wSlotsType.M, 1);
			AddWeapon1((int)Weapons.wSlotsType.P, 0);
			AddWeapon1((int)Weapons.wSlotsType.S, 0);
			
//			networkView.RPC("SelectWeaponInSlot_TPV", RPCMode.AllBuffered, (int)Weapons.wSlotsType.M, (int)Weapons.wSlotsType.M);
			SelectWeaponInSlot_FPV((int)Weapons.wSlotsType.M, (int)Weapons.wSlotsType.M);
		}
		
		ApplyLowerBodyAnimation(motionState_current, moveState_current);
		ApplyUpperBodyAnimation(moveState_current, WType_current, wSlots[(int)WType_current].ID);
	}

	void Start(){

		playerHealth = healthBar.GetComponent<EnergyBar> ();
		InvokeRepeating ("DecreaseDamage", 1.0f, 1);

		
	}
	
	void Update(){
		if(/*networkView.isMine*/ true){
		

			float xMovement = 0;
			float yMovement = 0;
			float xmouse = 0;
			float ymouse = 0;
			// this is to move the cube
			if(joyStickLeft.position.x<0) { 		
				xMovement = xMovement - 0.6f; 	
			} 	
			
			if(joyStickLeft.position.x>0) {
				xMovement = xMovement + 0.6f;
			}
			
			if(joyStickLeft.position.y<0) { 	
				yMovement = yMovement - 0.6f; 	
			} 		
			
			if(joyStickLeft.position.y>0) {
				yMovement = yMovement + 0.6f;
				
			}

			if(joyStickRight.position.x > 0){

				xmouse = xmouse + 0.2f;
			}

			if(joyStickRight.position.x < 0){

				xmouse = xmouse - 0.2f;
			}

			if(joyStickRight.position.y < 0){

				ymouse = ymouse - 0.2f;
			}
			if(joyStickRight.position.y > 0){
				ymouse = ymouse + 0.2f;
			}
			
			// to rotate the cube
			float rotatePos = 0;//joyStickInput(joyStickRight);
			transform.Rotate(0, rotatePos * 0.3f, 0);

#if UNITY_EDITOR

			h = Input.GetAxis("Horizontal");
			v = Input.GetAxis("Vertical");
			mouseX = Input.GetAxis("Mouse X");
			mouseY = Input.GetAxis("Mouse Y");

#endif

#if UNITY_ANDROID && !UNITY_EDITOR

			h = xMovement;
			v = yMovement;
			mouseX = xmouse;
			mouseY = ymouse;



#endif

			transform.Rotate(0, mouseX * mouseXSens, 0);	
			lookAngle -= mouseY * mouseYSens;
			
			lookAngle = Utils.ClampAngle(lookAngle, -lookAngleThreshold, lookAngleThreshold);
			
//			if(Input.GetKeyDown(cInput.KEY_BROWSE_WEAPONS)){
//				switch(WType_current){
//					case Weapons.wSlotsType.M : 
//						if(wSlots[(int)Weapons.wSlotsType.P].enable){
//							WType_current = Weapons.wSlotsType.P;
//						}
//						else {
//							if(wSlots[(int)Weapons.wSlotsType.S].enable){
//								WType_current = Weapons.wSlotsType.S;
//							}
//							else {
//								WType_current = Weapons.wSlotsType.M;
//							}
//						}
//						break;
//					case Weapons.wSlotsType.P : 
//						if(wSlots[(int)Weapons.wSlotsType.S].enable){
//							WType_current = Weapons.wSlotsType.S;
//						}
//						else{
//							WType_current = Weapons.wSlotsType.M;
//						}
//						break;
//					case Weapons.wSlotsType.S : 
//						WType_current = Weapons.wSlotsType.M;
//						break;
//				}
//				
//				weaponReloadTime = Time.fixedTime;
//				weaponChangeTime = Time.fixedTime + 1.15f;
////				networkView.RPC("SelectWeaponInSlot_TPV", RPCMode.AllBuffered, (int)WType_current, (int)WType_last);
//				SelectWeaponInSlot_FPV((int)WType_current, (int)WType_last);
//			}
			
//			if(Input.GetKey(cInput.KEY_TRY_ATTACK) && Time.fixedTime > weaponChangeTime && Time.fixedTime > weaponReloadTime){
//				if(Time.fixedTime >= wSlots[(int)WType_current].rateTime){
//					wSlots[(int)WType_current].rateTime = Time.fixedTime + wSlots[(int)WType_current].attackRate;
////					networkView.RPC("AttackAnimation", RPCMode.All);
//					weaponAudioSourse.PlayOneShot(wSlots[(int)WType_current].attackSound);
//					wSlots[(int)WType_current].FPV_WController.Attack();
//				}
//			}
			
			if(Input.GetKeyDown(cInput.KEY_RELOAD) &&  Time.fixedTime > weaponReloadTime){
				if(WType_current != Weapons.wSlotsType.M){
					weaponReloadTime = Time.fixedTime + wSlots[(int)WType_current].reloadTime;
//					networkView.RPC("ReloadAnimation", RPCMode.All);
					wSlots[(int)WType_current].FPV_WController.Reload();
				}
			}
			
			motionState_current = UpdateDirection();
			
			
			if(motionState_current != MotionDirection.IDLE)
				moveState_current = shouldRun ? MoveState.WALK : MoveState.RUN; 
			else
				moveState_current = MoveState.IDLE;
		}
		
		if(motionState_last != motionState_current){
			motionState_last = motionState_current;
			ApplyLowerBodyAnimation(motionState_current, moveState_current);
			ApplyUpperBodyAnimation(moveState_current, WType_current, wSlots[(int)WType_current].ID);
		}
		
		if(moveState_last != moveState_current){
			moveState_last = moveState_current;
			ApplyLowerBodyAnimation(motionState_current, moveState_current);
			ApplyUpperBodyAnimation(moveState_current, WType_current, wSlots[(int)WType_current].ID);
		}
		
		if(WType_last != WType_current){
			WType_last = WType_current;
			ApplyUpperBodyAnimation(moveState_current, WType_current, wSlots[(int)WType_current].ID);
		}
	}
	
	void FixedUpdate () {
		if(/*networkView.isMine*/ true){
			showDamageTex();
			targetVelocity = new Vector3(h, 0, v);
			if(targetVelocity.sqrMagnitude > 1.0f)
				targetVelocity.Normalize();
			
			targetVelocity = transform.TransformDirection(targetVelocity);
			
			targetVelocity *= moveState_current == MoveState.RUN ? 4.0f : moveState_current == MoveState.WALK ? 2.0f : 0.0f;
			
			Vector3 velocity = rigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			
			velocityChange.x = Mathf.Clamp(velocityChange.x, -10.0f, 10.0f);
	        velocityChange.z = Mathf.Clamp(velocityChange.z, -10.0f, 10.0f);
	        velocityChange.y = 0;

			rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			
			rigidbody.AddForce(new Vector3 (0, Physics.gravity.y * rigidbody.mass, 0));
 		}


#if UNITY_EDITOR
		if (Input.GetMouseButton (0)) {
			onFireClick();
		}
#endif
		MainBodyState_current.normalizedTime = UppreBodyState_current.normalizedTime;
	}

	void LateUpdate(){
		foreach(Transform segment in bodyLookSegments){
			segment.RotateAround(segment.position, transform.right, lookAngle / bodyLookSegments.Length);
		}
	}
	
	void ApplyLowerBodyAnimation(MotionDirection mDir, MoveState mState){
		string temp_animName = mState + "_" + mDir.ToString();
		_anim.CrossFade(temp_animName);
		MainBodyState_current = _anim[temp_animName];
	}
	
	void ApplyUpperBodyAnimation(MoveState mState, Weapons.wSlotsType wType, int wID){
		string temp_animName = mState.ToString() + "_" + wType.ToString() + "_" + wID;
		_anim.CrossFade(temp_animName);
		UppreBodyState_current = _anim[temp_animName];
	}
	
	public void AddWeapon1(int slotType, int ID){
		WeaponData newWD = new WeaponData();
		newWD = Weapons.GetDataByID_Fast(ID, (Weapons.wSlotsType)slotType);
		
		wSlots[(int)slotType].enable = true;
		wSlots[(int)slotType].wName = newWD.name;
		wSlots[(int)slotType].ID = ID;
		wSlots[(int)slotType].attackRate = newWD.attackRate;
		wSlots[(int)slotType].reloadTime = newWD.reloadTime;
//		if(wSlots[(int)slotType].weaponGO_TPV)
//			Network.Destroy(wSlots[(int)slotType].weaponGO_TPV);
//		networkView.RPC("InstantiateWeapon_TPV", RPCMode.AllBuffered, newWD.path_TPV, (int)slotType, ID, newWD.path_Sound_Attack);
		
		if(!Utils.IsStringEmpty(newWD.path_FPV)){
			InstantiateWeapon_FPV(newWD.path_FPV, (int)slotType , newWD.path_Sound_Attack);
		}
	}
	
	[RPC]
	void SelectWeaponInSlot_TPV(int slotType, int slotType_last){
		if(wSlots[slotType].enable){
			weaponAudioSourse.Stop();
			Utils.StopAnimationsInLayer(_anim, 3);
			if(wSlots[slotType_last].weaponGO_TPV)
				MoveAndParentTo(wSlots[slotType_last].bBone, wSlots[slotType_last].weaponGO_TPV);
			if(wSlots[slotType].weaponGO_TPV)
				MoveAndParentTo(wSlots[slotType].wBone, wSlots[slotType].weaponGO_TPV);
		}
	}
	
	[RPC]
	void AttackAnimation(){
		string aStateShoot_Name = "ATTACK_" + WType_current.ToString() + "_" + wSlots[(int)WType_current].ID;
		_anim.Stop(aStateShoot_Name);
		_anim.Play(aStateShoot_Name);
		_anim[aStateShoot_Name].weight = (float)upperAnimations.blendWeightsThresholds[aStateShoot_Name];
		if(wSlots[(int)WType_current].TPV_WController)
			wSlots[(int)WType_current].TPV_WController.Attack();
		weaponAudioSourse.Stop();
		weaponAudioSourse.PlayOneShot(wSlots[(int)WType_current].attackSound);

	}
	
	[RPC]
	void ReloadAnimation(){
		string aStateReload_Name = "RELOAD_" + WType_current.ToString() + "_" + wSlots[(int)WType_current].ID;
		_anim.Play(aStateReload_Name);
		_anim[aStateReload_Name].weight = (float)upperAnimations.blendWeightsThresholds[aStateReload_Name];
	}
	
	[RPC]
	void InstantiateWeapon_TPV(string path, int slotType, int ID, string attackSound){
		if(!Utils.IsStringEmpty(path)){
			GameObject weaponGO = Instantiate(Resources.Load(path), 
				wSlots[(int)slotType].bBone.position, 
				wSlots[(int)slotType].bBone.rotation)  as GameObject;
		
			weaponGO.transform.parent = wSlots[(int)slotType].bBone;
			wSlots[(int)slotType].weaponGO_TPV = weaponGO;
			wSlots[(int)slotType].TPV_WController = weaponGO.GetComponent<TPV_WeaponController>();
			weaponGO.SendMessage("SetupRenderer", SendMessageOptions.DontRequireReceiver);
		}
	
		wSlots[(int)slotType].ID = ID;
		wSlots[(int)slotType].attackSound = (AudioClip)Resources.Load(attackSound);
		wSlots[(int)slotType].enable = true;
		
	}
	
	void SelectWeaponInSlot_FPV(int slotType, int slotType_last){
		if(wSlots[slotType].enable){
			wSlots[slotType_last].weaponGO_FPV.SetActive(false);
			wSlots[slotType].weaponGO_FPV.SetActive(true);
			wSlots[slotType].FPV_WController.Idle();
			wSlots[slotType].FPV_WController.Draw();
		}
	}
	
	void InstantiateWeapon_FPV(string path, int slotType , string attackSound){
		wSlots[slotType].FPV_WController = null;
		Destroy(wSlots[slotType].weaponGO_FPV);
		GameObject weaponGO = Instantiate(Resources.Load(path), transform.position, transform.rotation) as GameObject;
		
		wSlots[slotType].weaponGO_FPV = weaponGO;
		wSlots[slotType].FPV_WController = weaponGO.GetComponent<FPV_WeaponController>();
		wSlots[slotType].FPV_WController.owner = gameObject.GetComponent<PlayerControllerMain>();
		weaponGO.GetComponent<SetUpRenderers>().SetupViewWeaponRenderer();
		wSlots[slotType].weaponGO_FPV.SetActive(false);
		wSlots[(int)slotType].attackSound = (AudioClip)Resources.Load(attackSound);

	}
	
	void MoveAndParentTo(Transform _tr, GameObject _go){
		_go.transform.position = _tr.position;
		_go.transform.rotation = _tr.rotation;
		_go.transform.parent = _tr;
	}
	
	public MotionDirection UpdateDirection(){
		if(h > 0){
			if(v == 0)
				return MotionDirection.RIGHT;
			if(v > 0)
				return MotionDirection.FWD_RIGHT;
			if(v < 0)
				return MotionDirection.BACK_RIGHT;
		}
		
		if(h < 0){
			if(v == 0)
				return MotionDirection.LEFT;
			if(v > 0)
				return MotionDirection.FWD_LEFT;
			if(v < 0)
				return MotionDirection.BACK_LEFT;
		}
		
		if(h == 0){
			if(v == 0)
				return MotionDirection.IDLE;
			if(v > 0)
				return MotionDirection.FWD;
			if(v < 0)
				return MotionDirection.BACK;
		}
		return MotionDirection.IDLE;
	}
//
//	void OnGUI(){
//		if (/*networkView.isMine*/ true){
//			int i = 0;
//			foreach(wSlot sd in wSlots){
//				if(!sd.enable)
//					break;
//				GUI.Label(new Rect(10, 10 + i*25, 150, 25), sd.wName + " " + sd.ID);
//				i++;
//			}
//			GUI.Label(new Rect(10,150,100,20) , "left jostick: " + joyStickLeft.position);
//		}
//	}

 

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
        Vector3 temp_pos = Vector3.zero;
		Quaternion temp_rot = Quaternion.identity;
		
		int temp_motionState = 0; 
		int temp_moveState = 0;
		int temp_slotType_sync = 0;
		
		float temp_lookAngle = 0;
		
		if (stream.isWriting) {
            temp_pos = transform.position;
			temp_rot = transform.rotation;
			
			temp_motionState = (int)motionState_current;
			temp_moveState = (int)moveState_current;
          	temp_slotType_sync = (int)WType_current;
			
			temp_lookAngle	= lookAngle;
			
			stream.Serialize(ref temp_pos);
			stream.Serialize(ref temp_rot);
			
			stream.Serialize(ref temp_motionState);
			stream.Serialize(ref temp_moveState);
			stream.Serialize(ref temp_slotType_sync);
			
			stream.Serialize(ref temp_lookAngle);

        } else {
            stream.Serialize(ref temp_pos);
			stream.Serialize(ref temp_rot);
			
			stream.Serialize(ref temp_motionState);
			stream.Serialize(ref temp_moveState);
          	stream.Serialize(ref temp_slotType_sync);
			
			stream.Serialize(ref temp_lookAngle);

			transform.position = temp_pos;
			transform.rotation = temp_rot;
			
			motionState_current = (MotionDirection)temp_motionState;
			moveState_current = (MoveState)temp_moveState;
			WType_current = (Weapons.wSlotsType)temp_slotType_sync;

			lookAngle = temp_lookAngle;
		}
    }


	public void onChangeWeapon (){

		//if(Input.GetKeyDown(cInput.KEY_BROWSE_WEAPONS)){
			switch(WType_current){
			case Weapons.wSlotsType.M : 
				if(wSlots[(int)Weapons.wSlotsType.P].enable){
					WType_current = Weapons.wSlotsType.P;
				}
				else {
					if(wSlots[(int)Weapons.wSlotsType.S].enable){
						WType_current = Weapons.wSlotsType.S;
					}
					else {
						WType_current = Weapons.wSlotsType.M;
					}
				}
				break;
			case Weapons.wSlotsType.P : 
				if(wSlots[(int)Weapons.wSlotsType.S].enable){
					WType_current = Weapons.wSlotsType.S;
				}
				else{
					WType_current = Weapons.wSlotsType.M;
				}
				break;
			case Weapons.wSlotsType.S : 
				WType_current = Weapons.wSlotsType.M;
				break;
			}
		playerAmmo.SetValueMax (wSlots [(int)WType_current].clipSize);
		playerAmmo.valueCurrent = playerAmmo.valueMax;
		weaponReloadTime = Time.fixedTime;
			weaponChangeTime = Time.fixedTime + 1.15f;
			//				networkView.RPC("SelectWeaponInSlot_TPV", RPCMode.AllBuffered, (int)WType_current, (int)WType_last);
			SelectWeaponInSlot_FPV((int)WType_current, (int)WType_last);
		//}



	}

	public void onFireClick() {
		if (playerAmmo.valueCurrent != 0) {
			if (Time.fixedTime > weaponChangeTime && Time.fixedTime > weaponReloadTime) {
				if (Time.fixedTime >= wSlots [(int)WType_current].rateTime) {
					wSlots [(int)WType_current].rateTime = Time.fixedTime + wSlots [(int)WType_current].attackRate;
					//					networkView.RPC("AttackAnimation", RPCMode.All);
					weaponAudioSourse.PlayOneShot (wSlots [(int)WType_current].attackSound);
					wSlots [(int)WType_current].FPV_WController.Attack ();
					if ((int)WType_current != 0)
						playerAmmo.valueCurrent --;
					Quaternion rotation = Quaternion.Euler (this.lookAngle, transform.eulerAngles.y, 0);
					transform.rotation = rotation;
					offset = normalOffset_FPS;

					RaycastHit hit;
					if (Physics.Raycast (calcPOS (normalOffset_FPS, transform.rotation), transform.TransformDirection (Vector3.forward), out hit, 1000)) {
						if(hit.collider.tag == "Objective"){
							showResultScreen("Mission Accomplished");
						}

						if (hit.collider.tag == "Enemy") {
							if ((int)WType_current != 0) {
								hit.collider.gameObject.GetComponent<EnemyStrategy> ().killThisEntity ();
							} else {
								if (Vector3.Distance (gameObject.transform.localPosition, hit.collider.gameObject.transform.localPosition) < 2) {
									hit.collider.gameObject.GetComponent<EnemyStrategy> ().killThisEntity ();

								}
							}
						}
					}
				}
			}
		} else {
			reloadSprite.GetComponent<TweenColor>().enabled = true;
			weaponAudioSourse.PlayOneShot(emptyClip);
		}
	}

	Vector3 calcPOS(Vector3 _offset, Quaternion _rotation){
		return _rotation * new Vector3(_offset.x, 0, -_offset.z) + transform.position + new Vector3(0, _offset.y, 0);
	}
	public void onReloadClick () {

		if(Time.fixedTime > weaponReloadTime){
			if(WType_current != Weapons.wSlotsType.M){
				weaponReloadTime = Time.fixedTime + wSlots[(int)WType_current].reloadTime;
				//					networkView.RPC("ReloadAnimation", RPCMode.All);
				wSlots[(int)WType_current].FPV_WController.Reload();

				playerAmmo.valueCurrent = playerAmmo.valueMax;
				reloadSprite.GetComponent<TweenColor>().enabled = false;
				reloadSprite.color = Color.white;
			}
		}
		
	}
	public void takingDamage(){

		if (playerHealth.valueCurrent <= playerHealth.valueMin) {
			showResultScreen("Mission Failed");
			return;
		} else {
			playerHealth.valueCurrent --;
		}
	}

	void  showResultScreen (string text){
		pauseCamera.GetComponent<AudioSource> ().Stop ();
		pauseCamera.GetComponent<AudioSource> ().clip = resultAudio;
		pauseCamera.GetComponent<AudioSource> ().Play ();
		pauseCamera.GetComponent<AudioSource> ().volume = 1.0f;
		pauseCamera.GetComponent<PauseMenuScript> ().showResultScreen (text);
	}

	void DecreaseDamage (){
		if (playerHealth.valueCurrent < playerHealth.valueMax)
			playerHealth.valueCurrent ++;
	}

	void showDamageTex(){

		if (playerHealth.valueCurrent < 5) {
			Color ColorT = damageTexture.GetComponent<UITexture> ().color;
			ColorT.a = 4.0f;
			damageTexture.GetComponent<UITexture> ().color = ColorT;
		}

		if (playerHealth.valueCurrent < 20 && playerHealth.valueCurrent > 0) {
			Color ColorT = damageTexture.GetComponent<UITexture> ().color;
			ColorT.a = 0.8f;
			damageTexture.GetComponent<UITexture> ().color = ColorT;
		}
		if (playerHealth.valueCurrent < 30 && playerHealth.valueCurrent > 20 ) {
			Color ColorT = damageTexture.GetComponent<UITexture> ().color;
			ColorT.a = 0.3f;
			damageTexture.GetComponent<UITexture> ().color = ColorT;
		} 
		if(playerHealth.valueCurrent > 30) {
			Color ColorT = damageTexture.GetComponent<UITexture> ().color;
			ColorT.a = 0.0f;
			damageTexture.GetComponent<UITexture> ().color = ColorT;
		}
	
	}
	public void changeMoveSpeed (){

		if (shouldRun) {

			shouldRun = false;
		} else {

			shouldRun = true;
		}

	}
	
}
