using UnityEngine;
using System;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    [SerializeField] string AppKey = "your_app_key";
    private int gameCompletedCount = 0;

    public Action OnRewardedAdSuccess; // Callback for reward success

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        //InitIronSource();

        // Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;

        // Interstitial Events
        IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;

        // Banner Events
        IronSourceBannerEvents.onAdLoadedEvent += (IronSourceAdInfo adInfo) => Debug.Log("Banner Loaded");

        // Load ads
        IronSource.Agent.loadInterstitial();
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
    }

    /*public void InitIronSource()
    {
        //IronSource.Agent.init(AppKey); // Replace with your real key
        IronSource.Agent.validateIntegration();
        IronSource.Agent.init(AppKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);
        IronSourceEvents.onSdkInitializationCompletedEvent += () => {
            Debug.Log("IronSource SDK initialized");
        };
    }*/

    // On Game Restarted
    public void OnGameRestarted()
    {
        gameCompletedCount++;
    }

    // On Game Completed
    public void OnGameCompleted()
    {
        Debug.Log("Count " + gameCompletedCount);
        if (gameCompletedCount > 0 && gameCompletedCount % 5 == 0 && IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial();
        }
    }

    #region Rewarded Video

    public void ShowRewardedAd(Action onSuccess)
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            OnRewardedAdSuccess = onSuccess;
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            Debug.Log("Rewarded video not available");
        }
    }

    private void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
        Debug.Log($"User rewarded with {placement.getRewardAmount()} {placement.getRewardName()}");

        OnRewardedAdSuccess?.Invoke(); // call the callback
        OnRewardedAdSuccess = null;    // reset it
    }

    private void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("Rewarded video closed.");
        // Optional logic after ad closes
    }

    #endregion

    #region Interstitial

    private void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("Interstitial closed, reloading...");
        IronSource.Agent.loadInterstitial();
    }

    #endregion

    #region Banner

    public void ShowBannerAd()
    {
        IronSource.Agent.displayBanner();
    }

    public void HideBannerAd()
    {
        IronSource.Agent.hideBanner();
    }

    #endregion
}
