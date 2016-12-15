using UnityEngine;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class AdMobManager : MonoBehaviour
{

	
		public static BannerView bannerView;
		public static InterstitialAd interstitial;
	
		public void RequestBanner ()
		{
				#if UNITY_EDITOR
				string adUnitId = "unused";
				#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-7276724968804470/5928679542";
				#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-7276724968804470/1389231942";
				#else
		string adUnitId = "unexpected_platform";
				#endif
		
				// Create a 320x50 banner at the top of the screen.
				bannerView = new BannerView (adUnitId, AdSize.SmartBanner, AdPosition.Top);
				// Register for ad events.
				bannerView.AdLoaded += HandleAdLoaded;
				bannerView.AdFailedToLoad += HandleAdFailedToLoad;
				bannerView.AdOpened += HandleAdOpened;
				bannerView.AdClosing += HandleAdClosing;
				bannerView.AdClosed += HandleAdClosed;
				bannerView.AdLeftApplication += HandleAdLeftApplication;
				// Load a banner ad.
				bannerView.LoadAd (createAdRequest ());
		}
	
		public static void RequestInterstitial ()
		{
				#if UNITY_EDITOR
				string adUnitId = "unused";
				#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-7276724968804470/5928679542";
				#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-7276724968804470/1389231942";
				#else
		string adUnitId = "unexpected_platform";
				#endif
		
				// Create an interstitial.
				interstitial = new InterstitialAd (adUnitId);
				// Register for ad events.
				interstitial.AdLoaded += HandleInterstitialLoaded;
				interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
				interstitial.AdOpened += HandleInterstitialOpened;
				interstitial.AdClosing += HandleInterstitialClosing;
				interstitial.AdClosed += HandleInterstitialClosed;
				interstitial.AdLeftApplication += HandleInterstitialLeftApplication;
				// Load an interstitial ad.
				interstitial.LoadAd (createAdRequest ());
		}
	
		// Returns an ad request with custom ad targeting.
		private static AdRequest createAdRequest ()
		{
				return new AdRequest.Builder ()
			.AddTestDevice (AdRequest.TestDeviceSimulator)
				.AddTestDevice ("0123456789ABCDEF0123456789ABCDEF")
				.AddKeyword ("game")
				.SetGender (Gender.Male)
				.SetBirthday (new DateTime (1985, 1, 1))
				.TagForChildDirectedTreatment (false)
				.AddExtra ("color_bg", "9B30FF")
				.Build ();
		
		}
	
		public static void ShowInterstitial ()
		{
				if (interstitial.IsLoaded ()) {
						interstitial.Show ();
				} else {
						print ("Interstitial is not ready yet.");
				}
		}
	
	#region Banner callback handlers
	
		public void HandleAdLoaded (object sender, EventArgs args)
		{
				print ("HandleAdLoaded event received.");
		}
	
		public void HandleAdFailedToLoad (object sender, AdFailedToLoadEventArgs args)
		{
				print ("HandleFailedToReceiveAd event received with message: " + args.Message);
		}
	
		public void HandleAdOpened (object sender, EventArgs args)
		{
				print ("HandleAdOpened event received");
		}
	
		void HandleAdClosing (object sender, EventArgs args)
		{
				print ("HandleAdClosing event received");
		}
	
		public void HandleAdClosed (object sender, EventArgs args)
		{
				print ("HandleAdClosed event received");
		}
	
		public void HandleAdLeftApplication (object sender, EventArgs args)
		{
				print ("HandleAdLeftApplication event received");
		}
	
	#endregion
	
	#region Interstitial callback handlers
	
		public static void HandleInterstitialLoaded (object sender, EventArgs args)
		{
				print ("HandleInterstitialLoaded event received.");
		}
	
		public static void HandleInterstitialFailedToLoad (object sender, AdFailedToLoadEventArgs args)
		{
				print ("HandleInterstitialFailedToLoad event received with message: " + args.Message);
		}
	
		public static void HandleInterstitialOpened (object sender, EventArgs args)
		{
				print ("HandleInterstitialOpened event received");
		}
	
		public static void HandleInterstitialClosing (object sender, EventArgs args)
		{
				print ("HandleInterstitialClosing event received");
		}
	
		public static void HandleInterstitialClosed (object sender, EventArgs args)
		{
				print ("HandleInterstitialClosed event received");
		}
	
		public static void HandleInterstitialLeftApplication (object sender, EventArgs args)
		{
				print ("HandleInterstitialLeftApplication event received");
		}
	
	#endregion
}
