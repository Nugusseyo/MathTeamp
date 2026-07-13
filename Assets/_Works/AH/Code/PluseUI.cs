using UnityEngine;
using DG.Tweening;
using Unity.Cinemachine;

namespace AH.Code
{
    public class PulseUI : MonoBehaviour
    {
        [SerializeField, MinMaxRangeSlider(0.1f, 10f)] private Vector2 value = new Vector2(0.8f, 1.2f);
        [SerializeField] private float duration = 0.5f;

        private Vector3 originalScale;
        private Tween pulseTween;

        private void Start()
        {
            originalScale = transform.localScale;

            // 시작을 minValue 크기로 세팅
            transform.localScale = originalScale * value.x;

            StartPulse();
        }

        private void StartPulse()
        {
            pulseTween = transform
                .DOScale(originalScale * value.y, duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void OnDestroy()
        {
            pulseTween?.Kill();
        }
    }
}