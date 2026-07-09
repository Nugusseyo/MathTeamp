using UnityEngine;

namespace NKT.Upgrade
{
    [CreateAssetMenu(fileName = "UpgradeSo", menuName = "SO/KT/Upgrade", order = 0)]
    public class UpgradeSo : ScriptableObject
    {//업그레이드 하면 그만큼 돈 감소하고 로그써서 업그레이드 비용 점점 증가시키기 지수 써서 클릭 시 돈 버는 양 증가시키기 SO로 저장 바람
        public int baseCost;
        public float multiplier;

        public int GetCost(int level)
        {
            float cost = baseCost * Mathf.Pow(multiplier, level);
            return (int)cost;
        }
    }
}