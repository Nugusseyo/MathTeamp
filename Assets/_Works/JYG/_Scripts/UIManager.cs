using System;
using UnityEngine;

namespace JYG._Scripts
{
    [CreateAssetMenu(fileName=("new UIManager"),menuName="Managers/UIManager")]
    public class UIManager : ManagerBase
    {
        private GameManager _gameManager;
        public delegate void OnUIChanged(UIState prevState, UIState currentState);

        public event OnUIChanged OnCanvasChanged;

        public override void Initialize(ManagerInitializer initializer)
        {
            base.Initialize(initializer);
            _gameManager = initializer.GetManager<GameManager>();
        }

        public void OpenUI()
        {
            OnCanvasChanged?.Invoke(_gameManager.curUIState, UIState.OPEN);
            _gameManager.curUIState = UIState.OPEN;
        }

        public void CloseUI()
        {
            OnCanvasChanged?.Invoke(_gameManager.curUIState, UIState.CLOSE);
            _gameManager.curUIState = UIState.CLOSE;
        }
    }
}
