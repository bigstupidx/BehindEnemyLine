using UnityEngine;
using System.Collections;

#region IN-APPS

[System.Serializable]
public enum GP8InAppPurchasePackageType
{
	PackageTypeNone				= -1,
	PackageTypeUtility			= 0,
	PackageTypeHotCake			= 1,
	PackageTypeTreasureHunt		= 2,
	PackageTypeExecutiveCall	= 3,
	PackageTypeRemoveAds		= 4,
	PackageTypeRestorePurchases	= 5,
	PackageTypeUnlockFullGame 	= 6
	
}
;


[System.Serializable]
public enum GP8InAppPurchaseStatus
{
	InAppPurchaseFailedBecauseInternetNotReachable		= 0,
	InAppPurchaseFailed  								= 1,
	InAppPurchaseCompletedForAds						= 2,
	InAppPurchaseCompletedForCash						= 3,
	InAppPurchaseCompletedForUnlockFullGame				= 4,
	InAppPurchaseCancelled  						    = 5,
}
;


[System.Serializable]
public enum GP8AdsRemovalStatus
{
	AdsNotRemoved				= 0,
	AdsRemoved					= 1
	
}
;

#endregion


#region GAME-SETTINGS

[System.Serializable]
public enum GP8SoundPlayStatus
{
	PlaySound					= 0,
	StopSound					= 1
}
;

[System.Serializable]
public enum GP8SoundEffectsPlayStatus
{
	PlaySoundEffects			= 0,
	StopSoundEffects			= 1	
}
;

[System.Serializable]
public enum GP8SoundsPlayStatusForPauseMenu
{
	PlaySound					= 0,
	PauseSound					= 1	
}
;

[System.Serializable]
public enum GP8SteeringStatus
{
	Automatic					= 0,
	Manual						= 1
}
;

[System.Serializable]
public enum GP8AccelarationStatus
{
	Automatic					= 0,
	Manual						= 1
}
;

#endregion
