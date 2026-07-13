using System;
using JYG._Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AH.Code
{
    [CreateAssetMenu(fileName=("new SceneManager"),menuName="Managers/SceneManager")]
    public class SceneManager : ManagerBase
    {
        public event Action<string> OnSceneLoadStarted;
        public event Action<string> OnSceneLoaded;
        public event Action<string> OnSceneUnloaded;
        
        public string CurrentSceneName { get; private set; }
        public bool IsLoading { get; private set; }
        
        private AsyncOperation _currentLoadOperation;

        public override void Initialize(ManagerInitializer initializer)
        {
            base.Initialize(initializer);

            CurrentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            UnityEngine.SceneManagement.SceneManager.sceneLoaded += HandleSceneLoaded;
            UnityEngine.SceneManagement.SceneManager.sceneUnloaded += HandleSceneUnloaded;
        }

        public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (IsLoading)
            {
                Debug.LogWarning($"[SceneManager] 이미 씬 로드 중입니다: {sceneName} 요청 무시됨");
                return;
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, mode);
        }

        public void LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (IsLoading)
            {
                Debug.LogWarning($"SceneManager 로딩중");
                return;
            }

            IsLoading = true;
            OnSceneLoadStarted?.Invoke(sceneName);

            _currentLoadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, mode);
            if (_currentLoadOperation != null)
                _currentLoadOperation.completed += _ =>
                {
                    IsLoading = false;
                    _currentLoadOperation = null;
                };
        }

        public float GetLoadProgress()
        {
            return _currentLoadOperation?.progress ?? 0f;
        }
        
        public void UnloadSceneAsync(string sceneName)
        {
            var operation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
            if (operation == null)
            {
                Debug.LogWarning($"[SceneManager] 씬 언로드 실패: {sceneName} (로드되어 있지 않음)");
            }
        }

        private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            CurrentSceneName = scene.name;
            OnSceneLoaded?.Invoke(scene.name);
        }

        private void HandleSceneUnloaded(Scene scene)
        {
            OnSceneUnloaded?.Invoke(scene.name);
        }
    }
}