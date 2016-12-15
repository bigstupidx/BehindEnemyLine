using UnityEngine;
using System.Collections;
using Prime31;

public class GP8SingletonInAppStoreKitManager : SingletonBase<GP8SingletonInAppStoreKitManager>
{

		private GP8SingletonInAppStoreKitManager ()
		{
		} // guarantee this will be always a singleton only - can't use the constructor!

		private bool readyForRequests = false;
	
		private void Awake ()
		{		
				#if UNITY_IPHONE || UNITY_ANDROID
				IAP.init (GP8UtilityInAppStoreKit.KEY_FOR_ANDROID_PURCHASES);

				IAP.requestProductData (GP8UtilityInAppStoreKit.INAPP_STORE_KIT_IDS, GP8UtilityInAppStoreKit.INAPP_STORE_KIT_IDS, productList =>
				{
						if (productList != null) {
								Debug.Log ("Product list received");
								this.setReadyForRequests ();
						} else {
								Debug.Log ("productListRequestFailedEvent:");
						}
				});
				#endif
		}
	
		public void setReadyForRequests ()
		{
				readyForRequests = true;
		}
	
		public bool isReadyForRequests ()
		{
				return readyForRequests;
		}

}
