using UnityEngine;
using System.Collections;

public class PayerCamera : MonoBehaviour {
	public bool canControl = true;
	public GameVIEW gameView;
	public Vector3 normalOffset_FPS;
	public Vector3 normalOffset_TPS;
	public LayerMask FPV_RenderLayers;
	public LayerMask TPV_RenderLayers;
	public LayerMask FPV_WeaponCamera_RenderLayers;
	public LayerMask TPV_WeaponCamera_RenderLayers;
	public Camera weaponRenderCamera;
	public float reticuleSize = 15.0f;

	public Transform target;
	Texture2D reticule;
	Vector3 offset;
	public PlayerControllerMain owner;
	Camera _camera;
	CustomInput cInput;

	private float shakeSpeed = 50f;
	private Vector3 shakeRange = new Vector3(1f,1f);
	private float shakeTimer = 0f;
	private const float shakeTime = 0.75f;
	private bool shake = false;
	private Vector3 originalPosition;

	public void shakeCamera (){

		shakeSpeed = 10f;
		Time.timeScale = 0.2f;
		shake = true;
	}
	
	public enum GameVIEW{
		FirstPersone,
		ThirdPersone,
	}
	
	void Start(){
		cInput = Utils.LoadInput();
		_camera = gameObject.GetComponent<Camera>();
		reticule = (Texture2D)Resources.Load("GUITextures/reddot");
		
		Log("Current view: " + gameView.ToString());
	}
	
	public void Update(){
		if(Input.GetKeyDown(cInput.KEY_TOGGLE_VIEW)){
			switch(gameView){
				case GameVIEW.FirstPersone :
					gameView = GameVIEW.ThirdPersone;
					break;
				
				case GameVIEW.ThirdPersone :
					gameView = GameVIEW.FirstPersone;
					break;
			}
			Log("Current view: " + gameView.ToString());
		}
	}
	
	void LateUpdate () {

		if(Input.GetKey(KeyCode.K)){
			shakeCamera();
		}

	   if (!canControl)
			return;

		if (shake) {
			if (shakeTimer > shakeTime * Time.timeScale) {
				shakeTimer = 0;
				shake = false;
				Time.timeScale = 1;
			}
			else {
				shakeTimer += Time.deltaTime;
				gameObject.transform.localPosition = gameObject.transform.localPosition + Vector3.Scale (SmoothRandom.GetVector3 (shakeSpeed--), shakeRange);
				shakeSpeed *= -1;
				shakeRange = new Vector3 (shakeRange.x * -1, shakeRange.y*-1);
			}
		}

		if (target) {
			switch(gameView){
				case GameVIEW.FirstPersone :
					offset = normalOffset_FPS;
					_camera.cullingMask = FPV_RenderLayers;
					weaponRenderCamera.cullingMask = FPV_WeaponCamera_RenderLayers;
					break;
				
				case GameVIEW.ThirdPersone :
					offset = normalOffset_TPS;
					_camera.cullingMask = TPV_RenderLayers;
					weaponRenderCamera.cullingMask = TPV_WeaponCamera_RenderLayers;
					break;
			}
			
	       	Quaternion rotation = Quaternion.Euler(owner.lookAngle, target.eulerAngles.y, 0);
			transform.rotation = rotation;
			transform.position = calcPOS(offset, rotation);
	//		Debug.DrawRay(calcPOS(normalOffset_FPS, rotation), transform.TransformDirection(Vector3.forward) * 1000 , Color.green, 0, true);

		}
	}



	Vector3 calcPOS(Vector3 _offset, Quaternion _rotation){
		return _rotation * new Vector3(_offset.x, 0, -_offset.z) + target.position + new Vector3(0, _offset.y, 0);
	}
	
	void OnPlayerSpawn(GameObject player){
		target = player.transform;
		owner = player.GetComponent<PlayerControllerMain>();
	}
	
	void OnGUI(){
		Rect position = new Rect();
		switch(gameView){
			case GameVIEW.FirstPersone :
				position = new Rect((Screen.width - reticuleSize) * 0.5f, (Screen.height - reticuleSize) * 0.5f, reticuleSize, reticuleSize);
				break;
			case GameVIEW.ThirdPersone :
				RaycastHit hit;
				if(Physics.Raycast(calcPOS(normalOffset_FPS, transform.rotation), transform.TransformDirection(Vector3.forward), out hit, 1000)){
					Vector3 screenPosition = _camera.WorldToScreenPoint(hit.point);
					Vector3 cameraRelative = _camera.transform.InverseTransformPoint(hit.point);	
					float distanceToCamera = Vector3.Distance(transform.position, Camera.main.transform.position);
					if (cameraRelative.z > 0 && distanceToCamera < 100){
						position = new Rect(screenPosition.x - reticuleSize * 0.5f, Screen.height - screenPosition.y - reticuleSize * 0.5f, reticuleSize, reticuleSize);
					}
				}
				else{
					position = new Rect((Screen.width - reticuleSize) * 0.5f, (Screen.height - reticuleSize) * 0.5f, reticuleSize, reticuleSize);
				}
				break;
		}
		GUI.DrawTexture(position, reticule);
	}
	
	void Log(string text){
		Debug.Log("[CAMERA]" + text);
	}
}
