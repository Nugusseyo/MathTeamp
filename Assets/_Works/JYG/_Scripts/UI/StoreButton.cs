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
        
        [SerializeField] private Vector3 worldMoveOffset = new Vector3(30f, 0f, 0f); 
        [SerializeField] private Color highlightColor = new Color(1f, 0.9f, 0.5f);

        private RectTransform _rectTransform;
        private Image _buttonImage;
        
        private Vector3 _originalScale;
        private Vector3 _originalWorldPosition;
        private Color _originalColor;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _buttonImage = GetComponent<Image>();

            if (_rectTransform != null)
            {
                _originalScale = _rectTransform.localScale;
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
                AnimateButton(false);
            }
            else
            {
                AnimateButton(true);
            }
        }

        private void AnimateButton(bool isTargetState)
        {
            _rectTransform.DOKill();
            if (_buttonImage != null) _buttonImage.DOKill();

            if (isTargetState)
            {
                _rectTransform.DOMove(_originalWorldPosition + worldMoveOffset, duration)
                    .SetEase(Ease.OutCubic);
                
                _rectTransform.DOScale(targetScale, duration)
                    .SetEase(Ease.OutBack);
                
                if (_buttonImage != null)
                {
                    _buttonImage.DOColor(highlightColor, duration)
                        .SetEase(Ease.OutCubic);
                }
            }
            else
            {
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