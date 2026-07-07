using UnityEngine;
using UnityEngine.Advertisements;

public class RewardAdManager : MonoBehaviour,
    IUnityAdsInitializationListener,
    IUnityAdsLoadListener,
    IUnityAdsShowListener
{
    [SerializeField] private string androidGameId = "800080613";
    [SerializeField] private string rewardedAdUnitId = "Rewarded_Android";
    [SerializeField] private bool testMode = false;

    private bool isLoaded;

    private void Awake()
    {
        Advertisement.Initialize(androidGameId, testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("광고 초기화 완료");
        LoadRewardAd();
    }

    public void LoadRewardAd()
    {
        isLoaded = false;
        Advertisement.Load(rewardedAdUnitId, this);
    }

    public void ShowRewardAd()
    {
        if (!isLoaded)
        {
            Debug.Log("광고가 아직 로드되지 않았음");
            return;
        }

        Advertisement.Show(rewardedAdUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (adUnitId == rewardedAdUnitId)
        {
            isLoaded = true;
            Debug.Log("보상형 광고 로드 완료");
        }
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState state)
    {
        if (adUnitId == rewardedAdUnitId && state == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("광고 시청 완료 → 보상 지급");

            // 여기에 돈 지급 코드 연결
            // MoneyManager.Instance.AddMoney(3000);
        }

        LoadRewardAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"광고 초기화 실패: {error} / {message}");
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"광고 로드 실패: {error} / {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"광고 표시 실패: {error} / {message}");
        LoadRewardAd();
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
}