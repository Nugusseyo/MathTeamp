using UnityEngine;
// using Unity.Services.LevelPlay;

public class LevelPlayRewardAdManager : MonoBehaviour
{
    /*[SerializeField] private string appKey = "270fb87ad";
    [SerializeField] private string rewardedAdUnitId = "7o5lwmehh05u65hz";
    [SerializeField] private bool testMode = true;

    private LevelPlayRewardedAd rewardedAd;

    private void Awake()
    {
        LevelPlay.OnInitSuccess += OnInitSuccess;
        LevelPlay.OnInitFailed += OnInitFailed;
        LevelPlay.Init(appKey);
    }

    private void OnInitSuccess(LevelPlayConfiguration config)
    {
        Debug.Log("광고 초기화 완료");
        
        LevelPlay.LaunchTestSuite();

        rewardedAd = new LevelPlayRewardedAd(rewardedAdUnitId);
        rewardedAd.OnAdLoaded += OnAdLoaded;
        rewardedAd.OnAdLoadFailed += OnAdLoadFailed;
        rewardedAd.OnAdDisplayed += OnAdDisplayed;
        rewardedAd.OnAdDisplayFailed += OnAdDisplayFailed;
        rewardedAd.OnAdRewarded += OnAdRewarded;
        rewardedAd.OnAdClosed += OnAdClosed;


        LoadRewardAd();
    }

    private void OnAdDisplayFailed(LevelPlayAdInfo info, LevelPlayAdError error)
    {
        Debug.LogError($"광고 표시 실패: {error}");
        LoadRewardAd();
    }

    private void OnInitFailed(LevelPlayInitError error)
    {
        Debug.LogError($"광고 초기화 실패: {error}");
    }

    public void LoadRewardAd()
    {
        rewardedAd?.LoadAd();
    }

    public void ShowRewardAd()
    {
        if (rewardedAd != null && rewardedAd.IsAdReady())
        {
            rewardedAd.ShowAd();
        }
        else
        {
            Debug.Log("광고가 아직 로드되지 않았음");
        }
    }

    private void OnAdLoaded(LevelPlayAdInfo adInfo)
    {
        Debug.Log("보상형 광고 로드 완료");
    }

    private void OnAdLoadFailed(LevelPlayAdError error)
    {
        Debug.LogError($"광고 로드 실패: {error}");
    }

    private void OnAdDisplayed(LevelPlayAdInfo adInfo) { }
    

    private void OnAdRewarded(LevelPlayAdInfo adInfo, LevelPlayReward reward)
    {
        Debug.Log($"광고 시청 완료 → 보상 지급: {reward.Name} x{reward.Amount}");
    }

    private void OnAdClosed(LevelPlayAdInfo adInfo)
    {
        LoadRewardAd();
    }

    private void OnDestroy()
    {
        LevelPlay.OnInitSuccess -= OnInitSuccess;
        LevelPlay.OnInitFailed -= OnInitFailed;
        rewardedAd?.DestroyAd();
    }*/
}