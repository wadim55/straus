using UnityEngine;
using System.Collections;
//using GoogleMobileAds.Api;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class UnityAds : MonoBehaviour {

	public static UnityAds instance;
	public int coinReward = 50;
	public AudioClip soundReward;
	//private BannerView bannerView;
	//private RewardedAd rewardedAd;
	//private InterstitialAd interstitial;
	int loadinst = 3;

	public void Start()
	{

		instance = this;
		// Initialize the Google Mobile Ads SDK.
		//MobileAds.Initialize(initStatus => { });
		this.RequestBanner();
		RequestInterstitial();


		////this.rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");

		//// Called when an ad request has successfully loaded.
		//this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
		//// Called when an ad request failed to load.
		//this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
		//// Called when an ad is shown.
		//this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
		//// Called when an ad request failed to show.
		//this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
		//// Called when the user should be rewarded for interacting with the ad.
		//this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
		//// Called when the ad is closed.
		//this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

		//// Create an empty ad request.
		//AdRequest request = new AdRequest.Builder().Build();
		//// Load the rewarded ad with the request.
		//this.rewardedAd.LoadAd(request);
	}

	//public void HandleRewardedAdLoaded(object sender, System.EventArgs args)
	//{
	//	MonoBehaviour.print("HandleRewardedAdLoaded event received");
	//}

	//public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
	//{
	//	MonoBehaviour.print(
	//		"HandleRewardedAdFailedToLoad event received with message: "
	//						 + args.Message);
	//}

	//public void HandleRewardedAdOpening(object sender, System.EventArgs args)
	//{
	//	MonoBehaviour.print("HandleRewardedAdOpening event received");
	//}

	//public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
	//{
	//	MonoBehaviour.print(
	//		"HandleRewardedAdFailedToShow event received with message: "
	//						 + args.Message);
	//}

	//public void HandleRewardedAdClosed(object sender, System.EventArgs args)
	//{
	//	MonoBehaviour.print("HandleRewardedAdClosed event received");
	//}

	//public void HandleUserEarnedReward(object sender, Reward args)
	//{
	//	string type = args.Type;
	//	double amount = args.Amount;
	//	MonoBehaviour.print(
	//		"HandleRewardedAdRewarded event received for "
	//					+ amount.ToString() + " " + type);
	//}

	private void RequestBanner() // this method is used to request the banner ads
	{
#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-3940256099942544/6300978111";  // ad ids here
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/6300978111"; // ad ids here
#else
		string adUnitId = "unexpected_platform";
#endif

		// un comment these lines of code after google plugin import
		//// Create a 320x50 banner at the top of the screen.
		//this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

		//// Create an empty ad request.
		//AdRequest request = new AdRequest.Builder().Build();

		//// Load the banner with the request.
		//this.bannerView.LoadAd(request);
		//this.bannerView.Show();

		//// Called when an ad request has successfully loaded.
		//this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
	}

	public void HandleOnAdLoaded(object sender, System.EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received");
		print("i am ad");
	}

	private void RequestInterstitial()
	{
#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#else
		string adUnitId = "unexpected_platform";
#endif

		// un comment these lines of code after google plugin import
		//// Initialize an InterstitialAd.
		//this.interstitial = new InterstitialAd(adUnitId);

		//// Create an empty ad request.
		//AdRequest request = new AdRequest.Builder().Build();
		//// Load the interstitial with the request.
		//this.interstitial.LoadAd(request);
	}

	public void GameOver()
	{
		// un comment these lines of code after google plugin import
		//if (interstitial.IsLoaded()) { 
		//	this.interstitial.Show();
		//	RequestInterstitial();
		//} 
	}


	// un comment these lines of code after google plugin import in order to use reward ads
	//	public void ShowRewardVideo(){

	//		if (this.rewardedAd.IsLoaded())
	//		{
	//			this.rewardedAd.Show();
	//			// Create an empty ad request.
	//			AdRequest request = new AdRequest.Builder().Build();
	//			// Load the rewarded ad with the request.
	//			this.rewardedAd.LoadAd(request);
	//		}
	//#if UNITY_ADS
	//		ShowRewardedAd ();
	//#else
	//		Debug.Log("You must enable Unity Ads in Services to able watch video rewards");
	//		#endif 
	//	}

#if UNITY_ADS
	public void ShowNormalAd()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}


	private void ShowRewardedAd()
		{
		if (Advertisement.IsReady("rewardedVideo"))
			{
				var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
			}
	}
	



		private void HandleShowResult(ShowResult result)
		{
			switch (result)
			{
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");

			GameManager.Instance.SavedStars += coinReward;
			SoundManager.PlaySfx (soundReward);

				break;
			case ShowResult.Skipped:
				Debug.Log("The ad was skipped before reaching the end.");
				break;
			case ShowResult.Failed:
				Debug.LogError("The ad failed to be shown.");
				break;
			}
	}
#endif
}
