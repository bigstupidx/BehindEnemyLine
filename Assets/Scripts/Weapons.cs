using UnityEngine;
using System.Collections;

public class WeaponData{
	public string name;
	public string path_TPV;
	public string path_FPV;
	public string path_Sound_Attack;
	public float attackRate;
	public float reloadTime;
	
}

public class Weapons{
	public enum wSlotsType{
		M,
		P,
		S,
	}
	
	public static WeaponData GetDataByID_Fast(int id, wSlotsType slotType){
		WeaponData newWeaponData = new WeaponData();
		switch(slotType){
			case wSlotsType.M : 
				switch(id){
					case 0: 
					newWeaponData.name = "Hands";
					newWeaponData.path_TPV = "";
					newWeaponData.path_FPV = "Prefabs/Weapons/FPV/v_Knife";
					newWeaponData.path_Sound_Attack = "Sounds/Weapons/Knife/attack";
					
					newWeaponData.attackRate = 1.5f;
					newWeaponData.reloadTime = 0;
					return newWeaponData;
					
					case 1: 
					newWeaponData.name = "Knife";
					newWeaponData.path_TPV = "Prefabs/Weapons/TPV/Knife";
					newWeaponData.path_FPV = "Prefabs/Weapons/FPV/v_Knife";
					newWeaponData.path_Sound_Attack = "Sounds/Weapons/Knife/attack";
					
					newWeaponData.attackRate = 1.5f;
					newWeaponData.reloadTime = 0;
					return newWeaponData;
				}
			return null;
			case wSlotsType.P : 
				switch(id){
					case 0: 
					newWeaponData.name = "AK-47";
					newWeaponData.path_TPV = "Prefabs/Weapons/TPV/AK47";
					newWeaponData.path_FPV = "Prefabs/Weapons/FPV/v_AK47";
					newWeaponData.path_Sound_Attack = "Sounds/Weapons/AK47/singleshot1";
					
					newWeaponData.attackRate = 0.115f;
					newWeaponData.reloadTime = 3.0f;
					return newWeaponData;
					
					case 1: 
					newWeaponData.name = "M4A1";
					newWeaponData.path_TPV = "Prefabs/Weapons/TPV/M4";
					newWeaponData.path_FPV = "Prefabs/Weapons/FPV/v_AK47";
				
					newWeaponData.attackRate = 0.13f;
					newWeaponData.reloadTime = 3.0f;
					return newWeaponData;
					
					case 2: 
					newWeaponData.name = "AWP";
					newWeaponData.path_TPV = "Prefabs/Weapons/TPV/Awp";
					
					newWeaponData.attackRate = 0.3f;
					newWeaponData.reloadTime = 1.0f;
					return newWeaponData;
				}
			return null;
			case wSlotsType.S : 
				switch(id){
					case 0: 
					newWeaponData.name = "Glock18";
					newWeaponData.path_TPV = "Prefabs/Weapons/TPV/Glock18";
					newWeaponData.path_FPV = "Prefabs/Weapons/FPV/v_glock18";
					newWeaponData.path_Sound_Attack = "Sounds/Weapons/Glock18/shot1";
					
					newWeaponData.attackRate = 0.21f;	
					newWeaponData.reloadTime = 2.7f;
					return newWeaponData;
				}
			return null;
		}
		return null;
	}
}
