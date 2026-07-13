using UnityEngine;

namespace NKT.Upgrade
{
    [CreateAssetMenu(fileName = "UpgradeSo", menuName = "SO/KT/Upgrade", order = 0)]
    public class UpgradeSo : ScriptableObject
    {
        // 업그레이드 하면 그만큼 돈 감소하고 로그써서 업그레이드 비용 점점 증가시키기 
        // 지수 써서 클릭 시 돈 버는 양 증가시키기 SO로 저장 바람
        public ulong baseCost; // int에서 ulong으로 변경
        public float multiplier;
        public float logScale;

        // 지수 함수 기반 비용 계산
        public ulong GetCost(int level)
        {
            // Mathf.Pow는 float를 반환하므로 계산은 double/float로 한 뒤 ulong으로 형변환합니다.
            double cost = baseCost * Mathf.Pow(multiplier, level + 1);
            
            if (cost <= 0) return 1;
            return (ulong)cost;
        }

        // 로그 함수 기반 비용 계산
        public ulong GetLogCost(int level)
        {
            if (level <= 0) return baseCost; 
            
            float levelFactor = Mathf.Pow(level, 1.5f); 
            // 로그 계산 시 데이터 유실을 방지하기 위해 double로 임시 계산합니다.
            double cost = baseCost + (logScale * Mathf.Log(level + 1) * multiplier * levelFactor);
    
            if (cost <= 0) return 1;
            return (ulong)cost;
        }
    }
}