using UnityEngine;
using System.Collections;

public static class GP8PlayerPrefsUtility {

	public static void SetAdsRemovalStatus(GP8AdsRemovalStatus adsRemovalStatus)
	{
		PlayerPrefs.SetInt (GP8Strings.PREF_KEY_ADS_REMOVAL_STATUS, (int)adsRemovalStatus);
	}

	public static GP8AdsRemovalStatus GetAdsRemovalStatus()
	{
		return (GP8AdsRemovalStatus) PlayerPrefs.GetInt (GP8Strings.PREF_KEY_ADS_REMOVAL_STATUS);
	}
}
