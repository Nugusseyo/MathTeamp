using System.Collections;
using NKT.Manager;
using NKT.Upgrade;
using UnityEngine;

namespace JYG._Scripts
{
    public class AutoClickObject : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private UpgradeSo upgradeSO; // 비용 및 자동 획득량 계산용 SO
        [SerializeField] private GameObject activeItem; // 활성화할 아이템 오브젝트
        [SerializeField] private float interval = 1f; // 돈을 버는 주기 (초 단위)

        private int _currentLevel = 0;
        private Coroutine _autoClickCoroutine;

        private void Start()
        {
            // 게임 시작 시 레벨이 0보다 크다면 자동으로 가동 시작
            if (_currentLevel > 0)
            {
                StartAutoClick();
            }
            else
            {
                if (activeItem != null) activeItem.SetActive(false);
            }
        }

        /// <summary>
        /// 외부(UpgradeBtn 등)에서 레벨업 성공 시 호출해주는 메서드
        /// </summary>
        public void UpgradeItem(int level)
        {
            _currentLevel = level;

            if (_currentLevel > 0)
            {
                // 아이템 오브젝트 활성화
                if (activeItem != null) activeItem.SetActive(true);

                // 이미 돌고 있는 코루틴이 있다면 중복 실행 방지를 위해 정지 후 재시작
                StartAutoClick();
            }
        }

        private void StartAutoClick()
        {
            if (_autoClickCoroutine != null)
            {
                StopCoroutine(_autoClickCoroutine);
            }
            _autoClickCoroutine = StartCoroutine(AutoClickRoutine());
        }

        // 주기적으로 돈을 벌어다 주는 코루틴 루프
        private IEnumerator AutoClickRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(interval);

                // 현재 레벨에 맞는 자동 획득 금액을 계산 (지수 함수 GetCost 활용)
                ulong earnedMoney = upgradeSO.GetCost(_currentLevel);

                // CostManager를 통해 안전하게 ulong 재화 추가
                CostManager.Instance.AddMoney(earnedMoney);

                Debug.Log($"[자동 파밍] {interval}초마다 {earnedMoney}원 획득! (현재 레벨: {_currentLevel})");
            }
        }
    }
}