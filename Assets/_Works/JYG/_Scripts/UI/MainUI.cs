using System;
using System.Diagnostics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

namespace JYG._Scripts.UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;
        private Canvas _canvas;
        private bool _isOpen = false;

        [SerializeField] private RectTransform _rect;

        private void Awake()
        {
            if (_rect == null) Debug.Log("Rect가 없습니다.");
            _canvas = GetComponent<Canvas>();
            if(uiManager != null)
                uiManager.OnCanvasChanged += HandleCanvasAnimation;
        }

        private void OnDestroy()
        {
            if(uiManager != null)
                uiManager.OnCanvasChanged -= HandleCanvasAnimation;
        }

        private void HandleCanvasAnimation(UIState prevState, UIState currentState)
        {
            UIMover(currentState);
        }

        private void UIMover(UIState state)
        {
            float yPos = state switch
            {
                UIState.OPEN => 0f,
                UIState.CLOSE => 2000f,
                _ => 2000f
            };
            transform.DOKill();
            _rect.DOAnchorPos(new Vector3(0, yPos, 0), 0.5f).SetEase(Ease.InExpo);
            Debug.Log($"{state}");
        }

        private void Update()
        {
            if (uiManager == null) return;

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                _isOpen = !_isOpen;
                if (_isOpen)
                {
                    uiManager.OpenUI();
                }
                else
                {
                    uiManager.CloseUI();
                }
            }
        }
    }
}
