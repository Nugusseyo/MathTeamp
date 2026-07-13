using NKT.Manager;
using NKT.Upgrade;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace JYG._Scripts.UI
{
    public class UpgradeBtn : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private UpgradeSo upgradeSO;
        [SerializeField] private TextMeshProUGUI levelView;
        [SerializeField] private bool isClickBtn = false;
        public int Level { get; private set; } = 0;
        public UnityEvent failedEvent;
        public UnityEvent<int> succeedEvent;
        public void Upgrade()
        {
            ulong targetCost = upgradeSO.GetLogCost(Level);
            if (targetCost <= CostManager.Instance.Money)
            {
                CostManager.Instance.SpendMoney(upgradeSO.GetLogCost(Level));
                if (isClickBtn)
                {
                    gameManager.UpgradeModifier(upgradeSO.GetCost(Level));
                }
                Level++;
                succeedEvent?.Invoke(Level);
                levelView.text = Level +  "/" + upgradeSO.GetLogCost(Level);
            }
            else
            {
                failedEvent?.Invoke();
            }
        }
    }
}
