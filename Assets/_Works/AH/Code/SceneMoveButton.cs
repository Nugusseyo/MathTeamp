using UnityEngine;
using JYG._Scripts;

namespace AH.Code
{
    public class SceneMoveButton : MonoBehaviour
    {
        [SerializeField] private ManagerInitializer managerInitializer;
        [SerializeField] private string targetSceneName;

        public void OnClickMoveScene()
        {
            SceneManager sceneManager = managerInitializer.GetManager<SceneManager>();
            sceneManager.LoadSceneAsync(targetSceneName);
        }
    }
}