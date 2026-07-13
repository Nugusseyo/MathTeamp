using System;
using UnityEngine;

namespace NKT.Manager
{
    public class CostManager : MonoSingleton<CostManager>
    {
        private ulong _money;

        public ulong Money
        {
            get => _money;
            private set
            {
                // Mathf.Clamp는 float를 반환하므로 ulong 연산 시 데이터가 유실됩니다.
                // ulong은 애초에 음수가 될 수 없으므로, 오버플로우 방지만 고려해주거나 아래처럼 처리합니다.
                _money = value;
            }
        }
        
        public event Action OnMoneyChanged;        

        // 매개변수를 int에서 ulong으로 변경
        public void AddMoney(ulong money)
        {
            // ulong.MaxValue를 넘어가는 오버플로우 방지 처리
            if (ulong.MaxValue - _money < money)
            {
                _money = ulong.MaxValue;
            }
            else
            {
                _money += money;
            }
            OnMoneyChanged?.Invoke();
        }

        public void SpendMoney(ulong money)
        {
            // 언더플로우(0 미만으로 떨어지는 것) 방지 안전장치
            if (_money < money)
            {
                _money = 0;
            }
            else
            {
                _money -= money;
            }
            OnMoneyChanged?.Invoke();
        }

        public bool CanSpendMoney(ulong money)
        {
            return _money >= money;
        }

        [ContextMenu("돈 복사")]
        public void AddLegendMoney()
        {
            AddMoney(10000000UL);
        }
    }
}