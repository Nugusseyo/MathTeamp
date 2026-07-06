using System;
using UnityEngine;

namespace NKT.Manager
{
    public class CostManager : MonoSingleton<CostManager>
    {
        private int _money;

        public int Money
        {
            get => _money;
            private set => _money = Mathf.Clamp(value, 0,  int.MaxValue);
        }
        
        public event Action OnMoneyChanged;        

        public void AddMoney(int money)
        {
            _money += money;
            OnMoneyChanged?.Invoke();
        }
        public void SpendMoney(int money)
        {
            _money -= money;
            OnMoneyChanged?.Invoke();
        }

        public bool CanSpendMoney(int money)
        {
            return _money < money;
        }
    }
}
