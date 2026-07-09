using System;
using NKT.Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JYG._Scripts
{
    [CreateAssetMenu(fileName=("new GameManager"),menuName="Managers/GameManager")]
    public class GameManager : ManagerBase
    {
        [field: SerializeField] public PlayerInputSO PlayerInputSO { get; private set; }
        public int MoneyIncrease { get; private set; } = 1;
        public event Action OnClickUpgrade;
        public UIState curUIState = UIState.CLOSE;

        private void OnEnable()
        {
            Debug.Assert(PlayerInputSO != null, $"GameManager는 PlayerInputSO가 필수입니다.");

            PlayerInputSO.OnMouseClick += HandleMouseClick;
        }

        private void OnDisable()
        {
            if (PlayerInputSO != null)
            {
                PlayerInputSO.OnMouseClick -= HandleMouseClick;
            }
        }

        public void UpgradeModifier(int newValue)
        {
            if(MoneyIncrease == newValue)
                Debug.LogWarning("현재 IncreaseValue와 입력하신 NewValue의 값이 동일합니다.");
            OnClickUpgrade?.Invoke();
            MoneyIncrease = newValue;
        }

        private void HandleMouseClick()
        {
            Debug.Log("Try Add Money");
            if (curUIState != UIState.CLOSE)
                return;
            
            CostManager.Instance.AddMoney(MoneyIncrease);
        }
    }
    
    public enum UIState
    {
        OPEN,
        CLOSE,
        EXIT
    }
}
