using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class UnityAdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{

    public static UnityAdsManager Instance { get; private set; }

    public string myGameIdAndroid = "5322662";
    public string myGameIdIOS = "5322663";
    public string adUnitIdAndroidtest = "Banner_Android";
    public string adUnitIdIOS = "Interstitial_iOS";

    public string GAME_ID = "5322662";
    
    public bool adStarted;
    [SerializeField] private bool testMode = true;

    [SerializeField] private bool showBanner = false;

    private const string BANNER_PLACEMENT = "banner";
    private const string REWARDED_VIDEO_PLACEMENT = "Rewarded_Android";

    [SerializeField] private BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;


    private void Awake()
    {

        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        DontDestroyOnLoad(this);

        Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {

        #if UNITY_IOS
            GAME_ID = adUnitIdIOS;
        #else
            GAME_ID = adUnitIdAndroidtest;
        #endif

    }

    public void Initialize()
    {
        if (Advertisement.isSupported)
            Advertisement.Initialize(GAME_ID, testMode, this);
        else
            Debug.Log("Error - Ads Not Supported");
    }

    public void ToggleBanner()
    {
        showBanner = !showBanner;

        if(showBanner)
        {
            Advertisement.Banner.SetPosition(bannerPosition);
            Advertisement.Banner.Show(BANNER_PLACEMENT);
        }
        else
            Advertisement.Banner.Hide(false);
    }

    public void LoadRewardedAd()
    {
        Advertisement.Load(REWARDED_VIDEO_PLACEMENT, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(REWARDED_VIDEO_PLACEMENT, this);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            ToggleBanner();
        }
 
    }


    #region Interface Implementations

    public void OnInitializationComplete() 
    {
        //Debug.Log("Init Complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Init Failed: [{error}]: {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        //Debug.Log($"Load Success: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Load Failed: [{error}:{placementId}] {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        //Debug.Log($"OnUnityAdsShowFailure: [{error}]: {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        //Debug.Log($"OnUnityAdsShowStart: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        //Debug.Log($"OnUnityAdsShowClick: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {

        //Make sure this callback is for the correct placement
        if (placementId.Equals(REWARDED_VIDEO_PLACEMENT))
        {
            // Define conditional logic for each ad completion status:
            if ( showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            { 
                if(GameMode_Survival.Instance.watchingRewardedAd)
                    GameMode_Survival.Instance.CompletedRewardedAd();
            }
            else if (showCompletionState == UnityAdsShowCompletionState.SKIPPED)
            {
                if (GameMode_Survival.Instance.watchingRewardedAd)
                    GameMode_Survival.Instance.SkippedRewardedAd();

            }
            else if (showCompletionState == UnityAdsShowCompletionState.UNKNOWN)
            {
                Debug.LogWarning("Ad is unable to finish");
            }
        }
    }

    #endregion
}
