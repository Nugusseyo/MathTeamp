using System.Collections;
using NKT.Manager;
using NKT.Upgrade;
using UnityEngine;

namespace NKT.autoClick
{
    public class AutoClicker : MonoBehaviour
    {
        [SerializeField] private UpgradeSo upgradeSo;
        private ulong _getMoney = 1;
        public float clickInterval = 2f;
        public bool isAutoClick = false;

        public void MoneyIncrease(ulong money)
        {
            _getMoney += money;
        }
        
        public void StartAutoClick()
        {
            if (isAutoClick) return;
            StartCoroutine(AutoClickCoroutine());
        }

        private IEnumerator AutoClickCoroutine()
        {
            isAutoClick = true;
            while (true)
            {
                yield return new WaitForSeconds(clickInterval);
                
                CostManager.Instance.AddMoney(_getMoney);
            }
        }
    }
}