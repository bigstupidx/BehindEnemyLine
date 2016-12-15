using UnityEngine;
using System.Collections;

public class FPV_WeaponController : MonoBehaviour {
	public Animation animation_HAND;
	public Animation animation_WEAPON;
	
	public AnimationClip[] attackAnimations;
	public AnimationClip idleAnimation;
	public AnimationClip drawAnimation;
	public AnimationClip reloadAnimation;
	
	public ParticleEmitter[] emittOnAttack;
	
	public Vector3 normalOffset;
	public float lerp = 1.0f;
	public float weaponMove_High, weaponMove_Speed_Run, weaponMove_Speed_Walk;
	
	
	string FireAnimationName_last;
	float weaponMoveOffset;
	
	[HideInInspector]	public PlayerControllerMain owner;
	
	void Awake () {
		for(int i = 0; i < attackAnimations.Length; i++){
			animation_HAND.AddClip(attackAnimations[i], "FIRE_" + i.ToString());
			animation_WEAPON.AddClip(attackAnimations[i], "FIRE_" + i.ToString());
			
			animation_HAND["FIRE_" + i.ToString()].wrapMode = WrapMode.Once;
			animation_WEAPON["FIRE_" + i.ToString()].wrapMode = WrapMode.Once;
			
			animation_HAND["FIRE_" + i.ToString()].layer = 2;
			animation_WEAPON["FIRE_" + i.ToString()].layer = 2;
			
			animation_HAND["FIRE_" + i.ToString()].blendMode = AnimationBlendMode.Blend;
			animation_WEAPON["FIRE_" + i.ToString()].blendMode = AnimationBlendMode.Blend;
			
		}
		animation_HAND.AddClip(idleAnimation, "IDLE");
		animation_WEAPON.AddClip(idleAnimation, "IDLE");
		
		animation_HAND["IDLE"].wrapMode = WrapMode.Loop;
		animation_WEAPON["IDLE"].wrapMode = WrapMode.Loop;
		animation_HAND["IDLE"].layer = 1;
		animation_WEAPON["IDLE"].layer = 1;
		
		animation_HAND.AddClip(drawAnimation, "DRAW");
		animation_WEAPON.AddClip(drawAnimation, "DRAW");
		
		animation_HAND["DRAW"].wrapMode = WrapMode.Once;
		animation_WEAPON["DRAW"].wrapMode = WrapMode.Once;
		animation_HAND["DRAW"].layer = 2;
		animation_WEAPON["DRAW"].layer = 2;
		animation_HAND["DRAW"].blendMode = AnimationBlendMode.Blend;
		animation_WEAPON["DRAW"].blendMode = AnimationBlendMode.Blend;
		
		if(reloadAnimation){
			animation_HAND.AddClip(reloadAnimation, "RELOAD");
			animation_WEAPON.AddClip(reloadAnimation, "RELOAD");
			
			animation_HAND["RELOAD"].wrapMode = WrapMode.Once;
			animation_WEAPON["RELOAD"].wrapMode = WrapMode.Once;
			animation_HAND["RELOAD"].layer = 2;
			animation_WEAPON["RELOAD"].layer = 2;
			animation_HAND["RELOAD"].blendMode = AnimationBlendMode.Blend;
			animation_WEAPON["RELOAD"].blendMode = AnimationBlendMode.Blend;
		}
	}
	
	public void Draw(){
		animation_HAND.Play("DRAW");
		animation_WEAPON.Play("DRAW");
	}
	
	public void Idle(){
		animation_HAND.Play("IDLE");
		animation_WEAPON.Play("IDLE");
	}
	
	public void Reload(){
		animation_HAND.Stop(FireAnimationName_last);
		animation_WEAPON.Stop(FireAnimationName_last);
		
		animation_HAND.Play("RELOAD");
		animation_WEAPON.Play("RELOAD");
	}
	
	public void Attack(){
		string FireAnimationName_current = "FIRE_" + Random.Range(0, attackAnimations.Length).ToString();

		animation_HAND.Stop(FireAnimationName_last);
		animation_WEAPON.Stop(FireAnimationName_last);
		
		animation_HAND.Play(FireAnimationName_current);
		animation_WEAPON.Play(FireAnimationName_current);
		if(emittOnAttack.Length > 0){
			foreach(ParticleEmitter pe in emittOnAttack)
				pe.Emit();
		}
		FireAnimationName_last = FireAnimationName_current;
	}
	
	void LateUpdate(){
		if(!owner)
			return;
		float weaponMove_Speed_current = 0.0f;
		
		switch(owner.moveState_current){
			case PlayerControllerMain.MoveState.IDLE : 
				weaponMove_Speed_current = 0.0f;
				break;
			case PlayerControllerMain.MoveState.WALK : 
				weaponMove_Speed_current = weaponMove_Speed_Walk;
				break;
			case PlayerControllerMain.MoveState.RUN: 
				weaponMove_Speed_current = weaponMove_Speed_Run;
				break;
		}
	
		weaponMoveOffset = Mathf.Lerp(weaponMoveOffset, 
			Mathf.Sin(Time.time * weaponMove_Speed_current) * weaponMove_High / 2, Time.deltaTime * lerp);
		
		Quaternion rotation = Quaternion.Euler(owner.lookAngle, owner.transform.eulerAngles.y, 0);
		transform.rotation = rotation;
		transform.position = calcPOS(normalOffset + new Vector3(0, weaponMoveOffset, 0), rotation);
	}
	
	Vector3 calcPOS(Vector3 _offset, Quaternion _rotation){
		return _rotation * new Vector3(_offset.x, 0, -_offset.z) + owner.transform.position + new Vector3(0, _offset.y, 0);
	}
}
