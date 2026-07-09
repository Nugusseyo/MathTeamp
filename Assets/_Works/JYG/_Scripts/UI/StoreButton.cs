using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace JYG._Scripts.UI
{
    public class StoreButton : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;
        
        [Header("DOTween Settings")]
        [SerializeField] private float duration = 0.25f;
        [SerializeField] private Vector3 targetScale = new Vector3(1.15f, 1.15f, 1f);
        
        // ⚠️ 월드 좌표 기준이므로 픽셀 단위 offset이 아닌, 월드 공간에서의 이동량입니다.
        // UI 구조나 캔버스 스케일에 따라 값을 조절해 주세요 (예: 0.5f ~ 50f 등)
        [SerializeField] private Vector3 worldMoveOffset = new Vector3(30f, 0f, 0f); 
        [SerializeField] private Color highlightColor = new Color(1f, 0.9f, 0.5f);

        private RectTransform _rectTransform;
        private Image _buttonImage;
        
        private Vector3 _originalScale;
        private Vector3 _originalWorldPosition; // 월드 포지션 저장용으로 변경
        private Color _originalColor;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _buttonImage = GetComponent<Image>();

            if (_rectTransform != null)
            {
                _originalScale = _rectTransform.localScale;
                // Awake 시점의 초기 월드 포지션을 기록합니다.
                _originalWorldPosition = _rectTransform.position; 
                Debug.Log(_originalWorldPosition);
            }
            if (_buttonImage != null)
            {
                _originalColor = _buttonImage.color;
            }

            if (uiManager != null)
            {
                uiManager.OnCanvasChanged += HandleStoreBehaviour;
            }
        }

        private void OnDestroy()
        {
            if (uiManager != null)
            {
                uiManager.OnCanvasChanged -= HandleStoreBehaviour;
            }
            
            _rectTransform.DOKill();
            if (_buttonImage != null) _buttonImage.DOKill();
        }

        private void HandleStoreBehaviour(UIState prevState, UIState currentState)
        {
            if (currentState == UIState.CLOSE)
            {
                AnimateButton(true);
            }
            else
            {
                AnimateButton(false);
            }
        }

        private void AnimateButton(bool isTargetState)
        {
            _rectTransform.DOKill();
            if (_buttonImage != null) _buttonImage.DOKill();

            if (isTargetState)
            {
                // 1. DOAnchorPos 대신 DOMove를 사용하여 월드 좌표 기준으로 이동
                _rectTransform.DOMove(_originalWorldPosition + worldMoveOffset, duration)
                    .SetEase(Ease.OutCubic);

                // 2. 크기 확대 (탄성 효과)
                _rectTransform.DOScale(targetScale, duration)
                    .SetEase(Ease.OutBack);

                // 3. 색상 변경
                if (_buttonImage != null)
                {
                    _buttonImage.DOColor(highlightColor, duration)
                        .SetEase(Ease.OutCubic);
                }
            }
            else
            {
                // 원래 월드 좌표로 복귀
                _rectTransform.DOMove(_originalWorldPosition, duration * 0.8f)
                    .SetEase(Ease.OutCubic);
                    
                _rectTransform.DOScale(_originalScale, duration * 0.8f)
                    .SetEase(Ease.OutCubic);

                if (_buttonImage != null)
                {
                    _buttonImage.DOColor(_originalColor, duration * 0.8f)
                        .SetEase(Ease.OutCubic);
                }
            }
        }
    }
}