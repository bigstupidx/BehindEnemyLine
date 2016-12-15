using UnityEngine;
using System.Collections;

public class GP8SingletonAdvertisementManager : SingletonBase<GP8SingletonAdvertisementManager>
{

		private	GP8SingletonAdvertisementManager ()
		{
		} // guarantee this will be always a singleton only - can't use the constructor!
	
		private  GameObject		adsMediator;
		private  GameObject		adsEventListener;

		[HideInInspector]
		public RevMob
				revmob;
	
		public void Awake ()
		{
				this.adsMediator = Instantiate (GP8ResourceUtility.GetAdsMediatorPrefab ()) as GameObject;
				this.adsMediator.transform.parent = this.transform;

				this.adsEventListener = Instantiate (GP8ResourceUtility.GetAdsEventListenerPrefab ()) as GameObject;
				this.adsEventListener.transform.parent = this.transform;

				#if UNITY_IPHONE
				if (GP8PlayerPrefsUtility.GetAdsRemovalStatus () == GP8AdsRemovalStatus.AdsNotRemoved) {
						revmob = RevMob.Start (GP8UtilityAdvertisement.ADS_ID_REVMOB, gameObject.name);				
				}
				#endif
		}

}
