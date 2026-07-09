using UnityEngine;
using UnityEngine.Advertisements;

namespace AH.Code
{
    public class AdManager : MonoBehaviour,
        IUnityAdsInitializationListener,
        IUnityAdsLoadListener,
        IUnityAdsShowListener
    {
        [SerializeField] private string androidGameId = "800080613";
        [SerializeField] private string rewardedAdUnitId = "Rewarded_Android";
        [SerializeField] private bool testMode = true;

        private bool isLoaded;

        private void Awake()
        {
            Debug.Log($"[AD] Awake - Initialize 호출. gameId={androidGameId}, testMode={testMode}");
            Advertisement.Initialize(androidGameId, testMode, this);
        }

        public void OnInitializationComplete()
        {
            Debug.Log("[AD] 초기화 완료");
            LoadRewardAd();
        }

        public void LoadRewardAd()
        {
            Debug.Log($"[AD] Load 호출. adUnitId={rewardedAdUnitId}");
            isLoaded = false;
            Advertisement.Load(rewardedAdUnitId, this);
        }

        public void ShowRewardAd()
        {
            Debug.Log($"[AD] ShowRewardAd 호출됨. isLoaded={isLoaded}");
            if (!isLoaded)
            {
                Debug.Log("[AD] 아직 로드 안됨 - Show 취소");
                return;
            }
            Advertisement.Show(rewardedAdUnitId, this);
        }

        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log($"[AD] 로드 완료: {adUnitId}");
            isLoaded = true;
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState state)
        {
            Debug.Log($"[AD] Show 완료: {state}");
            LoadRewardAd();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.LogError($"[AD] 초기화 실패: {error} / {message}");
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.LogError($"[AD] 로드 실패: {error} / {message}");
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.LogError($"[AD] Show 실패: {error} / {message}");
            LoadRewardAd();
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
            Debug.Log("[AD] Show 시작");
        }

        public void OnUnityAdsShowClick(string adUnitId) { }
    }
}