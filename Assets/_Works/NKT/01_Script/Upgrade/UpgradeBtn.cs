using System;
using System.Collections;
using NKT.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NKT.Upgrade
{
    public class UpgradeBtn : MonoBehaviour
    {
        [SerializeField] private UpgradeSo upgradeSo;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float colorChangeTime = 0.15f;

        public Action OnUpgrade;

        private Button _button;
        private Image _image;
        private int _currentLevel = 0;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
            text = GetComponentInChildren<TextMeshProUGUI>();
            _button.onClick.AddListener(HandleUpgrade);
        }

        private void Start()
        {
            text.text = upgradeSo.GetCost(_currentLevel).ToString() + "원";
        }

        private void HandleUpgrade()
        {
            int cost = upgradeSo.GetCost(_currentLevel);
            if (CostManager.Instance.CanSpendMoney(cost))
            {
                CostManager.Instance.SpendMoney(cost);
                _currentLevel++;
                StartCoroutine(ColorChange(Color.green));
                text.text = upgradeSo.GetCost(_currentLevel).ToString() + "원";
                Debug.Log("야르");
                //업그레이드 했을때 바뀌는 거
                OnUpgrade?.Invoke();
            }
            else
            {
                //실패 피드백
                StartCoroutine(ColorChange(Color.red));
                Debug.Log("엉엉쓰");
            }
        }
        private IEnumerator ColorChange(Color color)
        {
            Color before = _image.color;
            _image.color = color;
            yield return new WaitForSeconds(colorChangeTime);
            _image.color = before;
        }
    }
}