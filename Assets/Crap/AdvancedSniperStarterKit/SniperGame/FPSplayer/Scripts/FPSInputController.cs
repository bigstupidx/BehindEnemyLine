using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FPSController))]


public class FPSInputController : MonoBehaviour
{
	private GunHanddle gunHanddle;
	private FPSController FPSmotor;
	public TouchScreenVal touchMove;
	public TouchScreenVal touchAim;
	public TouchScreenVal touchShoot;
	public TouchScreenVal touchZoom;
	public TouchScreenVal touchJump;
	public Texture2D ImgButton;
	public float TouchSensMult = 0.05f;

	
	void Start(){
		Application.targetFrameRate = 60;
		MouseLock.MouseLocked = true;

	}
	void Awake ()
	{
		FPSmotor = GetComponent<FPSController> ();		
		gunHanddle = GetComponent<GunHanddle> (); 
	}

	void Update ()
	{
		
		// Get the input vector from kayboard or analog stick
		#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		
		
		FPSmotor.Aim(new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y")));
		FPSmotor.Move (new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical")));
		FPSmotor.Jump (Input.GetButton ("Jump"));
		
		if(Input.GetKey(KeyCode.LeftShift)){
			FPSmotor.Boost(1.4f);	
		}
		
		FPSmotor.Holdbreath(1);	
		
		if(Input.GetKey(KeyCode.LeftShift)){
			FPSmotor.Holdbreath(0);	
		}
		if(Input.GetButton("Fire1")){
			gunHanddle.Shoot();	
		}
		if(Input.GetButtonDown("Fire2")){
			gunHanddle.Zoom();	
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0){
			gunHanddle.ZoomAdjust(-1);	
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0){
			gunHanddle.ZoomAdjust(1);	
		}
		if(Input.GetKeyDown(KeyCode.R)){
			gunHanddle.Reload();
		}
		if(Input.GetKeyDown(KeyCode.Q)){
			gunHanddle.SwitchGun();
		}
		if(Input.GetKeyDown(KeyCode.Z)){
			gunHanddle.OffsetAdjust(new Vector2(0,-1));
		}
		if(Input.GetKeyDown(KeyCode.X)){
			gunHanddle.OffsetAdjust(new Vector2(0,1));
		}
		if(Input.GetKeyDown(KeyCode.C)){
			gunHanddle.OffsetAdjust(new Vector2(-1,0));
		}
		if(Input.GetKeyDown(KeyCode.V)){
			gunHanddle.OffsetAdjust(new Vector2(1,0));
		}
		
		
		// For Mobile Devices
		#else
		
		
		Vector2 aimdir = touchAim.OnDragDirection(true);
		FPSmotor.Aim(new Vector2(aimdir.x,-aimdir.y)*TouchSensMult);
		Vector2 touchdir = touchMove.OnTouchDirection (false);
		FPSmotor.Move (new Vector3 (touchdir.x, 0, touchdir.y));
		
		FPSmotor.Jump (Input.GetButton ("Jump"));
		
		if(touchShoot.OnTouchPress()){
			gunHanddle.Shoot();	
		}
		if(touchZoom.OnTouchRelease()){
			gunHanddle.Zoom();
		}
		
		#endif
	}
	
	
	void OnGUI(){
		#if !UNITY_EDITOR && !UNITY_WEBPLAYER && !UNITY_STANDALONE_WIN && !UNITY_STANDALONE_OSX
		touchMove.Draw();
		touchAim.Draw();
		touchShoot.Draw();
		touchZoom.Draw();
		#endif
	}
}
