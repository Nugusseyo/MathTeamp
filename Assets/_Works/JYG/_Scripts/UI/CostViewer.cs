using System;
using NKT.Manager;
using TMPro;
using UnityEngine;

namespace JYG._Scripts.UI
{
    public class CostViewer : MonoBehaviour
    {
        private TextMeshProUGUI _tmp;

        private void Awake()
        {
            _tmp = GetComponent<TextMeshProUGUI>();
            CostManager.Instance.OnMoneyChanged += HandleMoneyChanged;
        }

        private void OnDestroy()
        {
            if (CostManager.Instance != null)
                CostManager.Instance.OnMoneyChanged -= HandleMoneyChanged;
        }

        private void HandleMoneyChanged()
        {
            _tmp.SetText($"{CostCutting(CostManager.Instance.Money)}원");
        }

        private string CostCutting(ulong cost)
        {
            if (cost < 1000)
            {
                return cost.ToString();
            }

            string[] units = { "", "K", "M", "B", "T", "P", "E", "Z", "Y", "X", "C" };

            int unitIndex = 0;
            double displayCost = cost;

            while (displayCost >= 1000 && unitIndex < units.Length - 1)
            {
                displayCost /= 1000;
                unitIndex++;
            }

            return $"{displayCost.ToString("0.#")}{units[unitIndex]}";
        }
    }
}
